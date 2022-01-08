using AnnuaireEntreprise.Context;
using System;
using System.Linq;
using System.Windows;

namespace AnnuaireEntreprise.Pages.Service
{
    /// <summary>
    /// Interaction logic for ModifyServices.xaml
    /// </summary>
    public partial class ModifyService : Window
    {
        private readonly AnnuaireContext _context;

        string NameService { get; set; }
        int idService { get; set; }

        public ModifyService(AnnuaireContext context, int id, string Name)
        {
            _context = context;
            InitializeComponent();

            idService = id;
            NameService = Name;
            textBoxNameService.Text = Name;
        }

        //Mise à jour du service
        private void buttonValid_Click(object sender, RoutedEventArgs e)
        {
            //Vérification doublon
            if (_context.Services.Where(se => se.Name == textBoxNameService.Text).Count() > 0)
            {
                MessageBox.Show($"Le service entrée ({textBoxNameService.Text}) est déjà présente dans la base de données !", "Doublon", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            else
            {
                if (textBoxNameService.Text != "")
                {
                    try
                    { 
                        var WriteService = _context.Services.Single(se => se.Id == idService);

                        WriteService.Name = textBoxNameService.Text;

                        _context.Entry(WriteService).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();

                        MessageBox.Show("Modification enregistrée", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);
                        DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Une erreur est survenue lors de la modification du nom du service \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez remplir le nom du service !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        //Bouton d'annulation des modifications
        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
