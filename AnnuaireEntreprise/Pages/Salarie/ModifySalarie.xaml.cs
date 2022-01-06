using AnnuaireEntreprise.Context;
using AnnuaireEntreprise.Pages.Salarie.Choice;
using AnnuaireEntreprise.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public partial class ModifySalarie : Window
    {
        private readonly AnnuaireContext _context;

        public int IdSalarie;
        public int IdServiceSalarie;
        public int IdSitesSalarie;

        public ModifySalarie(AnnuaireContext context, int Id, string FirstName, string LastName, string Phone, string MobilePhone, string Mail, int ServicesId, string ServicesName, int SitesId, string SitesCity)
        {
            _context = context;
            InitializeComponent();
            IdSalarie = Id;
            textBoxFirstName.Text = FirstName;
            textBoxLastName.Text = LastName;
            textBoxPhone.Text = Phone;
            textBoxMobilePhone.Text = MobilePhone;
            textBoxMail.Text = Mail;
            IdServiceSalarie = ServicesId;
            buttonService.Content = ServicesName;
            IdSitesSalarie = SitesId;
            buttonSites.Content = SitesCity;
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

        //Valider les modifications
        private void buttonValider_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxFirstName.Text != "" && textBoxLastName.Text != "" && textBoxPhone.Text != "" && textBoxMobilePhone.Text != "" && textBoxMail.Text != "")
            {
                var WriteServices = _context.Services.Single(se => se.Id == IdServiceSalarie);
                var WriteSites = _context.Sites.Single(si => si.Id == IdSitesSalarie);

                var WriteSalarie = _context.Employees.Single(e => e.Id == IdSalarie);

                WriteSalarie.Id = IdSalarie;
                WriteSalarie.FirstName = textBoxFirstName.Text;
                WriteSalarie.LastName = textBoxLastName.Text;
                WriteSalarie.Phone = textBoxPhone.Text;
                WriteSalarie.MobilePhone = textBoxMobilePhone.Text;
                WriteSalarie.Mail = textBoxMail.Text;
                WriteSalarie.Services = WriteServices;
                WriteSalarie.Sites = WriteSites;

                _context.Entry(WriteSalarie).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

                MessageBox.Show("Modification enregistrée", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Annuler les modifications
        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
