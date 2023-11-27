using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace MoneySenseWeb.Areas.Identity.Data
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            
            builder.ToTable("User");

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR(30)");

            builder.Property(x => x.UserLastName)
                .IsRequired()
                .HasColumnName("Last Name")
                .HasColumnType("NVARCHAR(30)");

        }
    }
}
