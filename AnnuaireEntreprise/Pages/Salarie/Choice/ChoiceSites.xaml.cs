using AnnuaireEntreprise.Context;
using AnnuaireEntreprise.Data.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AnnuaireEntreprise.Pages.Salarie.Choice
{
    /// <summary>
    /// Interaction logic for ChoiceServices.xaml
    /// </summary>
    public partial class ChoiceSites : Window
    {
        private readonly AnnuaireContext _context;
        public int IdSites { get; set; }
        public string CitySites { get; set; }

        public ChoiceSites(AnnuaireContext context)
        {
            _context = context;
            InitializeComponent();
            FillDataGrid();
            
        }

        //Validation du lieu de travail sélectionné
        private void SelectSites_Click(object sender, RoutedEventArgs e)
        {
            var sender_context = sender as Button;
            var context = sender_context!.DataContext as Sites;

            IdSites = context!.Id;
            CitySites = context.City;

            DialogResult = true;
        }

        //Permettant le filtre
        private void textBoxSites_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillDataGrid();
        }

        //Actualisation de la liste des lieux de travail
        public void FillDataGrid()
        {
            try
            {
                dataGridServices.ItemsSource = _context.Sites
                    .Where(s => s.City.Contains(textBoxSites.Text))
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des lieux \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
