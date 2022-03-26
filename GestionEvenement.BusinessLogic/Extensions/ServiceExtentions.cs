using Microsoft.Extensions.DependencyInjection;
using GestionEvenement.DataAccess.Repositories.Contracts;
using GestionEvenement.DataAccess;

namespace GestionEvenement.BusinessLogic.Extentions
{
    public static class ServiceExtention
    {        
        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
