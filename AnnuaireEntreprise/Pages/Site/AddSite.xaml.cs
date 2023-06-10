using System;
using System.Linq;
using System.Windows;

namespace AnnuaireEntreprise.Pages.Site
{
    /// <summary>
    /// Interaction logic for ModifyServices.xaml
    /// </summary>
    public partial class AddSite : Window
    {
        public AddSite()
        {
            //_context = context;
            InitializeComponent();

            textBoxNameSite.Focus();
        }

        //Bouton d'ajout d'un site
        private void buttonValid_Click(object sender, RoutedEventArgs e)
        {
            AddASite();
        }

        //Bouton d'annulation de l'ajout d'un site
        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        //En cas d'appuie sur la touche entrée
        private void textBoxNameSite_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                AddASite();
            }
        }

        //Fonction d'ajout d'une ville
        public void AddASite()
        {
            //Vérification doublon
            //if (_context.Sites.Where(si => si.City == textBoxNameSite.Text).Count() > 0)
            //{
            //    MessageBox.Show($"La ville entrée ({textBoxNameSite.Text}) est déjà présente dans la base de données !", "Doublon", MessageBoxButton.OK, MessageBoxImage.Error);

            //}
            //else
            //{
            //    if (textBoxNameSite.Text != "")
            //    {
            //        try
            //        {
            //            Sites WriteSite = new Sites();

            //            WriteSite.City = textBoxNameSite.Text;

            //            _context.Sites.Add(WriteSite);
            //            _context.SaveChanges();

            //            MessageBox.Show("Ajout enregistré", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);
            //            DialogResult = true;
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show($"Une erreur est survenue lors de l'ajout du lieu \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Veuillez remplir le nom de la ville !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //}
        }
    }
}
