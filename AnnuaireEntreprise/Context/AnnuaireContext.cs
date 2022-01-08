using AnnuaireEntreprise.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace AnnuaireEntreprise.Context
{
    public class AnnuaireContext : DbContext
    {
        public AnnuaireContext(DbContextOptions<AnnuaireContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Sites> Sites { get; set; }
    }
}
