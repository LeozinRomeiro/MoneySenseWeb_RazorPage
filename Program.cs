using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MoneySenseWeb.Areas.Identity.Data;
using MoneySenseWeb.Data;
using System.Globalization;
using System.Security.Claims;

namespace MoneySenseWeb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DevDockerConnection"))
                //options.UseSqlServer(builder.Configuration.GetConnectionString("DevSSMSConnection"))
                );

            builder.Services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager<UserSignInManager<User>>()
                .AddRoles<IdentityRole>();

            // Configura��o das op��es padr�o para Identity
            void ConfigureIdentityOptions(IdentityOptions options)
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = false;
                options.SignIn.RequireConfirmedEmail = false;
            }

            builder.Services.Configure<IdentityOptions>(ConfigureIdentityOptions);


            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Conta/Login";
            });

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
            });

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();

                options.AddPolicy("Admin", policy =>
                    policy.RequireAssertion(context =>
                        context.User.HasClaim(c => c.Type == "FamiliaId")
                        && context.User.FindFirst("FamiliaId").Value == ((ClaimsPrincipal)context.Resource).FindFirst("FamiliaId").Value));
            });

            builder.Services.AddRazorPages(options =>
            {
                options.Conventions.AddPageRoute("/Conta/Login", "");
            });

            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NHaF1cWWhIYVJyWmFZfVpgc19GYFZUQGY/P1ZhSXxQd0diXH5XcHRQQGBUUkU=");

            var app = builder.Build();

            //using (var scope = app.Services.CreateScope())
            //{
            //    var services = scope.ServiceProvider;
            //    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            //    string roleName = "Admin";

            //    if (!await roleManager.RoleExistsAsync(roleName))
            //    {
            //        await roleManager.CreateAsync(new IdentityRole(roleName));
            //    }
            //}

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR", "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}