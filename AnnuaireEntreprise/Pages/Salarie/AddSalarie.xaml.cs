﻿using AnnuaireEntreprise.Context;
using AnnuaireEntreprise.Data.Models;
using AnnuaireEntreprise.Pages.Salarie.Choice;
using AnnuaireEntreprise.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnnuaireEntreprise.Pages.Salarie
{
    /// <summary>
    /// Interaction logic for ModifySalarie.xaml
    /// </summary>
    public partial class AddSalarie : Window
    {
        private readonly AnnuaireContext _context;

        public int IdServiceSalarie;
        public int IdSitesSalarie;

        public AddSalarie(AnnuaireContext context)
        {
            _context = context;
            InitializeComponent();
        }

        //Afficher la liste des services
        private void buttonService_Click(object sender, RoutedEventArgs e)
        {
            var win = new ChoiceServices(_context);
            var result = win.ShowDialog();
            if (result == true)
            {
                IdServiceSalarie = win.IdServices;
                buttonService.Content = win.NameServices;
            }
        }

        //Afficher la liste des lieux de travail
        private void buttonSites_Click(object sender, RoutedEventArgs e)
        {
            var win = new ChoiceSites(_context);
            var result = win.ShowDialog();
            if (result == true)
            {
                IdSitesSalarie = win.IdSites;
                buttonSites.Content = win.CitySites;
            }
        }

        //Valider l'ajout
        private void buttonValider_Click(object sender, RoutedEventArgs e)
        {
            bool CheckDoublon = true;
            bool CheckMail = true;
            bool CheckPhone = true;
            bool CheckMobilePhone = true;

            if (textBoxFirstName.Text != "" && textBoxLastName.Text != "" && textBoxPhone.Text != "" && textBoxMobilePhone.Text != "" && textBoxMail.Text != "" && IdServiceSalarie != 0 && IdSitesSalarie != 0)
            {
                //Vérification doublon
                if (_context.Employees.Where(e => e.Mail == textBoxMail.Text).Count() > 0)
                {
                    var result = MessageBox.Show($"L'adresse email entrée ({textBoxMail.Text}) est déjà présente dans la base de données\nÊtes-vous sur de vouloir continuer ?", "Doublon", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result != MessageBoxResult.Yes)
                    {
                        CheckDoublon = false;
                    }
                }

                //Vérification mail
                try
                {
                    var addr = new System.Net.Mail.MailAddress(textBoxMail.Text);
                    textBoxMail.Text = addr.Address;
                    CheckMail = true;
                }
                catch
                {
                    CheckMail = false;
                    MessageBox.Show("L'adresse email entrée (" + textBoxMail.Text + ") est invalide !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                //Vérification du regex numérique
                Regex regex = new Regex("[^0-9]+");
                CheckPhone = regex.IsMatch(textBoxPhone.Text);
                CheckMobilePhone = regex.IsMatch(textBoxMobilePhone.Text);

                //Vérification des differents paramètres
                if (CheckDoublon && CheckMail && CheckPhone && CheckMobilePhone) 
                {
                    try
                    {
                        var WriteServices = _context.Services.Single(se => se.Id == IdServiceSalarie);
                        var WriteSites = _context.Sites.Single(si => si.Id == IdSitesSalarie);

                        Employee WriteSalarie = new Employee();

                        WriteSalarie.FirstName = textBoxFirstName.Text;
                        WriteSalarie.LastName = textBoxLastName.Text;
                        WriteSalarie.Phone = textBoxPhone.Text;
                        WriteSalarie.MobilePhone = textBoxMobilePhone.Text;
                        WriteSalarie.Mail = textBoxMail.Text;
                        WriteSalarie.Services = WriteServices;
                        WriteSalarie.Sites = WriteSites;

                        _context.Employees.Add(WriteSalarie);
                        _context.SaveChanges();

                        MessageBox.Show("Ajout enregistré", "Ajout", MessageBoxButton.OK, MessageBoxImage.Information);
                        DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Une erreur est survenue lors de l'ajout de l'employé \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Annulation de l'ajout", "Annulation", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }
            else if (CheckPhone == false)
            {
                MessageBox.Show("Le numéro de téléphone est invalide !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (CheckMobilePhone == false)
            {
                MessageBox.Show("Le numéro de téléphone portable est invalide !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Bouton permettant d'annuler l'ajout d'un salarié
        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        //Fonction regex pour vérifier que la textbox est bien numérique
        private void TextRegexNumeriqueSeulement(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
