using e_Estoque_API.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace e_Estoque_API.Infrastructure.Persistence.Mappings;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasColumnType("varchar(500)");

        builder.Property(c => c.ShortDescription)
            .IsRequired()
            .HasColumnType("varchar(250)");

        builder.Property(c => c.Price)
            .IsRequired()
            .HasColumnType("decimal");

        builder.Property(c => c.Weight)
            .IsRequired()
            .HasColumnType("decimal");

        builder.Property(c => c.Height)
            .IsRequired()
            .HasColumnType("decimal");

        builder.Property(c => c.Length)
            .IsRequired()
            .HasColumnType("decimal");

        builder.Property(c => c.Image)
            .IsRequired()
            .HasColumnType("varchar(5000)");

        builder.Property(x => x.IdCategory)
            .IsRequired();

        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.IdCategory);

        builder.Property(x => x.IdCompany)
            .IsRequired();

        builder
            .HasOne(x => x.Company)
            .WithMany()
            .HasForeignKey(x => x.IdCompany);

        builder.Property(p => p.CreatedAt)
           .IsRequired(true);

        builder.Property(p => p.UpdatedAt)
            .IsRequired(false);

        builder.Property(p => p.DeletedAt)
            .IsRequired(false);
    }
}