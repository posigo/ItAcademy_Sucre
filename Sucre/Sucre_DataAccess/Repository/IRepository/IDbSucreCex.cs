using Sucre_DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository.IRepository
{
    public interface IDbSucreCex: IDbSucre<Cex>
    {
        void Update(Cex cex);
    }
}
