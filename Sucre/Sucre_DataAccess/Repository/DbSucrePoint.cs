﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using Sucre_DataAccess.Data;
using Sucre_DataAccess.Entities;
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
                List<SelectListItem> returnValues= new List<SelectListItem>();
                
                foreach (var item in _db.Cexs)
                {
                    SelectListItem value = new SelectListItem();
                    //List<string> listText = new List<string>();                    
                    //if (item.Management !=null && item.Management.Trim() !="")
                    //    listText.Add(item.Management);
                    //if (item.CexName != null && item.CexName.Trim() != "")
                    //    listText.Add(item.CexName);
                    //if (item.Area != null && item.Area.Trim() != "")
                    //    listText.Add(item.Area);
                    //if (item.Device != null && item.Device.Trim() != "")
                    //    listText.Add(item.Device);
                    //if (item.Location != null && item.Location.Trim() != "")
                    //    listText.Add(item.Location);
                    //string textValue = String.Join("->", listText.ToArray());
                    //string textValue = GetStringCex(item);
                    string textValue = GetStringName(item);
                    value.Text = textValue;
                    value.Value = item.Id.ToString();
                    returnValues.Add(value);
                };
                return returnValues;
            }
            return null;
        }

        public string GetStringCex(Cex cex)
        {
            List<string> listText = new List<string>();
            if (cex.Management != null && cex.Management.Trim() != "")
                listText.Add(cex.Management);
            if (cex.CexName != null && cex.CexName.Trim() != "")
                listText.Add(cex.CexName);
            if (cex.Area != null && cex.Area.Trim() != "")
                listText.Add(cex.Area);
            if (cex.Device != null && cex.Device.Trim() != "")
                listText.Add(cex.Device);
            if (cex.Location != null && cex.Location.Trim() != "")
                listText.Add(cex.Location);
            return String.Join("->", listText.ToArray());
        }

        public string GetStringName(object obj)
        {
            Cex cex = (Cex)obj;
            List<string> listText = new List<string>();
            if (cex.Management != null && cex.Management.Trim() != "")
                listText.Add(cex.Management);
            if (cex.CexName != null && cex.CexName.Trim() != "")
                listText.Add(cex.CexName);
            if (cex.Area != null && cex.Area.Trim() != "")
                listText.Add(cex.Area);
            if (cex.Device != null && cex.Device.Trim() != "")
                listText.Add(cex.Device);
            if (cex.Location != null && cex.Location.Trim() != "")
                listText.Add(cex.Location);
            return String.Join("->", listText.ToArray());
            throw new NotImplementedException();
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
