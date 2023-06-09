using AnnuaireEntreprise.Core.Application.Repositories;
using AnnuaireEntreprise.Core.Infrastructure.DataLayers;
using AnnuaireEntreprise.Core.Interfaces.Infrastructure;
using AnnuaireEntreprise.Core.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Windows;

namespace AnnuaireEntreprise
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(MainWindow));

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
