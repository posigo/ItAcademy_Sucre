﻿using Microsoft.AspNetCore.Mvc;
using Sucre_DataAccess.Entity;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Models;
using Sucre_Utility;

namespace Sucre.Controllers
{
    public class PointController : Controller
    {
        private readonly IDbSucrePoint _pointDb;

        public PointController(IDbSucrePoint pointDb)
        {
            _pointDb = pointDb;        
        }

        [HttpGet]
        public IActionResult Index()
        {
            var pointsDb = _pointDb.GetAll(includeProperties:"Energy");
            IEnumerable<PointTableM> pointTablesM = pointsDb.Select(u => new PointTableM
            {
                Id = u.Id,
                Name = u.Name,
                ServiceStaff = u.ServiceStaff,
                EnergyName = u.Energy.EnergyName
            });
            return View(pointTablesM);
        }

        [HttpGet]
        public IActionResult Upsert(int? Id)
        {
            PointUpsertM pointUpsertM = new PointUpsertM()
            {
                PointM = new PointM(),
                EnergySelectList = _pointDb.GetAllDropdownList(WC.EnergyName),
                CexSelectList = _pointDb.GetAllDropdownList(WC.CexName)
            };
            if (Id == null)
            {
                return View(pointUpsertM);
            }
            else
            {
                Point point = _pointDb.Find(Id.GetValueOrDefault());
                if (point == null)
                {
                    return NotFound(point);
                }
                else
                {
                    PointM pointM = new PointM();
                    sp_Point(ref point, ref pointM, true);
                    pointUpsertM.PointM = pointM;
                    return View(pointUpsertM);
                }
            }
        }

        [HttpPost]
        public IActionResult Upsert(PointM pointM)
        {
            if (ModelState.IsValid)
            {
                Point point = new Point();
                if (point.Id == 0)
                {
                    //Creating                    
                    sp_Point(ref point, ref pointM, false);
                    _pointDb.Add(point);
                }
                else
                {
                    //Update
                    point = _pointDb.FirstOrDefault(filter: item => item.Id == pointM.Id, isTracking: false);
                    if (point == null)
                    {
                        return NotFound(point);
                    }
                    else
                    {                        
                        sp_Point(ref point, ref pointM, false);
                        _pointDb.Update(point);
                    }
                }
                _pointDb.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(pointM);
        }

        //[HttpGet]
        //public IActionResult Delete(int? Id)
        //{
        //    if (Id == null || Id == 0) return NotFound();
        //    Cex cex = _cexDb.FirstOrDefault(filter: item => item.Id == Id.GetValueOrDefault());
        //    if (cex == null) return NotFound(cex);
        //    CexM cexM = new CexM();
        //    sp_Cex(ref cex, ref cexM, true);
        //    return View(cexM);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Delete")]
        //public IActionResult DeletePost(int? Id)
        //{
        //    if (Id == null || Id == 0) return NotFound();
        //    Cex cex = _cexDb.Find(Id.GetValueOrDefault());
        //    if (cex == null) return NotFound(cex);
        //    _cexDb.Remove(cex);
        //    _cexDb.Save();
        //    return RedirectToAction(nameof(Index));
        //}

        /// <summary>
        /// Синхронизация между моделью и сущностью
        /// </summary>
        /// <param name="cex">сущность</param>
        /// <param name="cexM">модель</param>
        /// <param name="md">направление: 0 в сущность, 1 в модель </param>
        [NonAction]
        private void sp_Point(ref Point point, ref PointM pointM, bool md = false)
        {
            //From model in entity
            if (!md)
            {
                point.Id = pointM.Id;
                point.Name = pointM.Name;
                point.Description = pointM.Description;
                point.EnergyId = pointM.EnergyId;
                point.CexId = pointM.CexId;
                point.ServiceStaff = pointM.ServiceStaff;                
            }
            else //MFrom entity in model
            {
                pointM.Id = point.Id;
                pointM.Name = point.Name;
                pointM.Description = point.Description;
                pointM.EnergyId = point.EnergyId;
                pointM.CexId = point.CexId;
                pointM.ServiceStaff = point.ServiceStaff;
            }
        }
    }
}
