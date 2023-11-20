using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Data.Mappings;
using MoneySenseWeb.Models;

namespace MoneySenseWeb.Data;

public class ApplicationDbContext : DbContext 
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<Category> Categorys { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    //optionsBuilder.UseSqlServer("");//optionsBuilder.LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TransactionMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
    }
}
