using e_Estoque_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_Estoque_API.Infrastructure.Mappings
{
    public class ProfileMapping : IEntityTypeConfiguration<ProfileUser>
    {
        public void Configure(EntityTypeBuilder<ProfileUser> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(80)");

            builder.Property(p => p.LastName)
                .IsRequired()
                .HasColumnType("varchar(80)");

            builder.Property(p => p.BirthDate)
                .IsRequired(false);

            builder.Property(p => p.Type)
                .IsRequired();
        }
    }
}
