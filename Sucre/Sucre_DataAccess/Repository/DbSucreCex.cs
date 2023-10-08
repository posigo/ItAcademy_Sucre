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
