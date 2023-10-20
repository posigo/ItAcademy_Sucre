using Microsoft.AspNetCore.Mvc;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Models;
using System.Diagnostics;

namespace Sucre.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISucreUnitOfWork _sucreUnitOfWork;
        private readonly InitApplicattionDbContext _initDb;
        private IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, InitApplicattionDbContext initDb,
                                IConfiguration configuration)
        {
            _logger = logger;
            _initDb = initDb;
            _configuration = configuration;
            var connstr = _configuration.GetConnectionString("DefaultConnection").ToString();
            string strDatabase = connstr.Split(';').ToList().FirstOrDefault(item => item.Contains("Database"));
            string result = strDatabase.Split('=').Last().ToString();
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult InitDbData()
        {
            TempData["InitDb"] = null;
            string errMsg = "";
            try
            {
                if (_initDb.InitDbValue(out errMsg))
                {
                    TempData["InitDb"] = $"Ok";
                }
                else
                {
                    TempData["InitDb"] = $"The database tables have been initialized. The procedure was unsuccessful. Error: {errMsg}";
                }
                
            }
            catch (Exception ex) 
            {
                TempData["InitDb"] = $"The database tables have been initialized. The procedure was unsuccessful. Error: {ex.Message}";                                    
            }
            
            //return Ok(parameterType);
            return View(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //return View();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}