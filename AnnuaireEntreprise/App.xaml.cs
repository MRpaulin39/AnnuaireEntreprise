using AnnuaireEntreprise.Core.Application.Repositories;
using AnnuaireEntreprise.Core.Infrastructure.DataLayers;
using AnnuaireEntreprise.Core.Interfaces.Infrastructure;
using AnnuaireEntreprise.Core.Interfaces.Repositories;
using AnnuaireEntreprise.Pages.Salarie;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Windows;

namespace AnnuaireEntreprise
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider _ServiceProvider { get; private set; }

        public App()
        {

        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            _ServiceProvider = serviceCollection.BuildServiceProvider();

            MainWindow mainWindow = _ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(MainWindow));
            services.AddTransient(typeof(VisuSalarie));

            #region Résolution des interfaces
            #region DataLayers
            services.AddScoped<IEmployeeDataLayer, EmployeeDataLayer>();
            services.AddScoped<IServiceDataLayer, ServiceDataLayer>();
            services.AddScoped<ISiteDataLayer, SiteDataLayer>();
            #endregion

            #region Repositories
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ISiteRepository, SiteRepository>();
            #endregion
            #endregion
        }

    }
}
