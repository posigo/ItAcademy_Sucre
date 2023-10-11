using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualBasic;
using Sucre_DataAccess.Entities;
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
        public  async Task<IActionResult> Index()
        {

            //var canalsDb = _canalDb.GetAll(includeProperties: $"{WC.ParameterTypeName},{WC.AsPazName}");
            var canalsDb = _sucreUnitOfWork.repoSukreCanal.GetAll(includeProperties: $"{WC.ParameterTypeName},{WC.AsPazName}");
            ICollection<CanalTableM> canalTableMs = new HashSet<CanalTableM>();
            
            foreach (var canal in canalsDb) 
            { 
                CanalTableM canalTableM = new CanalTableM();
                //Canal ccanal = (Canal)canal;
                //CanalM canalM = new CanalM();
                //sp_Canal(ref ccanal, ref canalM, true);
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
                        A_LowType = canal.AsPaz.A_LowType,

                        CanalId = canalM.Id
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
        public async Task<IActionResult> Upsert(int? Id)
        {
            CanalUpsertM canalUpsertM = new CanalUpsertM()
            {
                CanalM = new CanalM(),
                //ParametryTyoeSelectList = _canalDb.GetAllDropdownList(WC.ParameterTypeName)
                ParametryTyoeSelectList = _sucreUnitOfWork.repoSukreCanal.GetAllDropdownList(WC.ParameterTypeName)
            };
            if (Id == null || Id.Value == 0)
            {
                return View(canalUpsertM);
            }
            else
            {
                //Canal canal = _canalDb.Find(Id.GetValueOrDefault());
                Canal canal = await _sucreUnitOfWork.repoSukreCanal.FindAsync(Id.GetValueOrDefault());
                if (canal == null)
                {
                    return NotFound($"Channel with Id equal to {Id.GetValueOrDefault()} not found");
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(CanalM canalM)
        {
            if (ModelState.IsValid)
            {
                Canal canal = new Canal();
                if (canalM.Id == 0)
                {
                    //Creating                    
                    sp_Canal(ref canal, ref canalM, false);
                    _sucreUnitOfWork.repoSukreCanal.Add(canal);
                }
                else
                {
                    //Update
                    //canal = _canalDb.FirstOrDefault(filter: item => item.Id == canalM.Id, isTracking: false);
                    canal = _sucreUnitOfWork.repoSukreCanal.FirstOrDefault(filter: item => item.Id == canalM.Id, 
                                                                        isTracking: false);
                    if (canal == null)
                    {
                        return NotFound($"Channel with Id equal to {canalM.Id} not found");
                    }
                    else
                    {
                        sp_Canal(ref canal, ref canalM, false);
                        _sucreUnitOfWork.repoSukreCanal.Update(canal);
                    }
                }
                if (!canal.AsPazEin)
                {
                    AsPaz asPaz = _sucreUnitOfWork.repoSukreAsPaz.FirstOrDefault(filter: item => item.CanalId == canal.Id);
                    if (asPaz != null)
                    {
                        _sucreUnitOfWork.repoSukreAsPaz.Remove(asPaz);
                        //_asPazDb.Save();
                    }
                }
                _sucreUnitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            return View(canalM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null || Id == 0) return NotFound("The passed ID is empty or equal to zero");
            Canal canal = _sucreUnitOfWork.repoSukreCanal.FirstOrDefault(
                                            filter: item => item.Id == Id.GetValueOrDefault(), 
                                            includeProperties: $"{WC.ParameterTypeName},{WC.AsPazName}");
            if (canal == null) 
                return NotFound($"Searching for an entry in the Channel entity with ID = {Id.GetValueOrDefault()} did not produce results");
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
                    A_LowType = canal.AsPaz.A_LowType,

                    CanalId = canal.Id
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
        public async Task<IActionResult> DeletePost(int? Id)
        {
            if (Id == null || Id == 0) 
                return NotFound("The passed ID is empty or equal to zero");
            Canal canal = _sucreUnitOfWork.repoSukreCanal.Find(Id.GetValueOrDefault());
            if (canal == null) 
                return NotFound($"Searching for an entry in the Channel entity with ID = {Id.GetValueOrDefault()} did not produce results");
            _sucreUnitOfWork.repoSukreCanal.Remove(canal);
            _sucreUnitOfWork.Commit();
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
        public async Task<IActionResult> IndexAsPaz(int? CanalId)
        {

            //var AsPazsDb = _asPazDb.GetAll(includeProperties: $"{WC.CanalName}");
            ICollection<AsPazCanalM> asPazCanalMs = new HashSet<AsPazCanalM>();

            ICollection<AsPaz> AsPazsDb = new HashSet<AsPaz>();
            if (CanalId == null || CanalId == 0)
            {
                AsPazsDb = (ICollection<AsPaz>)(await _sucreUnitOfWork.repoSukreAsPaz.GetAllAsync(includeProperties: $"{WC.CanalName}"));
                
            }            
            else
            {
                AsPazsDb = (ICollection<AsPaz>)(await _sucreUnitOfWork.repoSukreAsPaz.GetAllAsync(filter: item => item.CanalId == CanalId.GetValueOrDefault(),
                                                                                                includeProperties: $"{WC.CanalName}"));
            }

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
                return NotFound("Channel ID is empty or zero");
            }
            if (canalName == null) canalName = string.Empty;
            AsPazCanalM asPazCanalM = new AsPazCanalM()
            {
                AsPazM = new AsPazM()
                { CanalId = canalId.GetValueOrDefault()},
                CanalId = canalId.GetValueOrDefault(),
                CanalName = canalName
            };
            if (Id == null || Id.Value == 0)
            {
                return View(asPazCanalM);
              
            }
            else
            {
                AsPaz asPaz = await _sucreUnitOfWork.repoSukreAsPaz.FirstOrDefaultAsync(filter: item => item.Id == Id.Value && 
                                                                                    item.CanalId == canalId.Value);
                if (asPaz == null)
                {
                    return NotFound($"The AsPaz of a channel with Id equal to {canalId} was not found");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpsertAsPaz(AsPazCanalM asPazCanalM)
        {
            if (asPazCanalM.CanalId == null && asPazCanalM.CanalId == 0 &&
                asPazCanalM.AsPazM.CanalId == null && asPazCanalM.AsPazM.CanalId == 0)
                return BadRequest("Channel ID is empty or zero");
            if (ModelState.IsValid)
            {
                AsPaz asPaz = new AsPaz();
                if (asPazCanalM.AsPazM.Id == 0)
                {
                    //Creating                    
                    AsPazM asPazM = asPazCanalM.AsPazM;
                    asPazM.CanalId = asPazCanalM.CanalId;
                    //asPazM = asPazCanalM.AsPazM;
                    sp_AsPaz(ref asPaz, ref asPazM, false);

                    _sucreUnitOfWork.repoSukreAsPaz.Add(asPaz);
                }
                else
                {
                    //Update
                    asPaz = _sucreUnitOfWork.repoSukreAsPaz.FirstOrDefault(filter: item => item.Id == asPazCanalM.AsPazM.Id && 
                                                                            item.CanalId == asPazCanalM.CanalId, isTracking: false);
                    if (asPaz == null)
                    {
                        return NotFound($"The AsPaz of a channel with Id equal to {asPazCanalM.CanalId} was not found");
                    }
                    else
                    {
                        AsPazM asPazM = asPazCanalM.AsPazM;
                        asPazM.CanalId = asPazCanalM.CanalId;
                        sp_AsPaz(ref asPaz, ref asPazM, false);
                        _sucreUnitOfWork.repoSukreAsPaz.Update(asPaz);
                    }
                }
                //_sucreUnitOfWork.Commit();
                _sucreUnitOfWork.repoSukreAsPaz.Save();
                return RedirectToAction(nameof(Index));
            }

            
            return View(asPazCanalM);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsPaz(int? Id)
        {
            if (Id == null || Id == 0) return BadRequest("Id AsPaz is empty or equal to zero");
            AsPaz asPaz = await _sucreUnitOfWork.repoSukreAsPaz.FirstOrDefaultAsync(filter: item => item.Id == Id.GetValueOrDefault(),
                                                                            includeProperties: $"{WC.CanalName}");            
            if (asPaz == null) return NotFound(new String($"The AcPaz of a channel with ID equal to {Id.GetValueOrDefault()} was not found"));
            AsPazCanalM asPazCanalM = new AsPazCanalM();
            AsPazM asPazM = new AsPazM();
            sp_AsPaz(ref asPaz, ref asPazM, true);
            asPazCanalM.AsPazM = asPazM;
            asPazCanalM.CanalId = asPaz.CanalId.Value;
            asPazCanalM.CanalName = asPaz.Canal.Name;
            
            return View(asPazCanalM);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteAsPaz")]
        public IActionResult DeleteAsPazPost(int? Id)
        {
            if (Id == null || Id == 0) return NotFound("Id AsPaz is empty or equal to zero");
            AsPaz asPaz = _sucreUnitOfWork.repoSukreAsPaz.Find(Id.GetValueOrDefault());            
            if (asPaz == null) 
                return NotFound($"The AcPaz of a channel with ID equal to {Id.GetValueOrDefault()} was not found");

            int canalId = 0;
            if (asPaz.CanalId != null && asPaz.CanalId != 0)            
                canalId = asPaz.CanalId.GetValueOrDefault();            

            _sucreUnitOfWork.repoSukreAsPaz.Remove(asPaz);

            Canal canal = _sucreUnitOfWork.repoSukreCanal.Find(canalId);
            if (canal != null)
                canal.AsPazEin = false;

            _sucreUnitOfWork.repoSukreCanal.Update(canal);
            _sucreUnitOfWork.Commit();
                        
            return RedirectToAction(nameof(IndexAsPaz));
        }

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
                asPaz.A_HighEin = asPazM.A_HighEin;
                asPaz.W_HighEin = asPazM.W_HighEin;
                asPaz.W_LowEin = asPazM.W_LowEin;
                asPaz.A_LowEin = asPazM.A_LowEin;
                asPaz.A_HighType = asPazM.A_HighType;
                asPaz.W_HighType = asPazM.W_HighType;
                asPaz.W_LowType = asPazM.W_LowType;
                asPaz.A_LowType = asPazM.A_LowType;
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
