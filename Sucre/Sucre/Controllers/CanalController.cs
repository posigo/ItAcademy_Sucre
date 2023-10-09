using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Models;
using Sucre_Utility;

namespace Sucre.Controllers
{
    public class CanalController : Controller
    {
        private readonly IDbSucreCanal _canalDb;
        private readonly IDbSucreAsPaz _asPazDb;
        private readonly ISucreUnitOfWork _sucreUnitOfWork;

        public CanalController(IDbSucreCanal canalDb, IDbSucreAsPaz asPazDb, ISucreUnitOfWork sucreUnitOfWork)
        {
            _canalDb = canalDb;        
            _asPazDb = asPazDb;
            _sucreUnitOfWork = sucreUnitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var canalsDb = _canalDb.GetAll(includeProperties: $"{WC.ParameterTypeName},{WC.AsPazName}");
            ICollection<CanalTableM> canalTableMs = new HashSet<CanalTableM>();
            
            foreach (var canal in canalsDb) 
            { 
                CanalTableM canalTableM = new CanalTableM();

                CanalM canalM = new CanalM()
                {
                    Id = canal.Id,
                    Name = canal.Name,
                    Description = canal.Description,
                    ParameterTypeId = canal.ParameterTypeId,
                    Reader = canal.Reader,
                    SourceType = canal.SourceType,
                    AsPazEin = canal.AsPazEin,
                };
                if (canalM.AsPazEin && canal.AsPaz!=null)
                {
                    AsPazM asPazM = new AsPazM
                    {
                        Id = canal.AsPaz.Id,
                        High = canal.AsPaz.High,
                        Low = canal.AsPaz.Low,

                        A_High = canal.AsPaz.A_High,
                        W_High = canal.AsPaz.W_High,
                        W_Low = canal.AsPaz.W_Low,
                        A_Low = canal.AsPaz.A_Low,

                        A_HighEin = canal.AsPaz.A_HighEin,
                        W_HighEin = canal.AsPaz.W_HighEin,
                        W_LowEin = canal.AsPaz.W_LowEin,
                        A_LowEin = canal.AsPaz.A_LowEin,

                        A_HighType = canal.AsPaz.A_HighType,
                        W_HighType = canal.AsPaz.W_HighType,
                        W_LowType = canal.AsPaz.W_LowType,
                        A_LowType = canal.AsPaz.A_LowType
                    };
                    canalM.AsPazM = asPazM;                    
                };
                if (canal.Points.Count!=0)
                {
                    canalM.PointMs =(ICollection<PointM>)canal.Points;                    
                }
                canalTableM.canalM = canalM;
                canalTableM.ParameterTypeName = _canalDb.GetStringName(canal.ParameterType);

                canalTableMs.Add(canalTableM);
            }            
            return View(canalTableMs);
        }

        [HttpGet]
        public IActionResult Upsert(int? Id)
        {
            CanalUpsertM canalUpsertM = new CanalUpsertM()
            {
                CanalM = new CanalM(),
                ParametryTyoeSelectList = _canalDb.GetAllDropdownList(WC.ParameterTypeName)
            };
            if (Id == null)
            {
                return View(canalUpsertM);
            }
            else
            {
                Canal canal = _canalDb.Find(Id.GetValueOrDefault());
                if (canal == null)
                {
                    return NotFound(canal);
                }
                else
                {
                    CanalM canalM = new CanalM();
                    sp_Canal(ref canal, ref canalM, true);
                    canalUpsertM.CanalM = canalM;
                    return View(canalUpsertM);
                }
            }
        }

