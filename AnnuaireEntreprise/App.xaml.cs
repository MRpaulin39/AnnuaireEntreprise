using AnnuaireEntreprise.Core.Application.Repositories;
using AnnuaireEntreprise.Core.Infrastructure.Databases;
using AnnuaireEntreprise.Core.Infrastructure.DataLayers;
using AnnuaireEntreprise.Core.Interfaces.Infrastructure;
using AnnuaireEntreprise.Core.Interfaces.Repositories;
using AnnuaireEntreprise.Pages.Authentification;
using AnnuaireEntreprise.Pages.Salarie;
using AnnuaireEntreprise.Pages.Service;
using AnnuaireEntreprise.Pages.Site;
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
            services.AddDbContext<AnnuaireDbContext>();

            services.AddTransient(typeof(MainWindow));
            services.AddTransient(typeof(VisuSalarie));
            services.AddTransient(typeof(VisuServices));
            services.AddTransient(typeof(VisuSite));
            services.AddTransient(typeof(AuthentificationAdmin));

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
