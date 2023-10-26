using Microsoft.AspNetCore.Mvc.Rendering;
using Sucre_DataAccess.Data;
using Sucre_DataAccess.Entities;
using Sucre_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository
{
    public class DbSucreCex : DbSucre<Cex>, IDbSucreCex
    {
        private readonly ApplicationDbContext _db;

        public DbSucreCex(ApplicationDbContext db): base(db)
        {
            _db = db;
        }

        public bool CheckAllFieldsEmpty (Cex cex)
        {
            if ((cex.Management.Trim() == string.Empty || cex.Management.Trim() == "") &&
                (cex.CexName.Trim() == string.Empty || cex.CexName.Trim() == "") &&
                (cex.Area.Trim() == string.Empty || cex.Area.Trim() == "") &&
                (cex.Device.Trim() == string.Empty || cex.Device.Trim() == "") &&
                (cex.Location.Trim() == string.Empty || cex.Location.Trim() == ""))
            { 
                return true;
            }
            return false;
        }

        public string FullName (Cex cex)
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
        public IEnumerable<SelectListItem> GetAllDropdownList(string strInclude)
        {
            throw new NotImplementedException();
        }

        public string GetStringName(object obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Cex cex)
        {
            var objFromDb = base.FirstOrDefault(item => item.Id == cex.Id);
            if (objFromDb != null) 
            {
                objFromDb.Area = cex.Area;
                objFromDb.Device = cex.Device;
                objFromDb.CexName = cex.CexName;
                objFromDb.Location = cex.Location;
                objFromDb.Management = cex.Management;


            }
        }
        public async Task UpdateAsync(Cex cex)
        {
            await Task.Run(() => Update(cex));
        }
    }
}
