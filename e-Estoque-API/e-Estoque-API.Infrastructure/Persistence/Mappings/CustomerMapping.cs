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

        builder.Property(x => x.IdCustomerAddress).IsRequired();

        builder
            .HasOne(x => x.CustomerAddress)
            .WithMany()
            .HasForeignKey(x => x.IdCustomerAddress);

        builder.Property(p => p.CreatedAt)
            .IsRequired(true);

        builder.Property(p => p.UpdatedAt)
            .IsRequired(false);

        builder.Property(p => p.DeletedAt)
            .IsRequired(false);
    }
}