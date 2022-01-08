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

        //Remplissage de la BDD
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

        private void ModifyService_Click(object sender, RoutedEventArgs e)
        {
            //Affichage d'une form avec la possibilité de modifier l'employée
            var sender_context = sender as System.Windows.Controls.Button;
            var context = sender_context!.DataContext as Services;

            var win = new ModifyService(_context, context.Id, context.Name);
            var result = win.ShowDialog();
            if (result == true)
            {
                FillDataGrid();                
            }

        }

        //Suppression d'un employée
        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            //Demander la confirmation de suppression
            var sender_context = sender as Button;
            
            var context = sender_context!.DataContext as Services;

            //Ajouter la vérification si service attribué
            var resultMsgBoxDelete = MessageBox.Show("Êtes-vous sûr de vouloir supprimer le service : '" + context!.Name + "' ?", "Confirmer la suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultMsgBoxDelete == MessageBoxResult.Yes)
            {
                try 
                {
                    var serviceSelected = _context.Services.Single(s => s.Id == context.Id);
                    if (serviceSelected != null)
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

        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltreText = textBoxService.Text;
            FillDataGrid();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddService(_context);
            var result = win.ShowDialog();
            if (result == true)
            {
                FillDataGrid();
            }
        }
    }
}
