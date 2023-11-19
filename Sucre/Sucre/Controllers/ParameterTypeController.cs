﻿using Microsoft.AspNetCore.Mvc;
using Sucre.Filters;
using Sucre_Core.LoggerExternal;
using Sucre_DataAccess;
using Sucre_DataAccess.Data;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Models;

namespace Sucre.Controllers
{
    public class ParameterTypeController : Controller
    {
        //private readonly IDbSucreParameterType _parameterTypeDb;
        private readonly ISucreUnitOfWork _sucreUnitOfWork;
        private readonly ILogger<ParameterTypeController> _log;

        public ParameterTypeController(IDbSucreParameterType parameterTypeDb,
                                    ISucreUnitOfWork sucreUnitOfWork,
                                    ILogger<ParameterTypeController> log)
        {
            //_parameterTypeDb = parameterTypeDb;
            _sucreUnitOfWork = sucreUnitOfWork;     
            _log = log;

        }

        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var ggg = _parameterTypeDb.GetAll();
            //var ggg = await _parameterTypeDb.GetAllAsync();
            var ggg = await _sucreUnitOfWork.repoSucreParameterType.GetAllAsync();
            IEnumerable<ParameterTypeM> fff = ggg.Select(u => new ParameterTypeM { 
                Id = u.Id, Name = u.Name, Mnemo = u.Mnemo, UnitMeas = u.UnitMeas });
            
            var t = TestGlobalFilterResourceValue.GlobalResourceIn;
            _log.LogInformation($"!!!{t}!!!");
            LoggerExternal.LoggerEx.Information($"!!!{t}!!!");
            Console.WriteLine(t);

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
                await _sucreUnitOfWork.repoSucreParameterType.AddAsync(parameterType);
                //_parameterTypeDb.Add(parameterType);
                await _sucreUnitOfWork.CommitAsync();
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
                ParameterType parameterType = await _sucreUnitOfWork.repoSucreParameterType.FindAsync(Id.GetValueOrDefault());
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
                    await _sucreUnitOfWork.repoSucreParameterType.AddAsync(parameterType);
                }
                else
                {
                    //Update
                    parameterType = await _sucreUnitOfWork.repoSucreParameterType.FirstOrDefaultAsync(
                                                                                filter: item => item.Id == parameterTypeM.Id, 
                                                                                isTracking: false);
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
                        await _sucreUnitOfWork.repoSucreParameterType.UpdateAsync(parameterType);
                    }
                }
                //await _parameterTypeDb.SaveAsync();
                await _sucreUnitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parameterTypeM);
        }

        [HttpGet]        
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            //ParameterType parameterType = _parameterTypeDb.FirstOrDefault(filter: item => item.Id == Id.GetValueOrDefault());
            ParameterType parameterType = await _sucreUnitOfWork.repoSucreParameterType.FirstOrDefaultAsync(
                                                                                filter: item => item.Id == Id.GetValueOrDefault());
            if (parameterType == null) return NotFound(parameterType);
            ParameterTypeDelM parameterTypeDelM = new ParameterTypeDelM()
            { 
                Id = parameterType.Id,
                FullName = _sucreUnitOfWork.repoSucreParameterType.GetStringName(parameterType)
            };
            //ParameterTypeM parameterTypeM = new ParameterTypeM();
            //sp_ParameterType(ref parameterType, ref parameterTypeM, true);
            //return View(parameterTypeM);
            return View(parameterTypeDelM);
        }
        //[HttpGet]
        //public async Task<IActionResult> Delete(int? Id)
        //{
        //    return await Task.Run(() => DeleteNoAsync(Id));
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost (int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            //var parameterType = _parameterTypeDb.Find(Id.GetValueOrDefault());
            var parameterType = await _sucreUnitOfWork.repoSucreParameterType.FindAsync(Id.GetValueOrDefault());
            if (parameterType == null) return NotFound(parameterType);
            //_parameterTypeDb.Remove(parameterType);
            //_parameterTypeDb.Save();
            _sucreUnitOfWork.repoSucreParameterType.Remove(parameterType);
            await _sucreUnitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
            //return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Delete")]
        //public async Task<IActionResult> DeletePost(int? Id)
        //{
        //    return await Task.Run(() => DeletePostNoAsync(Id));
        //}

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
