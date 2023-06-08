using AnnuaireEntreprise.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AnnuaireEntreprise.Core.Infrastructure.Databases.EntityConfigurations
{
    public class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            //Permet de définir la clé primaire
            builder.HasKey(e => e.Id);

            //Permet de définir le nom de la table
            //Par défaut, c'est le nom du model
            builder.ToTable("P_Service");

            //Défini la longueur max d'un champ
            builder.Property(e => e.Name).HasMaxLength(35);
        }
    }
}
