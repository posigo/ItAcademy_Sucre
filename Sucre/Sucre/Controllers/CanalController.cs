﻿using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualBasic;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Models;
using Sucre_Utility;
using System.Linq.Expressions;

namespace Sucre.Controllers
{
    public class CanalController : Controller
    {
        //private readonly IDbSucreCanal _canalDb;
        //private readonly IDbSucreAsPaz _asPazDb;
        private readonly ISucreUnitOfWork _sucreUnitOfWork;

        public CanalController(IDbSucreCanal canalDb, IDbSucreAsPaz asPazDb, ISucreUnitOfWork sucreUnitOfWork)
        {
            //_canalDb = canalDb;        
            //_asPazDb = asPazDb;
            _sucreUnitOfWork = sucreUnitOfWork;
        }

        [HttpGet]
        public  async Task<IActionResult> Index()
        {

            //var canalsDb = _canalDb.GetAll(includeProperties: $"{WC.ParameterTypeName},{WC.AsPazName}");
            //var canalsDb = _sucreUnitOfWork.repoSucreCanal.GetAll(includeProperties: $"{WC.ParameterTypeName},{WC.AsPazName}");
            var canalsDb = await _sucreUnitOfWork.repoSucreCanal.GetAllAsync(includeProperties: $"{WC.ParameterTypeName},{WC.AsPazName}");
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
                canalTableM.ParameterTypeName = _sucreUnitOfWork.repoSucreCanal.GetStringName(canal.ParameterType);

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
                ParametryTyoeSelectList = _sucreUnitOfWork.repoSucreCanal.GetAllDropdownList(WC.ParameterTypeName)
            };
            if (Id == null || Id.Value == 0)
            {
                return View(canalUpsertM);
            }
            else
            {
                //Canal canal = _canalDb.Find(Id.GetValueOrDefault());
                Canal canal = await _sucreUnitOfWork.repoSucreCanal.FindAsync(Id.GetValueOrDefault());
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
                    //_sucreUnitOfWork.repoSucreCanal.Add(canal);
                    await _sucreUnitOfWork.repoSucreCanal.AddAsync(canal);
                }
                else
                {
                    //Update
                    //canal = _canalDb.FirstOrDefault(filter: item => item.Id == canalM.Id, isTracking: false);
                    //canal = _sucreUnitOfWork.repoSucreCanal.FirstOrDefault(filter: item => item.Id == canalM.Id, 
                    //                                                    isTracking: false);
                    canal = await _sucreUnitOfWork.repoSucreCanal.FirstOrDefaultAsync(
                                                                            filter: item => item.Id == canalM.Id,
                                                                            isTracking: false);
                    if (canal == null)
                    {
                        return NotFound($"Channel with Id equal to {canalM.Id} not found");
                    }
                    else
                    {
                        sp_Canal(ref canal, ref canalM, false);
                        _sucreUnitOfWork.repoSucreCanal.Update(canal);
                    }
                }
                if (!canal.AsPazEin)
                {
                    //AsPaz asPaz = _sucreUnitOfWork.repoSucreAsPaz.FirstOrDefault(filter: item => item.CanalId == canal.Id);
                    AsPaz asPaz = await _sucreUnitOfWork.repoSucreAsPaz.FirstOrDefaultAsync(
                                                                                filter: item => item.CanalId == canal.Id);
                    if (asPaz != null)
                    {
                        _sucreUnitOfWork.repoSucreAsPaz.Remove(asPaz);
                        //_asPazDb.Save();
                    }
                }
                //_sucreUnitOfWork.Commit();
                await _sucreUnitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(canalM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null || Id == 0) return NotFound("The passed ID is empty or equal to zero");
            //Canal canal = _sucreUnitOfWork.repoSucreCanal.FirstOrDefault(
            //                                filter: item => item.Id == Id.GetValueOrDefault(), 
            //                                includeProperties: $"{WC.ParameterTypeName},{WC.AsPazName}");
            Canal canal = await _sucreUnitOfWork.repoSucreCanal.FirstOrDefaultAsync(
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
                ParameterTypeName = _sucreUnitOfWork.repoSucreCanal.GetStringName(canal.ParameterType)
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
            //Canal canal = _sucreUnitOfWork.repoSucreCanal.Find(Id.GetValueOrDefault());
            Canal cannale = await _sucreUnitOfWork.repoSucreCanal.FindAsync(Id.GetValueOrDefault());
            if (cannale == null)
                return NotFound($"Searching for an entry in the Channel entity with ID = {Id.GetValueOrDefault()} did not produce results");
            if (cannale.AsPazEin != null)
            {
                cannale.AsPaz = await _sucreUnitOfWork.repoSucreAsPaz.FirstOrDefaultAsync(item => item.CanalId == Id.GetValueOrDefault());
            }            
            _sucreUnitOfWork.repoSucreCanal.Remove(cannale);
            //_sucreUnitOfWork.Commit();
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

        #region АСиПаз
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
                AsPazsDb = (ICollection<AsPaz>)(await _sucreUnitOfWork.repoSucreAsPaz.GetAllAsync(includeProperties: $"{WC.CanalName}"));
                
            }            
            else
            {
                AsPazsDb = (ICollection<AsPaz>)(await _sucreUnitOfWork.repoSucreAsPaz.GetAllAsync(filter: item => item.CanalId == CanalId.GetValueOrDefault(),
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
                AsPaz asPaz = await _sucreUnitOfWork.repoSucreAsPaz.FirstOrDefaultAsync(filter: item => item.Id == Id.Value && 
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
        public async Task<IActionResult> UpsertAsPaz(AsPazCanalM asPazCanalM)
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

                    await _sucreUnitOfWork.repoSucreAsPaz.AddAsync(asPaz);
                }
                else
                {
                    //Update
                    //asPaz = _sucreUnitOfWork.repoSucreAsPaz.FirstOrDefault(filter: item => item.Id == asPazCanalM.AsPazM.Id && 
                    //                                                        item.CanalId == asPazCanalM.CanalId, isTracking: false);
                    asPaz = await _sucreUnitOfWork.repoSucreAsPaz.FirstOrDefaultAsync(
                                                                filter: item => item.Id == asPazCanalM.AsPazM.Id &&
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
                        _sucreUnitOfWork.repoSucreAsPaz.Update(asPaz);                        
                    }
                }
                //_sucreUnitOfWork.Commit();
                await _sucreUnitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }

            
            return View(asPazCanalM);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsPaz(int? Id)
        {
            if (Id == null || Id == 0) return BadRequest("Id AsPaz is empty or equal to zero");
            AsPaz asPaz = await _sucreUnitOfWork.repoSucreAsPaz.FirstOrDefaultAsync(filter: item => item.Id == Id.GetValueOrDefault(),
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
        public async Task<IActionResult> DeleteAsPazPost(int? Id)
        {
            if (Id == null || Id == 0) return NotFound("Id AsPaz is empty or equal to zero");
            AsPaz asPaz = await _sucreUnitOfWork.repoSucreAsPaz.FindAsync(Id.GetValueOrDefault());            
            if (asPaz == null) 
                return NotFound($"The AcPaz of a channel with ID equal to {Id.GetValueOrDefault()} was not found");

            int canalId = 0;
            if (asPaz.CanalId != null && asPaz.CanalId != 0)            
                canalId = asPaz.CanalId.GetValueOrDefault();            

            _sucreUnitOfWork.repoSucreAsPaz.Remove(asPaz);

            Canal canal = await _sucreUnitOfWork.repoSucreCanal.FindAsync(canalId);
            if (canal != null)
                canal.AsPazEin = false;

            _sucreUnitOfWork.repoSucreCanal.Update(canal);
            await _sucreUnitOfWork.CommitAsync();
                        
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
        #endregion

        #region Точки учёта
        [HttpGet]
        public async Task<IActionResult> CannalePointsIndex(int? Id)
        {
            //var pointCanalesDb = _sukreUnitOfWork.repoSukrePoint.GetAll(filter: item => item.Id == Id.Value,
            //                                                         includeProperties: WC.CanalsName);
            if (Id == null || Id.Value == 0)
                return BadRequest("Channel Id is null or equal to zero");

            Canal canalDb = await _sucreUnitOfWork.repoSucreCanal.FirstOrDefaultAsync(
                                            filter: item => item.Id == Id.Value,
                                            includeProperties: $"{WC.ParameterTypeName},{WC.PointsName}");
            #region dynamic predicate
            //var predicates = new List<Expression<Func<Canal, bool>>>();
            //predicates.Add(item => item.Id != 3);
            //predicates.Add(item => item.Id != 4);

            //Expression<Func<Canal, bool>> exprprd2 =  item => item.Id != 2;// && item.Id != 4;

            //exprprd2 = exprprd2.And(item => item.Id != 3);
            //exprprd2 = exprprd2.And(item => item.Id != 4);
            //exprprd2 = exprprd2.And(item => item.Id != 5);

            //prd3 = item => item.Id != 3;
            //prd3 = item => prd3(item) && item.Id !=4;
            //var parameter = Expression.Parameter(typeof(Func<Canal, bool>),"item");

            //Expression idProp = Expression.PropertyOrField(parameter, "Id");
            //Expression filter = Expression.NotEqual(idProp, Expression.Constant(3));

            //Expression idProp2 = Expression.PropertyOrField(parameter, "Id");
            //Expression filter2 = Expression.NotEqual(idProp2, Expression.Constant(4));
            //predicatre = Expression.AndAlso(predicatre, filter2);

            //Expression lamda = Expression.Lambda(predicatre, par
            //prd2 = Expression.Lambda<Func<Canal, bool>>(
            #endregion

            List<int> listIdPoints = new List<int>();
            
            ICollection<CannalePointsM> cannalePointssM = new HashSet<CannalePointsM>();

            CannalePointsM cannalePointsM = new CannalePointsM();
            cannalePointsM.Id = canalDb.Id;
            cannalePointsM.Name = canalDb.Name;
            cannalePointsM.PointsM = new HashSet<PointM>();
                        
            foreach (var point in canalDb.Points)
            {
                PointM pointM = new PointM();
                pointM.Id = point.Id;
                pointM.Name = point.Name;
                cannalePointsM.PointsM.Add(pointM);
                listIdPoints.Add(pointM.Id);                
            };
            
            Expression<Func<Point, bool>> epFilter = null;

            IEnumerable<Point> PointsId;
            if (listIdPoints.Count == 0)
            {
                //PointsId = _sucreUnitOfWork.repoSucrePoint.GetAll(
                //                                includeProperties: $"{WC.EnergyName},{WC.CexName}",
                //                                isTracking: false);
                PointsId = await _sucreUnitOfWork.repoSucrePoint.GetAllAsync(
                                                                   includeProperties: $"{WC.EnergyName},{WC.CexName}",
                                                                   isTracking: false);
            }
            else
            {
                bool begId = true;

                foreach (var id in listIdPoints)
                {
                    if (begId)
                    {
                        epFilter = item => item.Id != id;
                        begId = false;
                    }
                    else
                    {
                        epFilter = epFilter.And(item => item.Id != id);
                    }
                }

                //PointsId = _sucreUnitOfWork.repoSucrePoint.GetAll(filter: epFilter,
                //                                                includeProperties: $"{WC.EnergyName},{WC.CexName}",
                //                                                isTracking: false);
                PointsId = await _sucreUnitOfWork.repoSucrePoint.GetAllAsync(filter: epFilter,
                                                                includeProperties: $"{WC.EnergyName},{WC.CexName}",
                                                                isTracking: false);
            }


            List<SelectListItem> returnValues = new List<SelectListItem>();



            foreach (var item in PointsId)
            {
                SelectListItem value = new SelectListItem();
                string cexName=_sucreUnitOfWork.repoSucrePoint.GetStringName(item.Cex);
                value.Text = $"{item.Id},{item.Name},{item.Energy.EnergyName},{cexName}";
                value.Value = item.Id.ToString();

                returnValues.Add(value);
            };

            cannalePointsM.FreePointsSelectList = returnValues;
            
            return View(cannalePointsM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CannalePointsDelete(int Id, int IdPoint, CannalePointsM cannalePointsM)
        {
            //var id = Id;
            //var idc = IdCannale;            
            Canal cannaleDb = await _sucreUnitOfWork.repoSucreCanal.FirstOrDefaultAsync(
                                                    filter: item => item.Id == Id,
                                                    includeProperties: $"{WC.PointsName},{WC.ParameterTypeName}");
            Point point = cannaleDb.Points.FirstOrDefault(item => item.Id == IdPoint);
            cannaleDb.Points.Remove(point);
            _sucreUnitOfWork.CommitAsync();

            return RedirectToAction(nameof(CannalePointsIndex), new { Id = Id });
            //return Ok("ssss");
            //return View(pointCannalesM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CannalePointsAdding(int Id, int Add, CannalePointsM cannalePointsM)
        {
            if (cannalePointsM.AddPoint == 0) 
                return RedirectToAction(nameof(CannalePointsIndex), new { Id = Id });
            int AddIdPoint = cannalePointsM.AddPoint;
            
            Canal cannaleDb = _sucreUnitOfWork.repoSucreCanal.FirstOrDefault(
                                                        filter: item => item.Id == Id,
                                                        includeProperties: $"{WC.PointsName},{WC.ParameterTypeName}");
            Point addPointDb = _sucreUnitOfWork.repoSucrePoint.FirstOrDefault(
                                                filter: item => item.Id == AddIdPoint,
                                                includeProperties: $"{WC.EnergyName},{WC.CexName}");
            cannaleDb.Points.Add(addPointDb);
            await _sucreUnitOfWork.CommitAsync();

            return RedirectToAction(nameof(CannalePointsIndex), new { Id = Id });

            //return Ok("ssss");
            //return View(pointCannalesM);
        }
        #endregion

    }
}
