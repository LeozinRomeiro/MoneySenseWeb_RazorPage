using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Models;

namespace MoneySenseWeb.Data.Mappings
{
	public class CategoryMap : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Category");
			
			builder.HasKey(x => x.CategoryId);
			
			builder.Property(x=>x.CategoryId)
				.ValueGeneratedOnAdd()
				.UseIdentityColumn();

			builder.Property(x => x.Title)
				.IsRequired()
				.HasColumnName("Title")
				.HasColumnType("NVARCHAR(50)");

			builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasColumnType("NVARCHAR(100)");

            builder.Property(x => x.Icon)
				.IsRequired()
				.HasColumnName("Icon")
				.HasColumnType("NVARCHAR(5)");

			builder.Property(x => x.Type)
				.IsRequired()
				.HasColumnName("Type")
				.HasColumnType("NVARCHAR(10)");

            builder.Ignore(x => x.TitleWithIcon);
        }
	}
}