        [HttpPost]
        public IActionResult Upsert(CanalM canalM)
        {
            if (ModelState.IsValid)
            {
                Canal canal = new Canal();
                if (canalM.Id == 0)
                {
                    //Creating                    
                    sp_Canal(ref canal, ref canalM, false);
                    _canalDb.Add(canal);
                }
                else
                {
                    //Update
                    canal = _canalDb.FirstOrDefault(filter: item => item.Id == canalM.Id, isTracking: false);
                    if (canal == null)
                    {
                        return NotFound(canal);
                    }
                    else
                    {
                        sp_Canal(ref canal, ref canalM, false);
                        _canalDb.Update(canal);
                    }
                }
                if (!canal.AsPazEin)
                {
                    AsPaz asPaz = _asPazDb.FirstOrDefault(filter: item => item.CanalId == canal.Id);
                    if (asPaz != null)
                    {
                        _asPazDb.Remove(asPaz);
                        _asPazDb.Save();
                    }
                }
                _canalDb.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(canalM);
        }

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0) return NotFound(new String("The passed ID is empty or equal to zero").ToString());
            Canal canal = _canalDb.FirstOrDefault(filter: item => item.Id == Id.GetValueOrDefault(), 
                                            includeProperties: $"{WC.ParameterTypeName},{WC.AsPazName}");
            if (canal == null) return NotFound(new String($"Searching for an entry in the Channel entity with ID = {Id.GetValueOrDefault()} did not produce results"));
            CanalM canalM = new CanalM();
            sp_Canal(ref canal, ref canalM, true);
            if (canal.AsPaz == null)
            {
                canalM.AsPazM = null;
            }
            else
            {
                canalM.AsPazM = new AsPazM()
                {
                    Id = canal.AsPaz.Id,
                    High = canal.AsPaz.High,
                    Low = canal.AsPaz.Low,

                    A_High = canal.AsPaz.A_High,
                    W_High = canal.AsPaz.W_High,
                    W_Low = canal.AsPaz.W_Low,
                    A_Low = canal.AsPaz.A_Low,

                    A_HighEin = canal.AsPaz.A_HighEin,
                    W_HighEin = canal.AsPaz.W_HighEin,
                    W_LowEin = canal.AsPaz.W_LowEin,
                    A_LowEin = canal.AsPaz.A_LowEin,

                    A_HighType = canal.AsPaz.A_HighType,
                    W_HighType = canal.AsPaz.W_HighType,
                    W_LowType = canal.AsPaz.W_LowType,
                    A_LowType = canal.AsPaz.A_LowType
                };
            }
            CanalTableM canalTableM = new CanalTableM()
            {
                canalM = canalM,
                ParameterTypeName = _canalDb.GetStringName(canal.ParameterType)
            };
            
            return View(canalTableM);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            if (Id == null || Id == 0) return NotFound(new String("The passed ID is empty or equal to zero").ToString());
            Canal canal = _canalDb.Find(Id.GetValueOrDefault());
            if (canal == null) return NotFound(new String($"Searching for an entry in the Channel entity with ID = {Id.GetValueOrDefault()} did not produce results"));
            _canalDb.Remove(canal);
            _canalDb.Save();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Синхронизация между моделью и сущностью
        /// </summary>
        /// <param name="cex">сущность</param>
        /// <param name="cexM">модель</param>
        /// <param name="md">направление: 0 в сущность, 1 в модель </param>
        [NonAction]
        private void sp_Canal(ref Canal canal, ref CanalM canalM, bool md = false)
        {
            //From model in entity
            if (!md)
            {
                canal.Id = canalM.Id;
                canal.Name = canalM.Name;
                canal.Description = canalM.Description;
                canal.ParameterTypeId = canalM.ParameterTypeId;
                canal.Reader = canalM.Reader;
                canal.SourceType = canalM.SourceType;
                canal.AsPazEin = canalM.AsPazEin;
            }
            else //MFrom entity in model
            {
                canalM.Id = canal.Id;
                canalM.Name = canal.Name;
                canalM.Description = canal.Description;
                canalM.ParameterTypeId = canal.ParameterTypeId;
                canalM.Reader = canal.Reader;
                canalM.SourceType = canal.SourceType;
                canalM.AsPazEin = canal.AsPazEin; ;
            }
        }

        //public IActionResult IndexAsPaz()
        //{
        //    var AsPazsDb = _asPazDb.GetAll(includeProperties: $"{WC.CanalName}");
        //    ICollection<AsPazCanalM> asPazCanalMs = new HashSet<AsPazCanalM>();

        //    foreach (var aspaz in AsPazsDb)
        //    {
        //        AsPazCanalM asPazCanalM = new AsPazCanalM(); 
        //        AsPazM asPazM = new AsPazM();
        //        AsPaz ggg = (AsPaz)aspaz;
        //        sp_AsPaz(ref ggg, ref asPazM, true); 

        //        asPazCanalM.AsPazM = asPazM;
        //        asPazCanalM.CanalId = aspaz.Canal.Id;
        //        asPazCanalM.CanalName = aspaz.Canal.Name;

        //        asPazCanalMs.Add(asPazCanalM);

        //    }
        //    return View(asPazCanalMs);            
        //}

        [HttpGet]
        public async Task<IActionResult> IndexAsPaz()
        {
            //var AsPazsDb = _asPazDb.GetAll(includeProperties: $"{WC.CanalName}");
            var AsPazsDb = await _sucreUnitOfWork.repoSukreAsPaz.GetAllAsync(includeProperties: $"{WC.CanalName}");
            ICollection<AsPazCanalM> asPazCanalMs = new HashSet<AsPazCanalM>();

            foreach (var aspaz in AsPazsDb)
            {
                AsPazCanalM asPazCanalM = new AsPazCanalM();
                AsPazM asPazM = new AsPazM();
                AsPaz ggg = (AsPaz)aspaz;
                sp_AsPaz(ref ggg, ref asPazM, true);

                asPazCanalM.AsPazM = asPazM;
                asPazCanalM.CanalId = aspaz.Canal.Id;
                asPazCanalM.CanalName = aspaz.Canal.Name;

                asPazCanalMs.Add(asPazCanalM);

            }
            return View(asPazCanalMs);
        }

