using Microsoft.AspNetCore.Mvc;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Models;

namespace Sucre.Controllers
{
    public class CexController : Controller
    {
        //private readonly IDbSucreCex _cexDb;
        private readonly ISucreUnitOfWork _sucreUnitOfWork;

        public CexController(IDbSucreCex cexDb, ISucreUnitOfWork sucreUnitOfWork)
        {
            //_cexDb = cexDb;            
            _sucreUnitOfWork = sucreUnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cexsDb =await _sucreUnitOfWork.repoSucreCex.GetAllAsync();
            IEnumerable<CexM> cexsM = cexsDb.Select(u => new CexM
            {
                Id = u.Id,
                Management = u.Management,
                Area = u.Area,
                CexName = u.CexName,
                Device = u.Device,
                Location = u.Location
            });
            return View(cexsM);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? Id)
        {
            TempData["FieldIsEmpty"] = null;
            CexM cexM = new CexM();
            if (Id == null)
            {
                return View(cexM);
            }
            else
            {
                Cex cex = await _sucreUnitOfWork.repoSucreCex.FindAsync(Id.GetValueOrDefault());
                if (cex == null)
                {
                    return NotFound();
                }
                else
                {
                    //energyM.Id = energy.Id;
                    //energyM.EnergyName = energy.EnergyName;
                    sp_Cex(ref cex, ref cexM, true);
                    return View(cexM);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(CexM cexM)
        {
            TempData["FieldIsEmpty"] = null;
            if (ModelState.IsValid)
            {

                if ((cexM.Management == null || cexM.Management.Trim() == "") &&
                    (cexM.CexName == null || cexM.CexName.Trim() == "") &&
                    (cexM.Area == null || cexM.Area.Trim() == "") &&
                    (cexM.Device == null || cexM.Device.Trim() == "") &&
                    (cexM.Location == null || cexM.Location.Trim() == ""))
                {
                    TempData["FieldIsEmpty"] = "There is no record of the location of the metering point";
                    return View(cexM); ;
                }

                Cex cex = new Cex();
                if (cexM.Id == 0)
                {
                    //Creating
                    //parameterType.Id = parameterTypeM.Id;
                    //parameterType.Name = parameterTypeM.Name;
                    //parameterType.Mnemo = parameterTypeM.Mnemo;
                    //parameterType.UnitMeas = parameterTypeM.UnitMeas;
                    sp_Cex(ref cex, ref cexM, false);
                    await _sucreUnitOfWork.repoSucreCex.AddAsync(cex);
                }
                else
                {
                    //Update
                    cex = await _sucreUnitOfWork.repoSucreCex.FirstOrDefaultAsync(
                                                                filter: item => item.Id == cexM.Id, 
                                                                isTracking: false);
                    if (cex == null)
                    {
                        return NotFound(cex);
                    }
                    else
                    {
                        //parameterType.Id = parameterTypeM.Id;
                        //parameterType.Name = parameterTypeM.Name;
                        //parameterType.Mnemo = parameterTypeM.Mnemo;
                        //parameterType.UnitMeas = parameterTypeM.UnitMeas;
                        sp_Cex(ref cex, ref cexM, false);
                        _sucreUnitOfWork.repoSucreCex.Update(cex);
                    }
                }
                await _sucreUnitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cexM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            Cex cex = await _sucreUnitOfWork.repoSucreCex.FirstOrDefaultAsync(filter: item => item.Id == Id.GetValueOrDefault());
            if (cex == null) return NotFound(cex);
            CexM cexM = new CexM();
            sp_Cex(ref cex, ref cexM, true);
            cexM.FullName = _sucreUnitOfWork.repoSucreCex.FullName(cex);
            return View(cexM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            Cex cex = await _sucreUnitOfWork.repoSucreCex.FindAsync(Id.GetValueOrDefault());
            if (cex == null) return NotFound(cex);
            _sucreUnitOfWork.repoSucreCex.Remove(cex);
            await _sucreUnitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Синхронизация между моделью и сущностью
        /// </summary>
        /// <param name="cex">сущность</param>
        /// <param name="cexM">модель</param>
        /// <param name="md">направление: 0 в сущность, 1 в модель </param>
        [NonAction]
        private void sp_Cex(ref Cex cex, ref CexM cexM, bool md = false)
        {
           //From model in entity
           if (!md)
           {
                cex.Id = cexM.Id;   
                cex.Management =cexM.Management;
                cex.CexName = cexM.CexName;
                cex.Area =  cexM.Area;
                cex.Device = cexM.Device;
                cex.Location = cexM.Location;
           }
           else //MFrom entity in model
           {
                cexM.Id = cex.Id;
                cexM.Management = cex.Management;
                cexM.CexName = cex.CexName;
                cexM.Area = cex.Area;
                cexM.Device = cex.Device;
                cexM.Location = cex.Location;
            }
        }
    }
}
