using AnnuaireEntreprise.Context;
using AnnuaireEntreprise.Pages.Authentification;
using AnnuaireEntreprise.Pages.Salarie;
using AnnuaireEntreprise.Pages.Service;
using AnnuaireEntreprise.Pages.Site;
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

namespace AnnuaireEntreprise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AnnuaireContext _context;
        public bool IsAuthentified = false;


        public MainWindow(AnnuaireContext context)
        {
            _context = context;
            InitializeComponent();

            Contents.Content = new VisuSalarie(_context, IsAuthentified);
            if (IsAuthentified == false)
            {
                MainMenu.Visibility = Visibility.Hidden;
            }
        }

        //Afficher l'interface de la liste des salariés
        private void VisuSalarie(object sender, RoutedEventArgs e)
        {
            Contents.Content = new VisuSalarie(_context, IsAuthentified);
        }

        //Afficher l'interface de la liste des services
        private void VisuServices(object sender, RoutedEventArgs e)
        {
            Contents.Content = new VisuServices(_context);
        }

        //Afficher l'interface de la liste des lieux des travails
        private void VisuSites(object sender, RoutedEventArgs e)
        {
            Contents.Content = new VisuSite(_context);
        }

        //Afficher l'interface d'authentification 
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //Afficher le formulaire d'authentification
            if (e.Key == Key.F12 && IsAuthentified == false)
            {
                var win = new AuthentificationAdmin();
                var result = win.ShowDialog();

                if (result == true)
                {
                    MainMenu.Visibility = Visibility.Visible;
                    IsAuthentified = true;
                    Contents.Content = new VisuSalarie(_context, IsAuthentified);
                }
            }
            else if(e.Key == Key.F12 && IsAuthentified)
            {
                MainMenu.Visibility = Visibility.Hidden;
                IsAuthentified = false;
                Contents.Content = new VisuSalarie(_context, IsAuthentified);
            }
        }
    }
}
