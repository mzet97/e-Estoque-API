using e_Estoque_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_Estoque.Data.Mappings
{
    public class SaleMapping : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Quantity)
                .IsRequired()
                .HasColumnType("integer");

            builder.Property(c => c.SaleType)
                .IsRequired()
                .HasColumnType("integer");

            builder.Property(c => c.PaymentType)
                .IsRequired()
                .HasColumnType("integer");

            builder.Property(c => c.TotalPrice)
                .IsRequired()
                .HasColumnType("decimal");

            builder.Property(c => c.TotalTax)
                .IsRequired()
                .HasColumnType("decimal");

            builder.Property(c => c.DeliveryDate)
                .IsRequired()
               .HasColumnType("timestamp");

            builder.Property(c => c.SaleDate)
               .IsRequired()
               .HasColumnType("timestamp");

            builder.Property(c => c.PaymentDate)
                .IsRequired()
               .HasColumnType("timestamp");

            builder.Property(x => x.IdCustomer)
                .IsRequired();

            builder
                .HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.IdCustomer);

            builder.Property(p => p.CreatedAt)
               .IsRequired(true);

            builder.Property(p => p.UpdatedAt)
                .IsRequired(false);

            builder.Property(p => p.DeletedAt)
                .IsRequired(false);
        }
    }
}