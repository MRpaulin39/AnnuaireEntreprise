using AnnuaireEntreprise.Context;
using AnnuaireEntreprise.Data.Models;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AnnuaireEntreprise.Pages.Site
{
    /// <summary>
    /// Interaction logic for VisuSalarie.xaml
    /// </summary>
    public partial class VisuSite : Page
    {
        private readonly AnnuaireContext _context;

        string FiltreText = "";

        public VisuSite(AnnuaireContext context)
        {
            _context = context;
            InitializeComponent();

            //A l'init, on remplit la DataGrid
            FillDataGrid();

        }

        //Affichage de la liste des lieux de travail
        public void FillDataGrid()
        {
            var ListServicesBDD = _context.Sites
                .Where(se => se.City.Contains(FiltreText))
                .ToList();

            dataGridSite.ItemsSource = ListServicesBDD;
        }

        //Affichage l'interface de modification
        private void ModifySite_Click(object sender, RoutedEventArgs e)
        {
            //Affichage d'une form avec la possibilité de modifier l'employée
            var sender_context = sender as System.Windows.Controls.Button;
            var context = sender_context!.DataContext as Sites;

            var win = new ModifySite(_context, context.Id, context.City);
            var result = win.ShowDialog();
            if (result == true)
            {
                FillDataGrid();                
            }

        }

        //Suppression d'un lieu de travail
        private void DeleteSite_Click(object sender, RoutedEventArgs e)
        {
            //Demander la confirmation de suppression
            var sender_context = sender as Button;
            
            var context = sender_context!.DataContext as Sites;

            //Ajouter la vérification si service attribué
            var resultMsgBoxDelete = MessageBox.Show("Êtes-vous sûr de vouloir supprimer la ville : '" + context!.City + "' ?", "Confirmer la suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultMsgBoxDelete == MessageBoxResult.Yes)
            {
                try
                {
                    //Récupération du nombre d'attribution du service à un employé
                    int NbAttribution = _context.Employees.Count(e => e.Sites.Id == context.Id);

                    var siteSelected = _context.Sites.Single(si => si.Id == context.Id);

                    if (NbAttribution > 0)
                    {
                        MessageBox.Show("Impossible de supprimer le lieu de travail, il est attribué à " + NbAttribution + " employé(s) !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else if (siteSelected != null && NbAttribution == 0)
                    {
                        _context.Remove(siteSelected);
                        _context.SaveChanges();

                        FillDataGrid();
                    }
                    else
                    {
                        MessageBox.Show("Impossible de supprimer le lieu de travail, il n'est pas présent dans la base de données", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des lieux \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        //Application du filtre
        private void textBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltreText = textBoxSite.Text;
            FillDataGrid();
        }

        //Affichage de l'interface d'ajout du site
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddSite(_context);
            var result = win.ShowDialog();
            if (result == true)
            {
                FillDataGrid();
            }
        }
    }
}
