using Microsoft.AspNetCore.Mvc;
using Sucre_DataAccess;
using Sucre_DataAccess.Data;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Models;

namespace Sucre.Controllers
{
    public class ParameterTypeController : Controller
    {
        private readonly IDbSucreParameterType _parameterTypeDb;
        private readonly ISucreUnitOfWork _sucreUnitOfWork;

        public ParameterTypeController(IDbSucreParameterType parameterTypeDb,
                                    ISucreUnitOfWork sucreUnitOfWork)
        {
            _parameterTypeDb = parameterTypeDb;
            _sucreUnitOfWork = sucreUnitOfWork;     

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var ggg = _parameterTypeDb.GetAll();
            //var ggg = await _parameterTypeDb.GetAllAsync();
            var ggg = await _sucreUnitOfWork.repoSukreParameterType.GetAllAsync();
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
        public async Task<IActionResult> Create (ParameterTypeM parameterTypeM)
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
                _sucreUnitOfWork.repoSukreParameterType.Add(parameterType);
                //_parameterTypeDb.Add(parameterType);
                _sucreUnitOfWork.Commit();
                //_parameterTypeDb.Save();
                return RedirectToAction(nameof(Index));
            }
            return View (parameterTypeM);
        }

        //[HttpGet]
        //public IActionResult Upsert(int? Id)
        //{
        //    ParameterTypeM parameterTypeM = new ParameterTypeM();
        //    if (Id == null)
        //    {
        //        return View(parameterTypeM);
        //    }
        //    else
        //    {
        //        ParameterType parameterType = _parameterTypeDb.Find(Id.GetValueOrDefault());
        //        if (parameterType == null) 
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            parameterTypeM.Id = parameterType.Id;
        //            parameterTypeM.Name = parameterType.Name;
        //            parameterTypeM.Mnemo = parameterType.Mnemo;
        //            parameterTypeM.UnitMeas = parameterType.UnitMeas;
        //            return View(parameterTypeM);
        //        }
        //    }
        //}
        [HttpGet]
        public async Task<IActionResult> Upsert(int? Id)
        {
            ParameterTypeM parameterTypeM = new ParameterTypeM();
            if (Id == null)
            {
                return View(parameterTypeM);
            }
            else
            {
                //ParameterType parameterType = await _parameterTypeDb.FindAsync(Id.GetValueOrDefault());
                ParameterType parameterType = await _sucreUnitOfWork.repoSukreParameterType.FindAsync(Id.GetValueOrDefault());
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

        //[HttpPost]
        //public IActionResult Upsert(ParameterTypeM parameterTypeM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ParameterType parameterType = new ParameterType();
        //        if (parameterTypeM.Id == 0)
        //        {
        //            //Creating
        //            //parameterType.Id = parameterTypeM.Id;
        //            //parameterType.Name = parameterTypeM.Name;
        //            //parameterType.Mnemo = parameterTypeM.Mnemo;
        //            //parameterType.UnitMeas = parameterTypeM.UnitMeas;
        //            sp_ParameterType(ref parameterType,ref parameterTypeM, false);
        //            _parameterTypeDb.Add(parameterType);
        //        }
        //        else
        //        {
        //            //Update
        //            parameterType = _parameterTypeDb.FirstOrDefault(filter: item => item.Id == parameterTypeM.Id, isTracking: false);
        //            if (parameterType == null) 
        //            {
        //                return NotFound(parameterType);
        //            }
        //            else
        //            {
        //                //parameterType.Id = parameterTypeM.Id;
        //                //parameterType.Name = parameterTypeM.Name;
        //                //parameterType.Mnemo = parameterTypeM.Mnemo;
        //                //parameterType.UnitMeas = parameterTypeM.UnitMeas;
        //                sp_ParameterType(ref parameterType, ref parameterTypeM, false);
        //                _parameterTypeDb.Update(parameterType);
        //            }
        //        }
        //        _parameterTypeDb.Save();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(parameterTypeM);
        //}
        [HttpPost]
        public async Task<IActionResult> Upsert(ParameterTypeM parameterTypeM)
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
                    sp_ParameterType(ref parameterType, ref parameterTypeM, false);
                    //await _parameterTypeDb.AddAsync(parameterType);
                    await _sucreUnitOfWork.repoSukreParameterType.AddAsync(parameterType);
                }
                else
                {
                    //Update
                    parameterType = await _parameterTypeDb.FirstOrDefaultAsync(filter: item => item.Id == parameterTypeM.Id, isTracking: false);
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
                        //await _parameterTypeDb.UpdateAsync(parameterType);
                        await _sucreUnitOfWork.repoSukreParameterType.UpdateAsync(parameterType);
                    }
                }
                //await _parameterTypeDb.SaveAsync();
                _sucreUnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(parameterTypeM);
        }

        [HttpGet]
        private IActionResult DeleteNoAsync(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            //ParameterType parameterType = _parameterTypeDb.FirstOrDefault(filter: item => item.Id == Id.GetValueOrDefault());
            ParameterType parameterType = _sucreUnitOfWork.repoSukreParameterType.FirstOrDefault(filter: item => item.Id == Id.GetValueOrDefault());
            if (parameterType == null) return NotFound(parameterType);
            ParameterTypeM parameterTypeM = new ParameterTypeM();
            sp_ParameterType(ref parameterType, ref parameterTypeM, true);
            return View(parameterTypeM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            return await Task.Run(() => DeleteNoAsync(Id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        private IActionResult DeletePostNoAsync (int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            //var parameterType = _parameterTypeDb.Find(Id.GetValueOrDefault());
            var parameterType = _sucreUnitOfWork.repoSukreParameterType.Find(Id.GetValueOrDefault());
            if (parameterType == null) return NotFound(parameterType);
            //_parameterTypeDb.Remove(parameterType);
            //_parameterTypeDb.Save();
            _sucreUnitOfWork.repoSukreParameterType.Remove(parameterType);
            _sucreUnitOfWork.Commit();
            return RedirectToAction(nameof(Index));
            //return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? Id)
        {
            return await Task.Run(() => DeletePostNoAsync(Id));
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
