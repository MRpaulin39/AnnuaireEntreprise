using AnnuaireEntreprise.Context;
using AnnuaireEntreprise.Data.Models;
using AnnuaireEntreprise.ViewModels;
using System;
using System.Collections.Generic;
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

namespace AnnuaireEntreprise.Pages.Salarie.Choice
{
    /// <summary>
    /// Interaction logic for ChoiceServices.xaml
    /// </summary>
    public partial class ChoiceServices : Window
    {
        private readonly AnnuaireContext _context;
        public int IdServices { get; set; }
        public string NameServices { get; set; }
        public string FiltreText { get; set; }

        public ChoiceServices(AnnuaireContext context)
        {
            _context = context;
            InitializeComponent();

            FiltreText = "";

            FillDataGrid();
            
        }

        //Validation du service sélectionné
        private void SelectService_Click(object sender, RoutedEventArgs e)
        {
            var sender_context = sender as Button;
            var context = sender_context!.DataContext as Services;

            IdServices = context!.Id;
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
                dataGridServices.ItemsSource = _context.Services
                    .Where(s => s.Name.Contains(FiltreText))
                    .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des services \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
