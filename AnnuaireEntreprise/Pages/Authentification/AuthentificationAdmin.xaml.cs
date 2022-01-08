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
using System.Windows.Shapes;

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

        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            CheckPassword();
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CheckPassword();
            }
        }

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
