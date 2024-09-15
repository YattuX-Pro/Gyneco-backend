using Gyneco.Infrastructure.EmailService;
using Gyneco.Infrastructure.Logging;
using Gyneco.Persistence.Contracts.Email;
using Gyneco.Persistence.Contracts.Logging;
using Gyneco.Persistence.Models.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gyneco.Infrastructure
{
    public static class InfrasctructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            return services;
        }
    }
}
