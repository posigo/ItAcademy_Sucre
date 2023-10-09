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
    public class DbSucreEnergy : DbSucre<Energy>, IDbSucreEnergy
    {
        private readonly ApplicationDbContext _db;

        public DbSucreEnergy(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        public void Update(Energy energy)
        {
            _db.Update(energy);
        }
        public async Task UpdateAsync(Energy energy)
        {
            await Task.Run(() => Update(energy));
        }
    }
}
