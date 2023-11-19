using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Sucre.Filters;
using Sucre_Core.LoggerExternal;
using Sucre_DataAccess.Data;
using Sucre_DataAccess.Repository;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_DataAccess.Services;
using Sucre_Services;
using Sucre_Services.Interfaces;
using Sucre_Utility;

namespace Sucre
{

    public class Program
    {       
        public static void Main(string[] args)
        {
            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Verbose()
            //    .WriteTo.Console()
            //    .WriteTo.File("Log-.log", rollingInterval: RollingInterval.Day)
            //    .CreateLogger();
            //Serilog.ILogger logger = new LoggerConfiguration()
            //       .MinimumLevel.Debug()
            //       .Enrich.FromLogContext()
            //       .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
            //       .WriteTo.File("Log-.log", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
            //       .CreateLogger();
            //Log.Logger = logger;
            //Log.Information($"*->------Program Main start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} ------");
            //Global Logger. Sucre_Core
            LoggerExternal.LoggerEx.Information($"*->------Program Main start {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} ------");
            try
            {
                //check record in logger file for fatal err
                //var s = 0;
                //var r = 1 / s;

                var builder = WebApplication.CreateBuilder(args);

                //Serilog.ILogger logger = new LoggerConfiguration()
                //    .MinimumLevel.Debug()
                //    .Enrich.FromLogContext()
                //    .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
                //    .WriteTo.File("Log-.log", rollingInterval: RollingInterval.Day,restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
                //    .CreateLogger();
                builder.Logging.ClearProviders();
                //builder.Logging.AddSerilog(logger);
                //Link External Logger from Sucre_Core
                builder.Logging.AddSerilog(LoggerExternal.LoggerEx);
                //builder.Logging.AddSerilog();

                //logger.Information("*->------Application Run------");
                LoggerExternal.LoggerEx.Information("*->------Application Run------");

                var provider = builder.Services.BuildServiceProvider();
                var configuration = provider.GetRequiredService<IConfiguration>();
                builder.Services.AddSingleton<IConfiguration>(configuration);

                // Add services to the container.
                builder.Services.AddControllersWithViews(options =>
                {
                    //options.Filters.Add(new ResourceFilterGlobal(0));
                });

                builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/AppUser/AppUserLogin";
                        options.AccessDeniedPath = "/AppUser/AccessDeniedPath";
                        options.LogoutPath = "/AppUser/AppUserLogout";
                    });

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

                //подключаемые сервисы из BLL
                builder.Services.AddScoped<IUserService, UserService>();

                builder.Services.AddScoped<InitApplicattionDbContext>();

                //builder.Services.AddScoped<HttpContext>();

                //builder.Services.AddScoped<IConfigurationSection>();

                //for filters
                //builder.Services.AddScoped<ResourceFilterAsyncLog>();
                builder.Services.AddScoped<ResourceFilterAsyncLog>();

                //logger.Information($"*->Services register successfully ({builder.ToString()})");
                LoggerExternal.LoggerEx.Information($"*->Services register successfully ({builder.ToString()})");

                var app = builder.Build();

                //logger.Information($"*->App create succesfully ({app.ToString()})");
                LoggerExternal.LoggerEx.Information($"*->App create succesfully ({app.ToString()})");

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

                app.UseAuthentication();        
                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //logger.Information($"*->App run ({app.Environment.EnvironmentName})");
                LoggerExternal.LoggerEx.Information($"*->App run ({app.Environment.EnvironmentName})");
                app.Run();

                //delete
                var t = TestGlobalFilterResourceValue.GlobalResourceIn;
                //logger.Information(t);

            }
            catch (Exception ex) 
            {
                //Log.Fatal(ex, "*->!!! FATAL ERROR. Programs-Main...fucked up");
                LoggerExternal.LoggerEx.Fatal(ex, "*->!!! FATAL ERROR. Programs-Main...fucked up");
            }
            finally 
            {               
                Log.CloseAndFlush();
                var t = TestGlobalFilterResourceValue.GlobalResourceIn;
                Console.WriteLine(t);
            }

            
        }
    }
}