        [HttpGet]
        public async Task<IActionResult> UpsertAsPaz(int? Id, int? canalId, string? canalName)
        {
            if (canalId == null || canalId == 0)
            {
                return NotFound("Non canal Id");
            }
            if (canalName == null) canalName = string.Empty;
            AsPazCanalM asPazCanalM = new AsPazCanalM()
            {
                AsPazM = new AsPazM()
                { CanalId = canalId.GetValueOrDefault()},
                CanalId = canalId.GetValueOrDefault(),
                CanalName = canalName
            };
            if (Id == null)
            {
                return View(asPazCanalM);
            }
            else
            {
                AsPaz asPaz = await _sucreUnitOfWork.repoSukreAsPaz.FirstOrDefaultAsync(filter: item => item.Id == Id.Value && item.CanalId == canalId.Value);
                if (asPaz == null)
                {
                    return NotFound("Not found aspaz");
                }
                else
                {
                    AsPazM asPazM = new AsPazM();
                    
                    sp_AsPaz(ref asPaz, ref asPazM, true);
                    asPazCanalM.AsPazM = asPazM;
                   
                    return View(asPazCanalM);
                }
            }
        }

        //[HttpPost]
        //public IActionResult Upsert(CanalM canalM)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Canal canal = new Canal();
        //        if (canalM.Id == 0)
        //        {
        //            //Creating                    
        //            sp_Canal(ref canal, ref canalM, false);
        //            _canalDb.Add(canal);
        //        }
        //        else
        //        {
        //            //Update
        //            canal = _canalDb.FirstOrDefault(filter: item => item.Id == canalM.Id, isTracking: false);
        //            if (canal == null)
        //            {
        //                return NotFound(canal);
        //            }
        //            else
        //            {
        //                sp_Canal(ref canal, ref canalM, false);
        //                _canalDb.Update(canal);
        //            }
        //        }
        //        if (!canal.AsPazEin)
        //        {
        //            AsPaz asPaz = _asPazDb.FirstOrDefault(filter: item => item.CanalId == canal.Id);
        //            if (asPaz != null)
        //            {
        //                _asPazDb.Remove(asPaz);
        //                _asPazDb.Save();
        //            }
        //        }
        //        _canalDb.Save();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(canalM);
        //}


        /// <summary>
        /// Синхронизация между моделью и сущностью
        /// </summary>
        /// <param name="asPaz">сущность</param>
        /// <param name="asPazM">модель</param>
        /// <param name="md">направление: 0 в сущность, 1 в модель </param>
        [NonAction]
        private void sp_AsPaz(ref AsPaz asPaz, ref AsPazM asPazM, bool md = false)
        {
            //From model in entity
            if (!md)
            {
                asPaz.Id = asPazM.Id;
                asPaz.High = asPazM.High;
                asPaz.Low = asPazM.Low;
                asPaz.A_High = asPazM.A_High;
                asPaz.W_High = asPazM.W_High;
                asPaz.W_Low = asPazM.W_Low;
                asPaz.A_Low = asPazM.A_Low;
                asPaz.A_HighEin = asPazM.A_HighEin.Value;
                asPaz.W_HighEin = asPazM.W_HighEin.Value;
                asPaz.W_LowEin = asPazM.W_LowEin.Value;
                asPaz.A_LowEin = asPazM.A_LowEin.Value;
                asPaz.A_HighType = asPazM.A_HighType.Value;
                asPaz.W_HighType = asPazM.W_HighType.Value;
                asPaz.W_LowType = asPazM.W_LowType.Value;
                asPaz.A_LowType = asPazM.A_LowType.Value;
                asPaz.CanalId = asPazM.CanalId;
            }
            else //MFrom entity in model
            {
                asPazM.Id = asPaz.Id;
                asPazM.High = asPaz.High;
                asPazM.Low = asPaz.Low;
                asPazM.A_High = asPaz.A_High;
                asPazM.W_High = asPaz.W_High;
                asPazM.W_Low = asPaz.W_Low;
                asPazM.A_Low = asPaz.A_Low;
                asPazM.A_HighEin = asPaz.A_HighEin;
                asPazM.W_HighEin = asPaz.W_HighEin;
                asPazM.W_LowEin = asPaz.W_LowEin;
                asPazM.A_LowEin = asPaz.A_LowEin;
                asPazM.A_HighType = asPaz.A_HighType;
                asPazM.W_HighType = asPaz.W_HighType;
                asPazM.W_LowType = asPaz.W_LowType;
                asPazM.A_LowType = asPaz.A_LowType;
                asPazM.CanalId = asPaz.CanalId.Value;
            }
        }

    }
}
