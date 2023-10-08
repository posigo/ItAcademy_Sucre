using Microsoft.AspNetCore.Mvc.Rendering;
using Sucre_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository.IRepository
{
    public interface IDbSucreCanal: IDbSucre<Canal>, IDBSucreMethodList
    {
        //IEnumerable<SelectListItem> GetAllDropdownList(string strInclude);
        //string GetStringCanal(ParameterType parameterType);
        void Update(Canal canal);
        Task UpdateAsync(Canal canal);
    }
}
