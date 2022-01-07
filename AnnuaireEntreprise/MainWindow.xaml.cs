using AnnuaireEntreprise.Context;
using AnnuaireEntreprise.Pages.Salarie;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnnuaireEntreprise
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AnnuaireContext _context;

        public MainWindow(AnnuaireContext context)
        {
            _context = context;
            InitializeComponent();

            Contents.Content = new VisuSalarie(_context);

        }

        private void VisuSalarie_Click(object sender, RoutedEventArgs e)
        {
            Contents.Content = new VisuSalarie(_context);
        }

        private void AddEmployee(object sender, RoutedEventArgs e)
        {
            var win = new AddSalarie(_context);
            win.ShowDialog();
            
        }
    }
}
