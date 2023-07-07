using AnnuaireEntreprise.Core.Application.Repositories;
using AnnuaireEntreprise.Core.Interfaces.Repositories;
using AnnuaireEntreprise.Core.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AnnuaireEntreprise.Pages.Salarie.Choice
{
    /// <summary>
    /// Interaction logic for ChoiceServices.xaml
    /// </summary>
    public partial class ChoiceServices : Window
    {
        public int IdServices { get; set; }
        public string NameServices { get; set; }
        public string FiltreText { get; set; }

        private readonly IServiceRepository _serviceRepository;

        public ChoiceServices(IServiceRepository serviceRepository)
        {
            InitializeComponent();
            _serviceRepository = serviceRepository;

            FiltreText = "";

            FillDataGrid();
        }

        //Validation du service sélectionné
        private void SelectService_Click(object sender, RoutedEventArgs e)
        {
            var sender_context = sender as Button;
            Core.Models.Service context = sender_context!.DataContext as Core.Models.Service;

            IdServices = context.Id;
            NameServices = context.Name;

            DialogResult = true;
        }

        //Fonction de filtre
        private void textBoxServices_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltreText = textBoxServices.Text;
            FillDataGrid();
        }

        //Actualisation de la liste des services
        public void FillDataGrid()
        {
            try
            {
                dataGridServices.ItemsSource = _serviceRepository.GetAllServices();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des services \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
