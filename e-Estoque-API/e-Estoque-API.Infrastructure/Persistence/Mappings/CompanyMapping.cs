using e_Estoque_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_Estoque_API.Infrastructure.Persistence.Mappings;

public class CompanyMapping : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.Name)
            .IsRequired()
            .HasColumnType("varchar(80)");

        builder.Property(c => c.DocId)
            .IsRequired()
            .HasColumnType("varchar(80)");

        builder.Property(c => c.Email)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasColumnType("varchar(80)");

        builder.Property(c => c.Description)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(p => p.CreatedAt)
            .IsRequired(true);

        builder.Property(p => p.UpdatedAt)
            .IsRequired(false);

        builder.Property(p => p.DeletedAt)
            .IsRequired(false);

        // Mapeando o value object CompanyAddress como Owned Entity
        builder.OwnsOne(c => c.CompanyAddress, address =>
        {
            address.Property(p => p.Street)
                .HasColumnName("CompanyAddress_Street")
                .HasColumnType("varchar(80)");

            address.Property(p => p.Number)
                .HasColumnName("CompanyAddress_Number")
                .HasColumnType("varchar(80)");

            address.Property(p => p.Complement)
                .HasColumnName("CompanyAddress_Complement")
                .HasColumnType("varchar(80)");

            address.Property(p => p.Neighborhood)
                .HasColumnName("CompanyAddress_Neighborhood")
                .HasColumnType("varchar(80)");

            address.Property(p => p.District)
                .HasColumnName("CompanyAddress_District")
                .HasColumnType("varchar(80)");

            address.Property(p => p.City)
                .HasColumnName("CompanyAddress_City")
                .HasColumnType("varchar(80)");

            address.Property(p => p.Country)
                .HasColumnName("CompanyAddress_County")
                .HasColumnType("varchar(80)");

            address.Property(p => p.ZipCode)
                .HasColumnName("CompanyAddress_ZipCode")
                .HasColumnType("varchar(80)");

            address.Property(p => p.Latitude)
                .HasColumnName("CompanyAddress_Latitude")
                .HasColumnType("varchar(80)");

            address.Property(p => p.Longitude)
                .HasColumnName("CompanyAddress_Longitude")
                .HasColumnType("varchar(80)");
        });
    }

}