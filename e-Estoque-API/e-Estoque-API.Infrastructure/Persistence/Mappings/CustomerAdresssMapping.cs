using e_Estoque_API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_Estoque_API.Infrastructure.Persistence.Mappings;

public class CustomerAddresssMapping : IEntityTypeConfiguration<CustomerAddress>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CustomerAddress> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.Street)
            .IsRequired()
            .HasColumnType("varchar(80)");

        builder.Property(c => c.Number)
            .IsRequired()
            .HasColumnType("varchar(80)");

        builder.Property(c => c.Complement)
           .IsRequired()
           .HasColumnType("varchar(80)");

        builder.Property(c => c.Neighborhood)
           .IsRequired()
           .HasColumnType("varchar(80)");

        builder.Property(c => c.District)
           .IsRequired()
           .HasColumnType("varchar(80)");

        builder.Property(c => c.City)
           .IsRequired()
           .HasColumnType("varchar(80)");

        builder.Property(c => c.County)
           .IsRequired()
           .HasColumnType("varchar(80)");

        builder.Property(c => c.ZipCode)
           .IsRequired()
           .HasColumnType("varchar(80)");

        builder.Property(c => c.Latitude)
           .IsRequired()
           .HasColumnType("varchar(80)");

        builder.Property(c => c.Longitude)
           .IsRequired()
           .HasColumnType("varchar(80)");

        builder.Property(p => p.CreatedAt)
            .IsRequired(true);

        builder.Property(p => p.UpdatedAt)
            .IsRequired(false);

        builder.Property(p => p.DeletedAt)
            .IsRequired(false);
    }
}