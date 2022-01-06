using AnnuaireEntreprise.Context;
using AnnuaireEntreprise.Data.Models;
using AnnuaireEntreprise.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for VisuSalarie.xaml
    /// </summary>
    public partial class VisuSalarie : Page
    {
        private readonly AnnuaireContext _context;

        string FiltreText = "";
        string FiltreServices = "";
        string FiltreSites = "";

        public VisuSalarie(AnnuaireContext context)
        {
            _context = context;
            InitializeComponent();

            //A l'init, on remplit la DataGrid
            FillDataGrid();

            //Remplit la liste des services
            FillComboBoxServices();

            //Remplit la liste des emplacements
            FillComboBoxVilles();
        }

        //Remplissage de la BDD
        public void FillDataGrid()
        {
            var ListEmployeeBDD = _context.Employees
                .Join(
                    _context.Services,
                    Employee => Employee.Services.Id,
                    Services => Services.Id,
                    (Employee, Services) => new { Employee, Services }
                )
                .Join(
                    _context.Sites,
                    Employee => Employee.Employee.Sites.Id,
                    Sites => Sites.Id,
                    (Employee, Sites) => new ReadEmployeeViewModels()
                    {
                        Id = Employee.Employee.Id,
                        FirstName = Employee.Employee.FirstName,
                        LastName = Employee.Employee.LastName,
                        Phone = Employee.Employee.Phone,
                        MobilePhone = Employee.Employee.MobilePhone,
                        Mail = Employee.Employee.Mail,
                        ServicesId = Employee.Services.Id,
                        ServicesName = Employee.Services.Name,
                        SitesId = Sites.Id,
                        SitesCity = Sites.City,
                        FirstAndLastName = Employee.Employee.FirstName + " " + Employee.Employee.LastName
                    }
                )
                .Where(e => e.FirstAndLastName.Contains(FiltreText))
                .Where(e => e.ServicesName.Contains(FiltreServices))
                .Where(e => e.SitesCity.Contains(FiltreSites))
                .ToList();

            dataGridEmployee.ItemsSource = ListEmployeeBDD;
        }

        //Permet de remplir le comboBox avec le nom des services
        public void FillComboBoxServices()
        {
            var ListServices = _context.Services.ToList();
            comboBoxService.Items.Add("");

            foreach (Services services in ListServices)
            {
                comboBoxService.Items.Add(services.Name);
            }

            comboBoxService.SelectedIndex = 0;
        }

        //Permet de remplir le comboBox avec le nom des différentes villes
        public void FillComboBoxVilles()
        {
            var ListSites = _context.Sites.ToList();
            comboBoxSites.Items.Add("");

            foreach (Sites sites in ListSites)
            {
                comboBoxSites.Items.Add(sites.City);
            }

            comboBoxSites.SelectedIndex = 0;
        }

        //Définition du filtre Nom et prénom
        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltreText = textBoxName.Text;
            FillDataGrid();
        }

        //Définition du filtre service
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

        private void ModifyEmployee_Click(object sender, RoutedEventArgs e)
        {
            //Affichage d'une form avec la possibilité de modifier l'employée
            var sender_context = sender as System.Windows.Controls.Button;
            var context = sender_context!.DataContext as ReadEmployeeViewModels;

            var win = new ModifySalarie(_context, context!.Id, context.FirstName, context.LastName, context.Phone, context.MobilePhone, context.Mail, context.ServicesId, context.ServicesName, context.SitesId, context.SitesCity);
            var result = win.ShowDialog();
            if (result == true)
            {
                FillDataGrid();
            }

        }

        //Suppression d'un employée
        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            //Demander la confirmation de suppression
            var sender_context = sender as Button;
            
            var context = sender_context!.DataContext as ReadEmployeeViewModels;

            var resultMsgBoxDelete = MessageBox.Show("Êtes-vous sûr de vouloir supprimer l'employée : '" + context!.FirstAndLastName + "' ?", "Confirmer la suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultMsgBoxDelete == MessageBoxResult.Yes)
            {
                var employeeSelected = _context.Employees.Single(e => e.Id == context.Id);
                if (employeeSelected != null)
                {
                    _context.Remove(employeeSelected);
                    _context.SaveChanges();

                    FillDataGrid();
                }
                else
                {
                    MessageBox.Show("Impossible de supprimer l'employée, il n'est pas présent dans la base de données", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            
        }
    }
}
