using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_DataAccess.Services.IServices
{
    public interface IUtilApplicattionDbContext
    {
        string DatabaseName {  get; }

        bool InitDbValue(out string errMsg);
    }
}
