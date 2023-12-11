using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Areas.Identity.Data;
using MoneySenseWeb.Data.Mappings;
using MoneySenseWeb.Models;

namespace MoneySenseWeb.Data;

public class ApplicationDbContext : DbContext 
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    public DbSet<Family> Familys { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    //optionsBuilder.UseSqlServer("");//optionsBuilder.LogTo(Console.WriteLine);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new TransactionMap());
        modelBuilder.ApplyConfiguration(new CategoryMap());
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");
        //modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityUserRole<string>>();
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
        modelBuilder.Ignore<IdentityRole<string>>();
    }
}
