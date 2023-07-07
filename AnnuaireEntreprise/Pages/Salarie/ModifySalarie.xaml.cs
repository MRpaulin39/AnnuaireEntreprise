using AnnuaireEntreprise.Core.Interfaces.Repositories;
using AnnuaireEntreprise.Core.Models;
using AnnuaireEntreprise.Pages.Salarie.Choice;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace AnnuaireEntreprise.Pages.Salarie
{
    /// <summary>
    /// Interaction logic for ModifySalarie.xaml
    /// </summary>
    public partial class ModifySalarie : Window
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ISiteRepository _siteRepository;
        private readonly int _IdSalarie;
        private int _IdServiceSalarie;
        private int _IdSitesSalarie;

        public ModifySalarie(Employee employee, IEmployeeRepository employeeRepository, IServiceRepository serviceRepository, ISiteRepository siteRepository)
        {
            InitializeComponent();

            _IdSalarie = employee.Id;
            textBoxFirstName.Text = employee.FirstName;
            textBoxLastName.Text = employee.LastName;
            textBoxPhone.Text = employee.Phone;
            textBoxMobilePhone.Text = employee.MobilePhone;
            textBoxMail.Text = employee.Mail;
            _IdServiceSalarie = employee.Service.Id;
            buttonService.Content = employee.Service.Name;
            _IdSitesSalarie = employee.Site.Id;
            buttonSites.Content = employee.Site.City;

            _employeeRepository = employeeRepository;
            _serviceRepository = serviceRepository;
            _siteRepository = siteRepository;

            textBoxFirstName.Focus();
        }

        //Afficher la liste des services
        private void buttonService_Click(object sender, RoutedEventArgs e)
        {
            var win = new ChoiceServices(_serviceRepository);
            var result = win.ShowDialog();
            if (result == true)
            {
                _IdServiceSalarie = win.IdServices;
                buttonService.Content = win.NameServices;
            }
        }

        //Afficher la liste des lieux de travail
        private void buttonSites_Click(object sender, RoutedEventArgs e)
        {
            var win = new ChoiceSites(_siteRepository);
            var result = win.ShowDialog();
            if (result == true)
            {
                _IdSitesSalarie = win.IdSites;
                buttonSites.Content = win.CitySites;
            }
        }

        //Valider les modifications
        private void buttonValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee updateEmployee = _employeeRepository.GetOneEmployeeById(_IdSalarie);

                updateEmployee.FirstName = textBoxFirstName.Text;
                updateEmployee.LastName = textBoxLastName.Text;
                updateEmployee.Mail = textBoxMail.Text;
                updateEmployee.MobilePhone = textBoxMobilePhone.Text;
                updateEmployee.Phone = textBoxPhone.Text;
                updateEmployee.Service = new Core.Models.Service() { Id = _IdServiceSalarie };
                updateEmployee.Site = new Core.Models.Site() { Id = _IdSitesSalarie };

                _employeeRepository.UpdateOneEmployee(updateEmployee);

                MessageBox.Show("Modification enregistrée", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);

                DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la modification du salarié \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Annuler les modifications
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
