using AnnuaireEntreprise.Core.Interfaces.Repositories;
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
        private string FiltreText = string.Empty;
        private string FiltreServices = string.Empty;
        private string FiltreSites = string.Empty;
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
                        
        }

        //Permet d'actualiser le front si utilisateur authentifier ou non
        public void CheckIfUserIsAuthentified(bool IsAuthentified)
        {
            if (IsAuthentified)
            {
                buttonAdd.Visibility = Visibility.Visible;
                DataGridColumnModifier.Visibility = Visibility.Visible;
                DataGridColumnDelete.Visibility = Visibility.Visible;
            }
            else
            {
                buttonAdd.Visibility = Visibility.Hidden;
                DataGridColumnModifier.Visibility = Visibility.Hidden;
                DataGridColumnDelete.Visibility = Visibility.Hidden;
            }
        }

        //Actualisation de la liste des employées
        private void FillDataGrid()
        {
            try
            {
                if (FiltreText != string.Empty || FiltreServices != string.Empty || FiltreSites != string.Empty)
                {
                    dataGridEmployee.ItemsSource = _employeeRepository.GetAllEmployeesFiltered(FiltreText, FiltreServices, FiltreSites);
                }
                else
                {
                    dataGridEmployee.ItemsSource = _employeeRepository.GetAllEmployees();
                }
                
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
            var sender_context = sender as Button;
            Employee employee = sender_context!.DataContext as Employee;

            var win = new ModifySalarie(employee, _employeeRepository, _serviceRepository, _siteRepository);
            var result = win.ShowDialog();
            if (result == true)
            {
                FillDataGrid();
            }
            else
            {
                MessageBox.Show("Modification annulée", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //Suppression d'un employée
        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Demander la confirmation de suppression
                var sender_context = sender as Button;
                Employee employee = sender_context!.DataContext as Employee;

                var resultMsgBoxDelete = MessageBox.Show("Êtes-vous sûr de vouloir supprimer l'employée : '" + employee.FirstName + " " + employee.LastName + "' ?", "Confirmer la suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultMsgBoxDelete == MessageBoxResult.Yes)
                {
                    _employeeRepository.DeleteOneEmployee(employee.Id);
                    FillDataGrid();

                }
            }
            catch (Exception ex) { 
                MessageBox.Show("Une erreur s'est produite lors de la suppression de l'employée : \n\r" + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Permet d'afficher l'interface d'ajout d'un employé
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddSalarie(_employeeRepository, _serviceRepository, _siteRepository);
            var result = win.ShowDialog();
            if (result == true)
            {
                FillDataGrid();
            }
            else
            {
                MessageBox.Show("Annulation de l'ajout", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
