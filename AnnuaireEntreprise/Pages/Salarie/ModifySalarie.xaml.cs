using AnnuaireEntreprise.Context;
using AnnuaireEntreprise.Pages.Salarie.Choice;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace AnnuaireEntreprise.Pages.Salarie
{
    /// <summary>
    /// Interaction logic for ModifySalarie.xaml
    /// </summary>
    public partial class ModifySalarie : Window
    {
        private readonly AnnuaireContext _context;

        public int IdSalarie;
        public int IdServiceSalarie;
        public int IdSitesSalarie;
        public string OldMail;

        public ModifySalarie(AnnuaireContext context, int Id, string FirstName, string LastName, string Phone, string MobilePhone, string Mail, int ServicesId, string ServicesName, int SitesId, string SitesCity)
        {
            _context = context;
            InitializeComponent();
            IdSalarie = Id;
            textBoxFirstName.Text = FirstName;
            textBoxLastName.Text = LastName;
            textBoxPhone.Text = Phone;
            textBoxMobilePhone.Text = MobilePhone;
            textBoxMail.Text = Mail;
            IdServiceSalarie = ServicesId;
            buttonService.Content = ServicesName;
            IdSitesSalarie = SitesId;
            buttonSites.Content = SitesCity;
            OldMail = Mail;

            textBoxFirstName.Focus();
        }

        //Afficher la liste des services
        private void buttonService_Click(object sender, RoutedEventArgs e)
        {
            var win = new ChoiceServices(_context);
            var result = win.ShowDialog();
            if (result == true)
            {
                IdServiceSalarie = win.IdServices;
                buttonService.Content = win.NameServices;
            }
        }

        //Afficher la liste des lieux de travail
        private void buttonSites_Click(object sender, RoutedEventArgs e)
        {
            var win = new ChoiceSites(_context);
            var result = win.ShowDialog();
            if (result == true)
            {
                IdSitesSalarie = win.IdSites;
                buttonSites.Content = win.CitySites;
            }
        }

        //Valider les modifications
        private void buttonValider_Click(object sender, RoutedEventArgs e)
        {
            bool CheckDoublon = true;
            bool CheckMail = true;
            bool CheckPhone = true;
            bool CheckMobilePhone = true;

            //Vérification doublon
            if (_context.Employees.Where(e => e.Mail == textBoxMail.Text).Count() > 0 && OldMail != textBoxMail.Text)
            {
                var result = MessageBox.Show($"L'adresse email entrée ({textBoxMail.Text}) est déjà présente dans la base de données\nÊtes-vous sur de vouloir continuer ?", "Doublon", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result != MessageBoxResult.Yes)
                {
                    CheckDoublon = false;
                }
            }

            //Vérification mail
            try
            {
                var addr = new System.Net.Mail.MailAddress(textBoxMail.Text);
                textBoxMail.Text = addr.Address;
                CheckMail = true;
            }
            catch
            {
                CheckMail = false;
                MessageBox.Show("L'adresse email entrée (" + textBoxMail.Text + ") est invalide !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //Vérification du regex numérique, renvoi FALSE si aucun problème
            Regex regex = new Regex("[^0-9]+");
            CheckPhone = regex.IsMatch(textBoxPhone.Text);
            CheckMobilePhone = regex.IsMatch(textBoxMobilePhone.Text);

            //Vérification des differents paramètres
            if (textBoxFirstName.Text != "" && textBoxLastName.Text != "" && textBoxPhone.Text != "" && textBoxMobilePhone.Text != "" && textBoxMail.Text != "")
            {
                if (CheckDoublon && CheckMail && CheckPhone == false && CheckMobilePhone == false)
                {
                    try
                    {
                        var WriteServices = _context.Services.Single(se => se.Id == IdServiceSalarie);
                        var WriteSites = _context.Sites.Single(si => si.Id == IdSitesSalarie);

                        var WriteSalarie = _context.Employees.Single(e => e.Id == IdSalarie);

                        WriteSalarie.Id = IdSalarie;
                        WriteSalarie.FirstName = textBoxFirstName.Text;
                        WriteSalarie.LastName = textBoxLastName.Text;
                        WriteSalarie.Phone = textBoxPhone.Text;
                        WriteSalarie.MobilePhone = textBoxMobilePhone.Text;
                        WriteSalarie.Mail = textBoxMail.Text;
                        WriteSalarie.Services = WriteServices;
                        WriteSalarie.Sites = WriteSites;

                        _context.Entry(WriteSalarie).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();

                        MessageBox.Show("Modification enregistrée", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);
                        DialogResult = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Une erreur est survenue lors de la modification du salarié \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
                else if (CheckPhone)
                {
                    MessageBox.Show("Le numéro de téléphone est invalide !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else if (CheckMobilePhone)
                {
                    MessageBox.Show("Le numéro de téléphone portable est invalide !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Veuillez remplir tous les champs !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Annuler les modifications
        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        //Fonction regex pour vérifier que la textbox est bien numérique
        private void TextRegexNumeriqueSeulement(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
