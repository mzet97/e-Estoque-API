using e_Estoque_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_Estoque_API.Infrastructure.Persistence.Mappings;

public class InventoryMapping : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.Quantity)
            .IsRequired()
            .HasColumnType("integer");

        builder.Property(c => c.DateOrder)
            .IsRequired()
            .HasColumnType("varchar(80)");

        builder.Property(x => x.IdProduct)
            .IsRequired();

        builder
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.IdProduct);

        builder.Property(p => p.CreatedAt)
            .IsRequired(true);

        builder.Property(p => p.UpdatedAt)
            .IsRequired(false);

        builder.Property(p => p.DeletedAt)
            .IsRequired(false);
    }
}