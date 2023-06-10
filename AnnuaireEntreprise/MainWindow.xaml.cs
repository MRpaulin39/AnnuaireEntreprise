using AnnuaireEntreprise.Pages.Salarie;
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


        public MainWindow(VisuSalarie visuSalarie)
        {
            InitializeComponent();

            Contents.Content = _visuSalarie;
            if (IsAuthentified == false)
            {
                MainMenu.Visibility = Visibility.Hidden;
            }

            _visuSalarie = visuSalarie;
        }

        //Afficher l'interface de la liste des salariés
        private void VisuSalarie(object sender, RoutedEventArgs e)
        {
            //    Contents.Content = new VisuSalarie(_context, IsAuthentified);
        }

        //Afficher l'interface de la liste des services
        private void VisuServices(object sender, RoutedEventArgs e)
        {
            //Contents.Content = new VisuServices(_context);
        }

        //Afficher l'interface de la liste des lieux des travails
        private void VisuSites(object sender, RoutedEventArgs e)
        {
            //Contents.Content = new VisuSite(_context);
        }

        //Afficher l'interface d'authentification 
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            //Afficher le formulaire d'authentification
            //if (e.Key == Key.F12 && IsAuthentified == false)
            //{
            //    var win = new AuthentificationAdmin();
            //    var result = win.ShowDialog();

            //    if (result == true)
            //    {
            //        MainMenu.Visibility = Visibility.Visible;
            //        IsAuthentified = true;
            //        Contents.Content = new VisuSalarie(_context, IsAuthentified);
            //    }
            //}
            //else if (e.Key == Key.F12 && IsAuthentified)
            //{
            //    MainMenu.Visibility = Visibility.Hidden;
            //    IsAuthentified = false;
            //    Contents.Content = new VisuSalarie(_context, IsAuthentified);
            //}
        }

    
}
}
