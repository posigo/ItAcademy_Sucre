using Microsoft.AspNetCore.Mvc;
using Sucre_DataAccess;
using Sucre_DataAccess.Data;
using Sucre_DataAccess.Entity;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Models;

namespace Sucre.Controllers
{
    public class ParameterTypeController : Controller
    {
        private readonly IDbSucreParameterType _parameterTypeDb;

        public ParameterTypeController(IDbSucreParameterType parameterTypeDb)
        {
            _parameterTypeDb = parameterTypeDb;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var ggg = _parameterTypeDb.GetAll();
            IEnumerable<ParameterTypeM> fff = ggg.Select(u => new ParameterTypeM { 
                Id = u.Id, Name = u.Name, Mnemo = u.Mnemo, UnitMeas = u.UnitMeas });
            return View(fff);
        }

        [HttpGet]
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create (ParameterTypeM parameterTypeM)
        {
            if (ModelState.IsValid)
            {
                ParameterType parameterType = new ParameterType
                {
                    Id = parameterTypeM.Id,
                    Name = parameterTypeM.Name,
                    Mnemo = parameterTypeM.Mnemo,
                    UnitMeas = parameterTypeM.UnitMeas
                };
                _parameterTypeDb.Add(parameterType);
                _parameterTypeDb.Save();
                return RedirectToAction(nameof(Index));
            }
            return View (parameterTypeM);
        }

        [HttpGet]
        public IActionResult Upsert(int? Id)
        {
            ParameterTypeM parameterTypeM = new ParameterTypeM();
            if (Id == null)
            {
                return View(parameterTypeM);
            }
            else
            {
                ParameterType parameterType = _parameterTypeDb.Find(Id.GetValueOrDefault());
                if (parameterType == null) 
                {
                    return NotFound();
                }
                else
                {
                    parameterTypeM.Id = parameterType.Id;
                    parameterTypeM.Name = parameterType.Name;
                    parameterTypeM.Mnemo = parameterType.Mnemo;
                    parameterTypeM.UnitMeas = parameterType.UnitMeas;
                    return View(parameterTypeM);
                }
            }
        }

        [HttpPost]
        public IActionResult Upsert(ParameterTypeM parameterTypeM)
        {
            if (ModelState.IsValid)
            {
                ParameterType parameterType = new ParameterType();
                if (parameterTypeM.Id == 0)
                {
                    //Creating
                    //parameterType.Id = parameterTypeM.Id;
                    //parameterType.Name = parameterTypeM.Name;
                    //parameterType.Mnemo = parameterTypeM.Mnemo;
                    //parameterType.UnitMeas = parameterTypeM.UnitMeas;
                    sp_ParameterType(ref parameterType,ref parameterTypeM, false);
                    _parameterTypeDb.Add(parameterType);
                }
                else
                {
                    //Update
                    parameterType = _parameterTypeDb.FirstOrDefault(filter: item => item.Id == parameterTypeM.Id, isTracking: false);
                    if (parameterType == null) 
                    {
                        return NotFound(parameterType);
                    }
                    else
                    {
                        //parameterType.Id = parameterTypeM.Id;
                        //parameterType.Name = parameterTypeM.Name;
                        //parameterType.Mnemo = parameterTypeM.Mnemo;
                        //parameterType.UnitMeas = parameterTypeM.UnitMeas;
                        sp_ParameterType(ref parameterType, ref parameterTypeM, false);
                        _parameterTypeDb.Update(parameterType);
                    }
                }
                _parameterTypeDb.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(parameterTypeM);
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            ParameterType parameterType = _parameterTypeDb.FirstOrDefault(filter: item => item.Id == Id.GetValueOrDefault());
            if (parameterType == null) return NotFound(parameterType);
            ParameterTypeM parameterTypeM = new ParameterTypeM();
            sp_ParameterType(ref parameterType, ref parameterTypeM, true);
            return View(parameterTypeM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePost (int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            var parameterType = _parameterTypeDb.Find(Id.GetValueOrDefault());
            if (parameterType == null) return NotFound(parameterType);
            _parameterTypeDb.Remove(parameterType);
            _parameterTypeDb.Save();
            return RedirectToAction(nameof(Index));
            return View();
        }

        [NonAction]
        private void sp_ParameterType(ref ParameterType parameterType, ref ParameterTypeM parameterTypeM, bool md = false)
        {
            //From model in entity
            if (!md)
            {
                parameterType.Id = parameterTypeM.Id;
                parameterType.Name = parameterTypeM.Name;
                parameterType.Mnemo = parameterTypeM.Mnemo;
                parameterType.UnitMeas = parameterTypeM.UnitMeas;
            }
            else //From entity in model
            {
                parameterTypeM.Id = parameterType.Id;
                parameterTypeM.Name = parameterType.Name;
                parameterTypeM.Mnemo = parameterType.Mnemo;
                parameterTypeM.UnitMeas = parameterType.UnitMeas;
            }
        }
    }
}
