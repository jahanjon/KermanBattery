using KBA.SellerApplication.Contract.Auth;
using KBA.SellerApplication.Contract.Seller;
using KBA.SellerApplication.Service;
using KBA.SellerInfrastructure.Mapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using KBA.SellerInfrastructure.Persistence;
using KBA.SellerInfrastructure.Repository;
using KBA.Domain.Entity.SellerAgg;
using KBA.Domain.Entity.SellerLogin;
using KBA.Domain.Repository;
using System.Text;

namespace KBA.SellerConfiguration
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
