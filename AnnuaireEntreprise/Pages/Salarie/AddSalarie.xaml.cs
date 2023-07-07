using AnnuaireEntreprise.Core.Interfaces.Repositories;
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
    public partial class AddSalarie : Window
    {

        public int IdServiceSalarie;
        public int IdSitesSalarie;

        private readonly IEmployeeRepository _employeeRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ISiteRepository _siteRepository;

        public AddSalarie(IEmployeeRepository employeeRepository, IServiceRepository serviceRepository, ISiteRepository siteRepository)
        {
            InitializeComponent();

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
                IdServiceSalarie = win.IdServices;
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
                IdSitesSalarie = win.IdSites;
                buttonSites.Content = win.CitySites;
            }
        }

        //Valider l'ajout
        private void buttonValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Core.Models.Employee newEmployee = new();

                newEmployee.FirstName = textBoxFirstName.Text;
                newEmployee.LastName = textBoxLastName.Text;
                newEmployee.Phone = textBoxPhone.Text;
                newEmployee.MobilePhone = textBoxMobilePhone.Text;
                newEmployee.Mail = textBoxMail.Text;
                newEmployee.Service = new() { Id = IdServiceSalarie };
                newEmployee.Site = new() { Id = IdSitesSalarie };

                _employeeRepository.AddOneEmployee(newEmployee);

                MessageBox.Show("Ajout enregistré", "Ajout", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de l'ajout de l'employé \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
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
