using AnnuaireEntreprise.Context;
using System;
using System.Linq;
using System.Windows;

namespace AnnuaireEntreprise.Pages.Site
{
    /// <summary>
    /// Interaction logic for ModifyServices.xaml
    /// </summary>
    public partial class ModifySite : Window
    {
        private readonly AnnuaireContext _context;

        string NameSite { get; set; }
        int idSite { get; set; }

        public ModifySite(AnnuaireContext context, int id, string Name)
        {
            _context = context;
            InitializeComponent();

            idSite = id;
            NameSite = Name;
            textBoxNameSite.Text = Name;
        }

        //Mise à jour du site
        private void buttonValid_Click(object sender, RoutedEventArgs e)
        {
            //Vérification doublon
            if (_context.Sites.Where(si => si.City == textBoxNameSite.Text).Count() > 0)
            {
                MessageBox.Show($"La ville entrée ({textBoxNameSite.Text}) est déjà présente dans la base de données !", "Doublon", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                if (textBoxNameSite.Text != "")
                {
                    try
                    { 
                        var WriteSite = _context.Sites.Single(si => si.Id == idSite);

                        WriteSite.City = textBoxNameSite.Text;

                        _context.Entry(WriteSite).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();

                        MessageBox.Show("Modification enregistrée", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);
                        DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Une erreur est survenue lors de la modification du lieux \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez remplir le nom de la ville !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
            
        }

        //Bouton d'annulation de la modificaiton du site
        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
