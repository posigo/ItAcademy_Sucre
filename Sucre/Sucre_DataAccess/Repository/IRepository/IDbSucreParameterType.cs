using Sucre_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository.IRepository
{
    public interface IDbSucreParameterType: IDbSucre<ParameterType>
    {
        void Update(ParameterType parameterType);
        Task UpdateAsync(ParameterType parameterType);
    }
}
