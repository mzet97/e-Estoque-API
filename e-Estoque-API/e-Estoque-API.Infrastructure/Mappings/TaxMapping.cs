using e_Estoque_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_Estoque.Data.Mappings
{
    public class TaxMapping : IEntityTypeConfiguration<Tax>
    {
        public void Configure(EntityTypeBuilder<Tax> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(80)");

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.Percentage)
                .IsRequired()
                .HasColumnType("decimal");

            builder.Property(x => x.IdCategory)
                .IsRequired();

            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Taxs)
                .HasForeignKey(x => x.IdCategory);

            builder.Property(p => p.CreatedAt)
              .IsRequired(true);

            builder.Property(p => p.UpdatedAt)
                .IsRequired(false);

            builder.Property(p => p.DeletedAt)
                .IsRequired(false);
        }
    }
}