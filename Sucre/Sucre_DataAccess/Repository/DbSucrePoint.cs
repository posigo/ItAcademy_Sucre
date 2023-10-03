using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using Sucre_DataAccess.Data;
using Sucre_DataAccess.Entity;
using Sucre_DataAccess.Repository.IRepository;
using Sucre_Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository
{
    public class DbSucrePoint : DbSucre<Point>, IDbSucrePoint
    {
        private readonly ApplicationDbContext _db;

        public DbSucrePoint(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetAllDropdownList(string strInclude)
        {
            if (strInclude == WC.EnergyName)
            {
                return _db.Energies.Select(item => new SelectListItem
                {
                    Text = item.EnergyName,
                    Value = item.Id.ToString()
                });
            }
            if (strInclude == WC.CexName)
            {
                //return _db.Cexs.Select(item => new SelectListItem
                //{
                //    Text = new StringBuilder()
                //        .Append((item.Management == null || item.Management.Trim() == "") ? " " : item.Management.Trim())                        
                //        .Append((item.CexName == null || item.CexName.Trim() == "") ? " " : item.CexName.Trim())                        
                //        .Append((item.Area == null || item.Area.Trim() == "") ? " " : item.Area.Trim())                        
                //        .Append((item.Device == null || item.Device.Trim() == "") ? " " : item.Device.Trim())                        
                //        .Append((item.Location == null || item.Location.Trim() == "") ? " " : item.Location.Trim())
                //        .ToString(),
                //    Value = item.Id.ToString()
                //});
                
                List<SelectListItem> returnValues= new List<SelectListItem>();
                
                foreach (var item in _db.Cexs)
                {
                    SelectListItem value = new SelectListItem();
                    List<string> listText = new List<string>();                    
                    if (item.Management !=null && item.Management.Trim() !="")
                        listText.Add(item.Management);
                    if (item.CexName != null && item.CexName.Trim() != "")
                        listText.Add(item.CexName);
                    if (item.Area != null && item.Area.Trim() != "")
                        listText.Add(item.Area);
                    if (item.Device != null && item.Device.Trim() != "")
                        listText.Add(item.Device);
                    if (item.Location != null && item.Location.Trim() != "")
                        listText.Add(item.Location);
                    string textValue = String.Join("->", listText.ToArray());
                    value.Text = textValue;
                    value.Value = item.Id.ToString();
                    returnValues.Add(value);
                };
                return returnValues;
            }
            return null;
        }
        public void Update(Point point)
        {
            var objFromDb = base.FirstOrDefault(pt => pt.Id == point.Id);
            if (objFromDb != null) 
            {
                objFromDb.Name = point.Name;
                objFromDb.Description = point.Description;
                objFromDb.EnergyId = point.EnergyId;
                objFromDb.CexId = point.CexId;
                objFromDb.ServiceStaff = point.ServiceStaff;
            }
        }
    }
}
