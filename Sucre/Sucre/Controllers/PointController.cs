using LinqKit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Models;
using Sucre_Utility;
using System.Linq.Expressions;

namespace Sucre.Controllers
{
    public class PointController : Controller
    {
        private readonly IDbSucrePoint _pointDb;
        private readonly ISucreUnitOfWork _sukreUnitOfWork;

        public PointController(IDbSucrePoint pointDb, ISucreUnitOfWork sucreUnitOfWork)
        {
            _pointDb = pointDb;        
            _sukreUnitOfWork = sucreUnitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var pointsDb = _sukreUnitOfWork.repoSukrePoint.GetAll(includeProperties:"Energy,Cex");
            IEnumerable<PointTableM> pointTablesM = pointsDb.Select(u => new PointTableM
            {
                Id = u.Id,
                Name = u.Name,
                ServiceStaff = u.ServiceStaff,
                EnergyName = u.Energy.EnergyName,
                CexName = _pointDb.GetStringName(u.Cex)
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
                if (pointM.Id == 0)
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

        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            Point point = _pointDb.FirstOrDefault(filter: item => item.Id == Id.GetValueOrDefault(),includeProperties: "Energy,Cex");
            if (point == null) return NotFound(point);
            PointM pointM = new PointM();
            PointTableM pointTableM = new PointTableM()
            {
                Id = point.Id,
                Name = point.Name,
                Description = point.Description,
                EnergyName = point.Energy.EnergyName,
                //CexName = _pointDb.GetStringCex(point.Cex),
                CexName = _pointDb.GetStringName(point.Cex),
                ServiceStaff = point.ServiceStaff
            };
            //sp_Point(ref point, ref pointM, true);
            return View(pointTableM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? Id)
        {
            if (Id == null || Id == 0) return NotFound();
            Point point = _pointDb.Find(Id.GetValueOrDefault());
            if (point == null) return NotFound(point);
            _pointDb.Remove(point);
            _pointDb.Save();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> PointCannalesIndex(int? Id)
        {
            //var pointCanalesDb = _sukreUnitOfWork.repoSukrePoint.GetAll(filter: item => item.Id == Id.Value,
            //                                                         includeProperties: WC.CanalsName);
            if (Id == null || Id.Value == 0)
                return BadRequest("Id null or equal zero");

            Point pointCanalesDb = await _sukreUnitOfWork.repoSukrePoint.FirstOrDefaultAsync(
                                                                    filter: item => item.Id == Id.Value,
                                                                    includeProperties: WC.CanalsName);

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

            List<int> listIdCannales = new List<int>();
            ICollection<PointCannalesM> pointCannalessM = new HashSet<PointCannalesM>();
            
            PointCannalesM pointCannalesM = new PointCannalesM();
            pointCannalesM.Id = pointCanalesDb.Id;
            pointCannalesM.Name = pointCanalesDb.Name;
            pointCannalesM.CannalesM = new HashSet<CanalM>();
            foreach (var canal in pointCanalesDb.Canals)
            {
                CanalM canalM = new CanalM();
                canalM.Id = canal.Id;
                canalM.Name = canal.Name;
                pointCannalesM.CannalesM.Add(canalM);
                listIdCannales.Add(canalM.Id);
            }
            pointCannalessM.Add(pointCannalesM);
            Expression<Func<Canal, bool>> epFilter = null;
            IEnumerable<Canal> cannalesId;
            if (listIdCannales.Count == 0)
            {
                cannalesId = _sukreUnitOfWork.repoSukreCanal.GetAll(includeProperties: WC.ParameterTypeName,
                                                                isTracking: false);
            }
            else
            {
                bool begId = true;

                foreach (var id in listIdCannales)
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

                cannalesId = _sukreUnitOfWork.repoSukreCanal.GetAll(filter: epFilter, 
                                                                includeProperties: WC.ParameterTypeName,
                                                                isTracking: false);

            }


            List<SelectListItem> returnValues = new List<SelectListItem>();



            foreach (var item in cannalesId)
            {
                SelectListItem value = new SelectListItem();
                value.Text = $"{item.Id},{item.Name},{item.ParameterType.Mnemo},{item.ParameterType.UnitMeas}";
                value.Value = item.Id.ToString();
                
                returnValues.Add(value);
            };
            
            pointCannalesM.FreeCanalesSelectList = returnValues;
            


         
         
           
            //var dddd = pointCannalessM.ToList();
            //var ssss = $"{dddd[0].Id.ToString()}->{dddd[0].Name}->Count cannal: {dddd[0].CannalesM.Count().ToString()}"; 
            //return Ok("ssss");
            return View(pointCannalesM);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PointCannalesDelete(int Id, int IdCannale, PointCannalesM pointCannalesM)
        {
            //var id = Id;
            //var idc = IdCannale;            
            Point pointDb = await _sukreUnitOfWork.repoSukrePoint.FirstOrDefaultAsync(
                                                                    filter: item => item.Id == Id,
                                                                    includeProperties: WC.CanalsName);
            Canal canal = pointDb.Canals.FirstOrDefault(item => item.Id == IdCannale);
            pointDb.Canals.Remove(canal);
            _sukreUnitOfWork.Commit();
            
            return RedirectToAction(nameof(PointCannalesIndex), new { Id = Id });
            //return Ok("ssss");
            //return View(pointCannalesM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PointCannalesAdding(int Id, int Add, PointCannalesM pointCannalesM)
        {
            //var res = HttpContext;
            //var id = Id;
            //var idc = Add;
            int AddIdCannale = pointCannalesM.AddCannale;

            Point pointDb = await _sukreUnitOfWork.repoSukrePoint.FirstOrDefaultAsync(
                                                                    filter: item => item.Id == Id,
                                                                    includeProperties: WC.CanalsName);
            Canal addCannaleDb = await _sukreUnitOfWork.repoSukreCanal.FirstOrDefaultAsync(
                                                                    filter: item => item.Id == AddIdCannale,
                                                                    includeProperties: WC.ParameterTypeName);
            pointDb.Canals.Add(addCannaleDb);
            _sukreUnitOfWork.Commit();

            return RedirectToAction(nameof(PointCannalesIndex), new { Id = Id });

            //return Ok("ssss");
            //return View(pointCannalesM);
        }

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
