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

        builder.Property(a => a.CompanyAddress.Street)
            .HasColumnName("CompanyAddress_Street")
            .HasColumnType("varchar(80)");

        builder.Property(a => a.CompanyAddress.Number)
            .HasColumnName("CompanyAddress_Number")
            .HasColumnType("varchar(80)");

        builder.Property(a => a.CompanyAddress.Complement)
            .HasColumnName("CompanyAddress_Complement")
            .HasColumnType("varchar(80)");

        builder.Property(a => a.CompanyAddress.Neighborhood)
            .HasColumnName("CompanyAddress_Neighborhood")
            .HasColumnType("varchar(80)");

        builder.Property(a => a.CompanyAddress.District)
            .HasColumnName("CompanyAddress_District")
            .HasColumnType("varchar(80)");

        builder.Property(a => a.CompanyAddress.City)
            .HasColumnName("CompanyAddress_City")
            .HasColumnType("varchar(80)");

        builder.Property(a => a.CompanyAddress.County)
            .HasColumnName("CompanyAddress_County")
            .HasColumnType("varchar(80)");

        builder.Property(a => a.CompanyAddress.ZipCode)
            .HasColumnName("CompanyAddress_ZipCode")
            .HasColumnType("varchar(80)");

        builder.Property(a => a.CompanyAddress.Latitude)
            .HasColumnName("CompanyAddress_Latitude")
            .HasColumnType("varchar(80)");

        builder.Property(a => a.CompanyAddress.Longitude)
            .HasColumnName("CompanyAddress_Longitude")
            .HasColumnType("varchar(80)");

    }
}