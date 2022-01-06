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

        private void SelectSites_Click(object sender, RoutedEventArgs e)
        {
            var sender_context = sender as Button;
            var context = sender_context!.DataContext as Sites;

            IdSites = context!.Id;
            CitySites = context.City;

            DialogResult = true;
        }

        private void textBoxSites_TextChanged(object sender, TextChangedEventArgs e)
        {
            FillDataGrid();
        }

        public void FillDataGrid()
        {
            dataGridServices.ItemsSource = _context.Sites
                .Where(s => s.City.Contains(textBoxSites.Text))
                .ToList();
        }
    }
}
