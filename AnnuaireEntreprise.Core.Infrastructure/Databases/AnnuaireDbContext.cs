using AnnuaireEntreprise.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireEntreprise.Core.Infrastructure.Databases
{
    public class AnnuaireDbContext : DbContext
    {
        #region Constructeur
        public AnnuaireDbContext(DbContextOptions options) : base(options)
        {
        }

        public AnnuaireDbContext()
        {
        }
        #endregion

        #region Méthodes publiques
        /// <summary>
        /// Permet de configurer la création de la base de données
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EntityConfigurations.EmployeeEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.ServiceEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new EntityConfigurations.SiteEntityTypeConfiguration());
        }
        #endregion

        #region Propriétés
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Site> Sites { get; set; }
        #endregion
    }
}
