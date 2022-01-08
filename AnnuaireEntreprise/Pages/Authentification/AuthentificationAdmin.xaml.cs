using System.Windows;
using System.Windows.Input;

namespace AnnuaireEntreprise.Pages.Authentification
{
    /// <summary>
    /// Interaction logic for AuthentificationAdmin.xaml
    /// </summary>
    public partial class AuthentificationAdmin : Window
    {
        public AuthentificationAdmin()
        {
            InitializeComponent();

            textBoxUsername.Focus();
        }

        //Fermer l'interface d'authentification
        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        //Bouton permettant la vérification du mot de passe
        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            CheckPassword();
        }

        //Lors de pression de la touche entrée, vérification du mot de passe
        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CheckPassword();
            }
        }

        //Fontion permettant la vérification du mot de passe
        private void CheckPassword()
        {
            if (textBoxUsername.Text == "Admin" && textBoxPassword.Password == "Admin")
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("L'identifiant ou le mot de passe est incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxPassword.Clear();
            }
        }
    }
}
