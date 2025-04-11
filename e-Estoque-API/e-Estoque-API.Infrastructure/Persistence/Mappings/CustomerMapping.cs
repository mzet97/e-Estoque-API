using e_Estoque_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_Estoque_API.Infrastructure.Persistence.Mappings;

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
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
            .HasColumnType("varchar(80)");

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

        // Mapeando o value object CustomerAddress como Owned Entity
        builder.OwnsOne(c => c.CustomerAddress, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("CustomerAddress_Street")
                .HasColumnType("varchar(80)");

            address.Property(a => a.Number)
                .HasColumnName("CustomerAddress_Number")
                .HasColumnType("varchar(80)");

            address.Property(a => a.Complement)
                .HasColumnName("CustomerAddress_Complement")
                .HasColumnType("varchar(80)");

            address.Property(a => a.Neighborhood)
                .HasColumnName("CustomerAddress_Neighborhood")
                .HasColumnType("varchar(80)");

            address.Property(a => a.District)
                .HasColumnName("CustomerAddress_District")
                .HasColumnType("varchar(80)");

            address.Property(a => a.City)
                .HasColumnName("CustomerAddress_City")
                .HasColumnType("varchar(80)");

            address.Property(a => a.County)
                .HasColumnName("CustomerAddress_County")
                .HasColumnType("varchar(80)");

            address.Property(a => a.ZipCode)
                .HasColumnName("CustomerAddress_ZipCode")
                .HasColumnType("varchar(80)");

            address.Property(a => a.Latitude)
                .HasColumnName("CustomerAddress_Latitude")
                .HasColumnType("varchar(80)");

            address.Property(a => a.Longitude)
                .HasColumnName("CustomerAddress_Longitude")
                .HasColumnType("varchar(80)");
        });
    }

}