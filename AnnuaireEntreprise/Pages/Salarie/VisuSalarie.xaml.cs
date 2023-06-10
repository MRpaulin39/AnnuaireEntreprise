﻿using AnnuaireEntreprise.Core.Interfaces.Repositories;
using AnnuaireEntreprise.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Policy;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Controls;

namespace AnnuaireEntreprise.Pages.Salarie
{
    /// <summary>
    /// Interaction logic for VisuSalarie.xaml
    /// </summary>
    public partial class VisuSalarie : Page
    {
        private string FiltreText = "";
        private string FiltreServices = "";
        private string FiltreSites = "";
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ISiteRepository _siteRepository;

        public VisuSalarie(IEmployeeRepository employeeRepository, IServiceRepository serviceRepository, ISiteRepository siteRepository) //, bool IsAuthentified
        {
            _employeeRepository = employeeRepository;
            _serviceRepository = serviceRepository;
            _siteRepository = siteRepository;
            InitializeComponent();

            //A l'init, on remplit la DataGrid
            FillDataGrid();

            //Remplit la liste des services
            FillComboBoxServices();

            //Remplit la liste des emplacements
            FillComboBoxVilles();


            //if (IsAuthentified)
            //{
            //    buttonAdd.Visibility = Visibility.Visible;
            //    DataGridColumnModifier.Visibility = Visibility.Visible;
            //    DataGridColumnDelete.Visibility = Visibility.Visible;
            //}
        }

        //Actualisation de la liste des employées
        private void FillDataGrid()
        {
            try
            {
                List<Employee> ListEmployeeBDD = _employeeRepository.GetAllEmployees();
                
                dataGridEmployee.ItemsSource = ListEmployeeBDD;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des employées \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //Permet de remplir le comboBox avec le nom des services
        private void FillComboBoxServices()
        {
            try
            {
                List<Core.Models.Service> ListServices = _serviceRepository.GetAllServices();
                comboBoxService.Items.Add("");

                foreach (Core.Models.Service service in ListServices)
                {
                    comboBoxService.Items.Add(service.Name);
                }

                comboBoxService.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des services \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Permet de remplir le comboBox avec le nom des différentes villes
        private void FillComboBoxVilles()
        {
            try
            {
                List<Core.Models.Site> ListSites = _siteRepository.GetAllSites();
                comboBoxSites.Items.Add("");

                foreach (Core.Models.Site site in ListSites)
                {
                    comboBoxSites.Items.Add(site.City);
                }

                comboBoxSites.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des villes \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Définition du filtre Nom et prénom
        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltreText = textBoxName.Text;
            FillDataGrid();
        }

        //Définition du filtre nom du service
        private void comboBoxService_DropDownClosed(object sender, EventArgs e)
        {
            FiltreServices = comboBoxService.Text;
            FillDataGrid();
        }

        //Définition du filtre lieux de travail
        private void comboBoxSites_DropDownClosed(object sender, EventArgs e)
        {
            FiltreSites = comboBoxSites.Text;
            FillDataGrid();
        }

        //Permet d'afficher la fonction de modification
        private void ModifyEmployee_Click(object sender, RoutedEventArgs e)
        {
            //Affichage d'une form avec la possibilité de modifier l'employée
            //var sender_context = sender as System.Windows.Controls.Button;
            //var context = sender_context!.DataContext as ReadEmployeeViewModels;

            //var win = new ModifySalarie(_context, context!.Id, context.FirstName, context.LastName, context.Phone, context.MobilePhone, context.Mail, context.ServicesId, context.ServicesName, context.SitesId, context.SitesCity);
            //var result = win.ShowDialog();
            //if (result == true)
            //{
            //    FillDataGrid();
            //}
            //else
            //{
            //    MessageBox.Show("Modification annulée", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            //}

        }

        //Suppression d'un employée
        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            //Demander la confirmation de suppression
            //var sender_context = sender as Button;

            //var context = sender_context!.DataContext as ReadEmployeeViewModels;

            //var resultMsgBoxDelete = MessageBox.Show("Êtes-vous sûr de vouloir supprimer l'employée : '" + context!.FirstAndLastName + "' ?", "Confirmer la suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //if (resultMsgBoxDelete == MessageBoxResult.Yes)
            //{
            //    var employeeSelected = _context.Employees.Single(e => e.Id == context.Id);
            //    if (employeeSelected != null)
            //    {
            //        _context.Remove(employeeSelected);
            //        _context.SaveChanges();

            //        FillDataGrid();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Impossible de supprimer l'employée, il n'est pas présent dans la base de données", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    }
            //}

        }

        //Permet d'afficher l'interface d'ajout d'un employé
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            //var win = new AddSalarie(_context);
            //var result = win.ShowDialog();
            //if (result == true)
            //{
            //    FillDataGrid();
            //}
            //else
            //{
            //    MessageBox.Show("Annulation de l'ajout", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
        }
    }
}
