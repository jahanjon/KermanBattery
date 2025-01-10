using KermanBattery.Farmework.Infrastructure;
using KermanBatterySeller.Infrastructure.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RepresentativePanel.Application.Contract.Auth;
using RepresentativePanel.Application.Contract.Seller;
using RepresentativePanel.Application.Service;
using RepresentativePanel.DataAccess.Mapper;
using RepresentativePanel.DataAccess.Persistence;
using RepresentativePanel.DataAccess.Repository;
using RepresentativePanel.Domain.Entity.SellerAgg;
using RepresentativePanel.Domain.Entity.SellerLogin;
using RepresentativePanel.Domain.Repository;
using System.Text;

namespace RepresentativePanel.Configuration
{
    public static class RepresentativePanelServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ISellerRepository, SellerRepository>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IAuthDataService, AuthDataService>();
            services.AddTransient<ISellerService,SellerService>();
            services.AddTransient<ISellerLoginRepository,SellerLoginRepository>();
            services.AddTransient<ISellerLoginService,SellerLoginService>();
        }
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
        public static void AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<KermanBatterySellerContext>(option =>
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
        public static void RegisterMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DashbordProfile));
            services.AddAutoMapper(typeof(SellerLoginProfile));
        }
    }
}
