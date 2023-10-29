using Microsoft.AspNetCore.Mvc.Rendering;
using Sucre_DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Repository.IRepository
{
    public interface IDbSucrePoint: IDbSucre<Point>
    {
        //IEnumerable<SelectListItem> GetAllDropdownList(string strInclude);
        //string GetStringCex(Cex cex);
        void Update(Point point);
    }
}
