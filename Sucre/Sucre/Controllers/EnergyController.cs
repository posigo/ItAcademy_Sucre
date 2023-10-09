using Microsoft.AspNetCore.Mvc;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Models;

namespace Sucre.Controllers
{
    public class EnergyController : Controller
    {
        private readonly IDbSucreEnergy _energyDb;
        private readonly ISucreUnitOfWork _sucreUnitOfWork;

        public EnergyController(IDbSucreEnergy energyDb, ISucreUnitOfWork sucreUnitOfWork)
        {
            _energyDb = energyDb;
            _sucreUnitOfWork = sucreUnitOfWork;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var energiesDb = await _energyDb.GetAllAsync();
            var energiesDb = await _sucreUnitOfWork.repoSukreEnergy.GetAllAsync();
            IEnumerable<EnergyM> energiesM = energiesDb.Select(u => new EnergyM
            {
                Id = u.Id,
                EnergyName = u.EnergyName                
            });
            return View(energiesM);
        }
      
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    var energiesDb = _energyDb.GetAll();
        //    IEnumerable<EnergyM> energiesM = energiesDb.Select(u => new EnergyM
        //    {
        //        Id = u.Id,
        //        EnergyName = u.EnergyName
        //    });
        //    return View(energiesM);
        //}
       
        [HttpGet]
        public async Task<IActionResult> Upsert(int? Id)
        {
            EnergyM energyM = new EnergyM();
            if (Id == null)
            {
                return View(energyM);
            }
            else
            {
                //Energy energy = await _energyDb.FindAsync(Id.GetValueOrDefault());
                Energy energy = _energyDb.Find(Id.GetValueOrDefault());
                if (energy == null)
                {
                    return NotFound();
                }
                else
                {
                    //energyM.Id = energy.Id;
                    //energyM.EnergyName = energy.EnergyName;
                    sp_Energy(ref energy, ref energyM,true);
                    return View(energyM);
                }
            }
        }

        //[HttpGet]
        //public IActionResult Upsert(int? Id)
        //{
        //    EnergyM energyM = new EnergyM();
        //    if (Id == null)
        //    {
        //        return View(energyM);
        //    }
        //    else
        //    {
        //        Energy energy = _energyDb.Find(Id.GetValueOrDefault());
        //        if (energy == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            //energyM.Id = energy.Id;
        //            //energyM.EnergyName = energy.EnergyName;
        //            sp_Energy(ref energy, ref energyM, true);
        //            return View(energyM);
        //        }
        //    }
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(EnergyM energyM)
        {
            if (ModelState.IsValid)
            {
                Energy energy = new Energy();
                if (energyM.Id == 0)
                {
                    //Creating
                    //parameterType.Id = parameterTypeM.Id;
                    //parameterType.Name = parameterTypeM.Name;
                    //parameterType.Mnemo = parameterTypeM.Mnemo;
                    //parameterType.UnitMeas = parameterTypeM.UnitMeas;
                    sp_Energy(ref energy, ref energyM, false);
                    //await _energyDb.AddAsync(energy);                    
                    _energyDb.Add(energy);
                }
                else
                {
                    //Update
                    energy = await _energyDb.FirstOrDefaultAsync(filter: item => item.Id == energyM.Id, isTracking: false);
                    if (energy == null)
                    {
                        return NotFound(energy);
                    }
                    else
                    {
                        //parameterType.Id = parameterTypeM.Id;
                        //parameterType.Name = parameterTypeM.Name;
                        //parameterType.Mnemo = parameterTypeM.Mnemo;
                        //parameterType.UnitMeas = parameterTypeM.UnitMeas;
                        sp_Energy(ref energy, ref energyM, false);
                        _energyDb.Update(energy);                        
                    }
                }
                //_energyDb.SaveAsync();
                _energyDb.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(energyM);
        }

        //[HttpPost]
        //public IActionResult Upsert(EnergyM energyM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Energy energy = new Energy();
        //        if (energyM.Id == 0)
        //        {
        //            //Creating
        //            //parameterType.Id = parameterTypeM.Id;
        //            //parameterType.Name = parameterTypeM.Name;
        //            //parameterType.Mnemo = parameterTypeM.Mnemo;
        //            //parameterType.UnitMeas = parameterTypeM.UnitMeas;
        //            sp_Energy(ref energy, ref energyM, false);
        //            _energyDb.Add(energy);
        //        }
        //        else
        //        {
        //            //Update
        //            energy = _energyDb.FirstOrDefault(filter: item => item.Id == energyM.Id, isTracking: false);
        //            if (energy == null)
        //            {
        //                return NotFound(energy);
        //            }
        //            else
        //            {
        //                //parameterType.Id = parameterTypeM.Id;
        //                //parameterType.Name = parameterTypeM.Name;
        //                //parameterType.Mnemo = parameterTypeM.Mnemo;
        //                //parameterType.UnitMeas = parameterTypeM.UnitMeas;
        //                sp_Energy(ref energy, ref energyM, false);
        //                _energyDb.Update(energy);
        //            }
        //        }
        //        _energyDb.Save();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(energyM);
        //}

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            Energy energy = await _energyDb.FirstOrDefaultAsync(filter: item => item.Id == Id.GetValueOrDefault());
            if (energy == null) return NotFound(energy);
            EnergyM energyM = new EnergyM();
            sp_Energy(ref energy, ref energyM, true);
            return View(energyM);
        }

        //[HttpGet]
        //public IActionResult Delete(int? Id)
        //{
        //    if (Id == null || Id == 0) return NotFound();
        //    Energy energy = _energyDb.FirstOrDefault(filter: item => item.Id == Id.GetValueOrDefault());
        //    if (energy == null) return NotFound(energy);
        //    EnergyM energyM = new EnergyM();
        //    sp_Energy(ref energy, ref energyM, true);
        //    return View(energyM);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            var energy = await _energyDb.FindAsync(Id.GetValueOrDefault());
            if (energy == null) return NotFound(energy);
            _energyDb.Remove(energy);
            //_energyDb.SaveAsync();
            _energyDb.Save();
            return RedirectToAction(nameof(Index));            
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[ActionName("Delete")]
        //public IActionResult DeletePost(int? Id)
        //{
        //    if (Id == null || Id == 0) return NotFound();
        //    var energy = _energyDb.Find(Id.GetValueOrDefault());
        //    if (energy == null) return NotFound(energy);
        //    _energyDb.Remove(energy);
        //    _energyDb.Save();
        //    return RedirectToAction(nameof(Index));
        //}

        /// <summary>
        /// Синхронизация между моделью и сущностью
        /// </summary>
        /// <param name="energy">сущность</param>
        /// <param name="energyM">модель</param>
        /// <param name="md">направление: 0 в сущность, 1 в модель </param>
        [NonAction]
        private void sp_Energy(ref Energy energy, ref EnergyM energyM, bool md = false)
        {
            //From model in entity
            if (!md)
            {
                energy.Id = energyM.Id;
                energy.EnergyName = energyM.EnergyName;                
            }
            else //From entity in model
            {
                energyM.Id = energy.Id;
                energyM.EnergyName = energy.EnergyName;                
            }
        }
    }
}
