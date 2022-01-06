using AnnuaireEntreprise.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AnnuaireEntreprise
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            CreateServiceCollection(services);
            serviceProvider = services.BuildServiceProvider();
        }

        static void ConfigSetup(IConfigurationBuilder builder)
        {
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        }

        public void CreateServiceCollection(IServiceCollection services)
        {
            var connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=AnnuaireEntreprise;Trusted_Connection=True;MultipleActiveResultSets=true";

            services.AddDbContext<AnnuaireContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddSingleton<MainWindow>();

        }

        public class AnnuaireDBContextFactory : IDesignTimeDbContextFactory<AnnuaireContext>
        {
            public AnnuaireContext CreateDbContext(string[] args)
            {
                IConfiguration config =
                    new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                var optionsBuilder = new DbContextOptionsBuilder<AnnuaireContext>();
                optionsBuilder.UseSqlServer(config.GetConnectionString("SQLServerConnection"));

                return new AnnuaireContext(optionsBuilder.Options);
            }
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
