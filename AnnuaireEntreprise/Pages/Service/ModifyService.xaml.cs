using AnnuaireEntreprise.Core.Interfaces.Repositories;
using AnnuaireEntreprise.Core.Models;
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
        int idService { get; set; }
        private readonly IServiceRepository _serviceRepository;


        public ModifyService(IServiceRepository serviceRepository, Core.Models.Service service)
        {
            InitializeComponent();
            _serviceRepository = serviceRepository;

            idService = service.Id;
            textBoxNameService.Text = service.Name;

            textBoxNameService.Focus();
        }

        //Mise à jour du service
        private void buttonValid_Click(object sender, RoutedEventArgs e)
        {

            ModifyAService();
        }

        //Bouton d'annulation des modifications
        private void buttonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        //En cas d'appuie sur la touche entrée
        private void textBoxNameService_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == System.Windows.Input.Key.Enter)
            {
                ModifyAService();
            }
        }

        //Fonction de modification de service
        public void ModifyAService()
        {
            //Vérification doublon
            try
            {
                _serviceRepository.UpdateOneService(new() { Id = idService, Name = textBoxNameService.Text });

                MessageBox.Show("Modification enregistrée", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);

                DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur imprévue s'est produite ! \nErreur : " + ex.Message);
            }
        }
    }
}
