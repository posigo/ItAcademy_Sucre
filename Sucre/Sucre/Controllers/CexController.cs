﻿using Microsoft.AspNetCore.Mvc;
using Sucre_DataAccess.Entity;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Models;

namespace Sucre.Controllers
{
    public class CexController : Controller
    {
        private readonly IDbSucreCex _cexDb;

        public CexController(IDbSucreCex cexDb)
        {
            _cexDb = cexDb;            
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cexsDb = _cexDb.GetAll();
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
        public IActionResult Upsert(int? Id)
        {
            CexM cexM = new CexM();
            if (Id == null)
            {
                return View(cexM);
            }
            else
            {
                Cex cex = _cexDb.Find(Id.GetValueOrDefault());
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
        public IActionResult Upsert(CexM cexM)
        {
            if (ModelState.IsValid)
            {
                Cex cex = new Cex();
                if (cexM.Id == 0)
                {
                    //Creating
                    //parameterType.Id = parameterTypeM.Id;
                    //parameterType.Name = parameterTypeM.Name;
                    //parameterType.Mnemo = parameterTypeM.Mnemo;
                    //parameterType.UnitMeas = parameterTypeM.UnitMeas;
                    sp_Cex(ref cex, ref cexM, false);
                    _cexDb.Add(cex);
                }
                else
                {
                    //Update
                    cex = _cexDb.FirstOrDefault(filter: item => item.Id == cexM.Id, isTracking: false);
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
                        _cexDb.Update(cex);
                    }
                }
                _cexDb.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(cexM);
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            Cex cex = _cexDb.FirstOrDefault(filter: item => item.Id == Id.GetValueOrDefault());
            if (cex == null) return NotFound(cex);
            CexM cexM = new CexM();
            sp_Cex(ref cex, ref cexM, true);
            return View(cexM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            Cex cex = _cexDb.Find(Id.GetValueOrDefault());
            if (cex == null) return NotFound(cex);
            _cexDb.Remove(cex);
            _cexDb.Save();
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
