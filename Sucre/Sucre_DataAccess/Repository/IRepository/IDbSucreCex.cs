using Sucre_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository.IRepository
{
    public interface IDbSucreCex: IDbSucre<Cex>
    {
        string FullName(Cex cex);
        void Update(Cex cex);
        Task UpdateAsync(Cex cex);
    }
}
