using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Models;

namespace MoneySenseWeb.Data.Mappings
{
    public class TransactionMap : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.HasKey(x => x.TransactionId);

            builder.Property(x => x.TransactionId)
                .ValueGeneratedOnAdd()
                .UseIdentityColumn();

            builder.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .HasConstraintName("FK_TRANSACTION_CATEGORY");

            builder.Property(x => x.Amount)
                .IsRequired()
                .HasColumnName("Amount");

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnName("Description")
                .HasColumnType("NVARCHAR(100)");

            builder.Property(x => x.CreateDate)
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
