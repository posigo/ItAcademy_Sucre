using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_Models
{
    public class CanalUpsertM
    {
        public CanalM CanalM { get; set; }
        public IEnumerable<SelectListItem> ParametryTyoeSelectList { get; set; }
        public CanalUpsertM()
        {
                this.CanalM = new CanalM();
        }
    }
}
