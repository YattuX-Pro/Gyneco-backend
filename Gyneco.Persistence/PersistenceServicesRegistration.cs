

using Gyneco.Persistence.DatabaseContext;
using Kada.Application.Contracts.Pesistence;
using Kada.persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gyneco.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GynecoDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("GynecoConnectionString"));
            });

           /* services.AddDbContext<GynecoDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("GynecoConnectionString")));*/

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }
    }
}
