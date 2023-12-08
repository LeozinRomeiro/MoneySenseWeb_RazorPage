using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Areas.Identity.Data;

namespace MoneySenseWeb.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("User");

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasColumnName("UserName")
                .HasColumnType("NVARCHAR(30)");

            builder.Property(x => x.UserLastName)
                .IsRequired()
                .HasColumnName("UserLastName")
                .HasColumnType("NVARCHAR(30)");

            builder.Ignore(x => x.FullName);

        }
    }
}
