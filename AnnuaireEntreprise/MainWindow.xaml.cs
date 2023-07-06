using AnnuaireEntreprise.Pages.Authentification;
using AnnuaireEntreprise.Pages.Salarie;
using AnnuaireEntreprise.Pages.Service;
using AnnuaireEntreprise.Pages.Site;
using System.Windows;
using System.Windows.Input;

namespace AnnuaireEntreprise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsAuthentified = false;
        private readonly VisuSalarie _visuSalarie;
        private readonly VisuServices _visuService;
        private readonly VisuSite _visuSite;
        private readonly AuthentificationAdmin _authentificationAdmin;


        public MainWindow(VisuSalarie visuSalarie, VisuServices visuServices, VisuSite visuSite, AuthentificationAdmin authentificationAdmin)
        {
            InitializeComponent();

            _visuSalarie = visuSalarie;
            _visuService = visuServices;
            _visuSite = visuSite;
            _authentificationAdmin = authentificationAdmin;

            Contents.Content = _visuSalarie;
            if (IsAuthentified == false)
            {
                MainMenu.Visibility = Visibility.Hidden;
            }
        }

        //Afficher l'interface de la liste des salariés
        private void VisuSalarie(object sender, RoutedEventArgs e)
        {
            Contents.Content = _visuSalarie;
        }

        //Afficher l'interface de la liste des services
        private void VisuServices(object sender, RoutedEventArgs e)
        {
            Contents.Content = _visuService;
        }

        //Afficher l'interface de la liste des lieux des travails
        private void VisuSites(object sender, RoutedEventArgs e)
        {
            Contents.Content = _visuSite;
        }

        //Afficher l'interface d'authentification 
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //Afficher le formulaire d'authentification
            if (e.Key == Key.F12 && IsAuthentified == false)
            {
                var win = _authentificationAdmin;
                var result = win.ShowDialog();

                if (result == true)
                {
                    MainMenu.Visibility = Visibility.Visible;
                    IsAuthentified = true;
                    _visuSalarie.CheckIfUserIsAuthentified(true);
                    Contents.Content = _visuSalarie;
                }
            }
            else if (e.Key == Key.F12 && IsAuthentified)
            {
                MainMenu.Visibility = Visibility.Hidden;
                IsAuthentified = false;
                _visuSalarie.CheckIfUserIsAuthentified(false);
                Contents.Content = _visuSalarie;
            }
        }
    }
}
