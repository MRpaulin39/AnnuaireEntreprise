using AnnuaireEntreprise.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnnuaireEntreprise.Core.Infrastructure.Databases.EntityConfigurations
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //Permet de définir la clé primaire
            builder.HasKey(e => e.Id);

            //Permet de définir le nom de la table
            //Par défaut, c'est le nom du model
            builder.ToTable("F_Employee");

            //Défini la longueur max d'un champ
            builder.Property(e => e.FirstName).HasMaxLength(50);
            builder.Property(e => e.LastName).HasMaxLength(50);
            builder.Property(e => e.Phone).HasMaxLength(10);
            builder.Property(e => e.MobilePhone).HasMaxLength(12);
            builder.Property(e => e.Mail).HasMaxLength(120);
        }
    }
}
