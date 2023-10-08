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
    public class DbSucreAsPaz : DbSucre<AsPaz>, IDbSucreAsPaz
    {
        private readonly ApplicationDbContext _db;

        public DbSucreAsPaz(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(AsPaz asPaz)
        {
            _db.Update(asPaz);
        }
        public async Task UpdateAsync(AsPaz asPaz)
        {
            await Task.Run(() => Update(asPaz));
        }
    }
}
