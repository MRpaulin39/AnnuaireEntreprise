using AnnuaireEntreprise.Core.CustomException.Repositories;
using AnnuaireEntreprise.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AnnuaireEntreprise.Pages.Service
{
    /// <summary>
    /// Interaction logic for VisuSalarie.xaml
    /// </summary>
    public partial class VisuServices : Page
    {
        string FiltreText = "";
        private readonly IServiceRepository _serviceRepository;

        public VisuServices(IServiceRepository serviceRepository)
        {
            InitializeComponent();

            _serviceRepository = serviceRepository;

            //A l'init, on remplit la DataGrid
            FillDataGrid();

        }

        //Actualisation de la liste des services
        public void FillDataGrid()
        {
            try
            {
                List<Core.Models.Service> ListServicesBDD = _serviceRepository.GetAllServices();

                dataGridService.ItemsSource = ListServicesBDD;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des services \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Bouton pour afficher l'interface de modification
        private void ModifyService_Click(object sender, RoutedEventArgs e)
        {
            //Affichage d'une form avec la possibilité de modifier l'employée
            var sender_context = sender as Button;
            Core.Models.Service service = sender_context!.DataContext as Core.Models.Service;

            var win = new ModifyService(_serviceRepository, service);
            var result = win.ShowDialog();
            if (result == true)
            {
                FillDataGrid();
            }
            else
            {
                MessageBox.Show("La modification a été annulée", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        //Suppression d'un service
        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            //Demander la confirmation de suppression
            var sender_context = sender as Button;

            Core.Models.Service serviceToDelete = sender_context!.DataContext as Core.Models.Service;

            var resultMsgBoxDelete = MessageBox.Show("Êtes-vous sûr de vouloir supprimer le service : '" + serviceToDelete.Name + "' ?", "Confirmer la suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultMsgBoxDelete == MessageBoxResult.Yes)
            {
                try
                {
                    //Récupération du nombre d'attribution du service à un employé
                    _serviceRepository.DeleteOneService(serviceToDelete.Id);
                }
                catch (ServiceRepositoryException ex)
                {
                    MessageBox.Show(ex.Message, "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur est survenue lors de la suppression du service \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        //Application du filtre
        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltreText = textBoxService.Text;
            FillDataGrid();
        }

        //Affichage de l'interface d'ajout d'un service
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddService(_serviceRepository);
            var result = win.ShowDialog();
            if (result == true)
            {
                FillDataGrid();
            }
            else
            {
                MessageBox.Show("L'ajout a été annulé", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
