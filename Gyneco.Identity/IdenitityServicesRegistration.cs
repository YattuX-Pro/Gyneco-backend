using Gyneco.Domain;
using Gyneco.Identity.DbContext;
using Gyneco.Identity.Models;
using Gyneco.Persistence.Contracts.Identity;
using Gyneco.Persistence.Models.Identity;
using Kada.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json.Serialization;

namespace Gyneco.Identity
{
    public static class IdenitityServicesRegistration
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<GynecoIdentityDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("GynecoConnectionString")));

            /* services.AddDbContext<GynecoIdentityDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("GynecoConnectionString")));*/

            services.AddIdentity<ApplicationUser, ApplicationUserRoles>()
                .AddEntityFrameworkStores<GynecoIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                });

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IApplicationUser, ApplicationUser>();
            services.AddScoped<RoleManager<ApplicationUserRoles>>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))

                };
            });

            return services;

        }
    }
}
