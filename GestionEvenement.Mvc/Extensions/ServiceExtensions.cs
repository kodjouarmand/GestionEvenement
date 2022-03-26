using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GestionEvenement.BusinessLogic.Services.Contracts;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using GestionEvenement.Domain.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GestionEvenement.Mvc.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(opts =>
                   opts.UseSqlite(configuration.GetConnectionString("SqliteConnectionString")));
        }

        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IEvenementService, EvenementService>();
        }

        public static void ConfigureControllers(this IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            services.AddRazorPages();
        }

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options => { });

        public static void ConfigureAutoMapper(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(Startup));

        public static void ConfigureTempData(this IServiceCollection services)
        {
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
        }
    }
}
