using Microsoft.EntityFrameworkCore;
using Sucre_DataAccess.Data;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_DataAccess.Repository;
using Sucre_Services.Interfaces;
using Sucre_Services;
using Sucre_Mappers;

namespace Sucre_WebApi
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterServices
            (this IServiceCollection services, IConfiguration configuration)
        {

            string connectionString = configuration.GetConnectionString("DefaultConnection").ToString();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));


            //подключаемые сервисы из BLL            
            services.AddScoped<IDbSucreAppRole, DbSucreAppRole>();
            services.AddScoped<IDbSucreAppUser, DbSucreAppUser>();
            services.AddScoped<IDbSucreAsPaz, DbSucreAsPaz>();
            services.AddScoped<IDbSucreCanal, DbSucreCanal>();
            services.AddScoped<IDbSucreCex, DbSucreCex>();
            services.AddScoped<IDbSucreEnergy, DbSucreEnergy>();
            services.AddScoped<IDbSucreGroupUser, DbSucreGroupUser>();
            services.AddScoped<IDbSucreParameterType, DbSucreParameterType>();
            services.AddScoped<IDbSucrePoint, DbSucrePoint>();
            services.AddScoped<ISucreUnitOfWork, SucreUnitOfWork>();

            //подключаемые сервисы из BLL
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEnergyService, EnergyService>();
            services.AddScoped<ICexService, CexService>();

            //Mappers
            services.AddScoped<EnergyMapper>();
            services.AddScoped<CexMapper>();

            return services;
        }
    }
}
