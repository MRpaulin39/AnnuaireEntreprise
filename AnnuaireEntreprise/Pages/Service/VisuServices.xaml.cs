using AnnuaireEntreprise.Context;
using AnnuaireEntreprise.Data.Models;
using System;
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
        private readonly AnnuaireContext _context;

        string FiltreText = "";

        public VisuServices(AnnuaireContext context)
        {
            _context = context;
            InitializeComponent();

            //A l'init, on remplit la DataGrid
            FillDataGrid();

        }

        //Actualisation de la liste des services
        public void FillDataGrid()
        {
            try
            {
                var ListServicesBDD = _context.Services
                    .Where(se => se.Name.Contains(FiltreText))
                    .ToList();

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
            var context = sender_context!.DataContext as Services;

            var win = new ModifyService(_context, context.Id, context.Name);
            var result = win.ShowDialog();
            if (result == true)
            {
                FillDataGrid();                
            }
            else
            {
                MessageBox.Show("La modification a été annulé", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        //Suppression d'un service
        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            //Demander la confirmation de suppression
            var sender_context = sender as Button;
            
            var context = sender_context!.DataContext as Services;

            var resultMsgBoxDelete = MessageBox.Show("Êtes-vous sûr de vouloir supprimer le service : '" + context!.Name + "' ?", "Confirmer la suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultMsgBoxDelete == MessageBoxResult.Yes)
            {
                try 
                {
                    //Récupération du nombre d'attribution du service à un employé
                    int NbAttribution = _context.Employees.Count(e => e.Services.Id == context.Id);

                    var serviceSelected = _context.Services.Single(s => s.Id == context.Id);
                    if (NbAttribution > 0)
                    {
                        MessageBox.Show("Impossible de supprimer le service, il est attribué à " + NbAttribution + " employé(s) !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else if (serviceSelected != null && NbAttribution == 0)
                    {
                        _context.Remove(serviceSelected);
                        _context.SaveChanges();

                        FillDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("Impossible de supprimer le service, il n'est pas présent dans la base de données", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
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
            var win = new AddService(_context);
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
