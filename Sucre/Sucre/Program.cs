using Microsoft.EntityFrameworkCore;
using Sucre_DataAccess.Data;
using Sucre_DataAccess.Repository;
using Sucre_DataAccess.Repository.IRepository;

namespace Sucre
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection").ToString();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
                        
            builder.Services.AddScoped<IDbSucreAsPaz, DbSucreAsPaz>();
            builder.Services.AddScoped<IDbSucreCanal, DbSucreCanal>();
            builder.Services.AddScoped<IDbSucreCex, DbSucreCex>();
            builder.Services.AddScoped<IDbSucreEnergy, DbSucreEnergy>();
            //builder.Services.AddScoped<IDbSucreMethodList, DbSucreMethodList>();
            builder.Services.AddScoped<IDbSucreParameterType, DbSucreParameterType>();
            builder.Services.AddScoped<IDbSucrePoint, DbSucrePoint>();
            builder.Services.AddScoped<ISucreUnitOfWork, SucreUnitOfWork>();


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