using e_Estoque_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace e_Estoque_API.Infrastructure.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.IdProfileUser).IsRequired();
            builder.HasOne(x => x.ProfileUser).WithMany().HasForeignKey(x => x.IdProfileUser);
        }
    }
}
