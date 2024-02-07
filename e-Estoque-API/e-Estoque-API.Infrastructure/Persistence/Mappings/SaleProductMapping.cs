using e_Estoque_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_Estoque_API.Infrastructure.Persistence.Mappings;

public class SaleProductMapping : IEntityTypeConfiguration<SaleProduct>
{
    public void Configure(EntityTypeBuilder<SaleProduct> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(x => x.IdProduct)
            .IsRequired();

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnType("integer");

        builder
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey(x => x.IdProduct);

        builder.Property(x => x.IdSale)
            .IsRequired();

        builder
            .HasOne(x => x.Sale)
            .WithMany(x => x.SaleProducts)
            .HasForeignKey(x => x.IdSale);

        builder.Property(p => p.CreatedAt)
          .IsRequired(true);

        builder.Property(p => p.UpdatedAt)
            .IsRequired(false);

        builder.Property(p => p.DeletedAt)
            .IsRequired(false);
    }
}