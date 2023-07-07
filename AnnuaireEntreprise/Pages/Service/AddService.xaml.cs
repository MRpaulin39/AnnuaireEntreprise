using AnnuaireEntreprise.Core.CustomException.Repositories;
using AnnuaireEntreprise.Core.Interfaces.Repositories;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AnnuaireEntreprise.Pages.Service
{
    /// <summary>
    /// Interaction logic for ModifyServices.xaml
    /// </summary>
    public partial class AddService : Window
    {
        private readonly IServiceRepository _serviceRepository;

        public AddService(IServiceRepository serviceRepository)
        {
            InitializeComponent();

            _serviceRepository = serviceRepository;

            textBoxNameService.Focus();
        }

        //Bouton d'ajout de service
        private void buttonValid_Click(object sender, RoutedEventArgs e)
        {
            AddAService();
        }

        //Bouton d'annulation
        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        //Lors de la pression de la touche entrée, on lance la fonction d'ajout
        private void textBoxNameService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddAService();
            }
        }

        //Fonction d'ajout de service
        public void AddAService()
        {
            try
            {
                _serviceRepository.AddOneService(new() { Id = 0, Name = textBoxNameService.Text });

                MessageBox.Show("Ajout réussit", "Ajout réussit", MessageBoxButton.OK, MessageBoxImage.Information);

                DialogResult = true;
                this.Close();
            }
            catch(ServiceRepositoryException ex)
            {
                MessageBox.Show(ex.Message, "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de l'ajout du service \nErreur : " + ex.Message, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
