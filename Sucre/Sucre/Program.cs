using Microsoft.EntityFrameworkCore;
using Sucre_DataAccess.Data;
using Sucre_DataAccess.Repository;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Utility;

namespace Sucre
{
    
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var provider = builder.Services.BuildServiceProvider();
            var configuration = provider.GetRequiredService<IConfiguration>();
            builder.Services.AddSingleton<IConfiguration>(configuration);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection").ToString();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            WP.DatabaseName = connectionString.
                Split(';').ToList().FirstOrDefault(item => item.Contains("Database")).
                Split('=').Last().ToString();
            var dbn = WP.DatabaseName;

            builder.Services.AddScoped<IDbSucreAsPaz, DbSucreAsPaz>();
            builder.Services.AddScoped<IDbSucreCanal, DbSucreCanal>();
            builder.Services.AddScoped<IDbSucreCex, DbSucreCex>();
            builder.Services.AddScoped<IDbSucreEnergy, DbSucreEnergy>();
            //builder.Services.AddScoped<IDbSucreMethodList, DbSucreMethodList>();
            builder.Services.AddScoped<IDbSucreParameterType, DbSucreParameterType>();
            builder.Services.AddScoped<IDbSucrePoint, DbSucrePoint>();
            builder.Services.AddScoped<ISucreUnitOfWork, SucreUnitOfWork>();

            builder.Services.AddScoped<InitApplicattionDbContext>();
            //builder.Services.AddScoped<IConfigurationSection>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}