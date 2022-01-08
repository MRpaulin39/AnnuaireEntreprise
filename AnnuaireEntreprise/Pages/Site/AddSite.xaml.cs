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
using System.Windows.Shapes;

namespace AnnuaireEntreprise.Pages.Site
{
    /// <summary>
    /// Interaction logic for ModifyServices.xaml
    /// </summary>
    public partial class AddSite : Window
    {
        private readonly AnnuaireContext _context;

        public AddSite(AnnuaireContext context)
        {
            _context = context;
            InitializeComponent();
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
                        Sites WriteSite = new Sites();

                        WriteSite.City = textBoxNameSite.Text;

                        _context.Sites.Add(WriteSite);
                        _context.SaveChanges();

                        MessageBox.Show("Ajout enregistré", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);
                        DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Une erreur est survenue lors de l'ajout du lieu \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez remplir le nom de la ville !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            
            
        }

        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
