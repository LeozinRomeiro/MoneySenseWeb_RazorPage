using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoneySenseWeb.Areas.Identity.Data;
using System.Reflection.Emit;

namespace MoneySenseWeb.Data;

public class IdentityContext : IdentityDbContext<User>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Ignore<IdentityUserClaim<string>>();
        builder.Ignore<IdentityUserRole<string>>();
        builder.Ignore<IdentityUserLogin<string>>();
        builder.Ignore<IdentityUserToken<string>>();
        builder.Ignore<IdentityRoleClaim<string>>();
        builder.Ignore<IdentityRole<string>>();
        builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims").HasKey(uc => uc.Id);
        builder.ApplyConfiguration(new UserMap());
    }
}
