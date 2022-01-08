using AnnuaireEntreprise.Context;
using AnnuaireEntreprise.Data.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace AnnuaireEntreprise.Pages.Service
{
    /// <summary>
    /// Interaction logic for ModifyServices.xaml
    /// </summary>
    public partial class AddService : Window
    {
        private readonly AnnuaireContext _context;

        public AddService(AnnuaireContext context)
        {
            _context = context;
            InitializeComponent();

            textBoxNameService.Focus();
        }

        //Bouton d'ajout de service
        private void buttonValid_Click(object sender, RoutedEventArgs e)
        {
            AddAService();
        }

        //Bouton d'annulation
        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        //Lors de la pression de la touche entrée, on lance la fonction d'ajout
        private void textBoxNameService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddAService();
            }
        }

        //Fonction d'ajout de service
        public void AddAService()
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
                        Services WriteService = new Services();

                        WriteService.Name = textBoxNameService.Text;

                        _context.Services.Add(WriteService);
                        _context.SaveChanges();

                        MessageBox.Show("Ajout enregistré", "Ajout", MessageBoxButton.OK, MessageBoxImage.Information);
                        DialogResult = true;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Une erreur est survenue lors de l'ajout du nom du service \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez remplir le nom du service !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
