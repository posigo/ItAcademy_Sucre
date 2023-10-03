    using Sucre_DataAccess.Data;
using Sucre_DataAccess.Entity;
using Sucre_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository
{
    public class DbSucreParameterType : DbSucre<ParameterType>, IDbSucreParameterType
    {
        private readonly ApplicationDbContext _db;

        public DbSucreParameterType(ApplicationDbContext db): base(db)
        {
            _db = db;
        }
        public void Update(ParameterType parameterType)
        {
            var objFromDb = base.FirstOrDefault(pt => pt.Id == parameterType.Id);
            if (objFromDb != null) 
            {
                objFromDb.Name = parameterType.Name;
                objFromDb.Mnemo = parameterType.Mnemo;
                objFromDb.UnitMeas = parameterType.UnitMeas;
            }
        }
    }
}
