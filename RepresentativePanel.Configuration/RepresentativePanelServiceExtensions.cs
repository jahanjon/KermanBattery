using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RepresentativePanel.DataAccess;
using RepresentativePanel.Domain.SellerAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Configuration
{
    public static class RepresentativePanelServiceExtensions
    {
        //public static void RegisterServices(this IServiceCollection services)
        //{
        //    services.AddTransient<ISellerRepository, SellerService>();
        //}
        //public static void RegisterRepositories(this IServiceCollection services)
        //{
        //    services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        //}
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepresentativePanelContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("KermanBatteryDbContext"));
            });
        }
        public static void AddJwtAuthenticationToServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var key = Encoding.UTF8.GetBytes(configuration["TokenKey"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["AuthSettings:JwtIssuer"],
                    ValidAudience = configuration["AuthSettings:JwtAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }
        //public static void RegisterMapperProfiles(this IServiceCollection services)
        //{
        //    services.AddAutoMapper(typeof(DashbordProfile));
        //}
    }
}
