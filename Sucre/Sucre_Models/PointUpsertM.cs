using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_Models
{
    public class PointUpsertM
    {
        public PointM PointM { get; set; }
        public IEnumerable<SelectListItem> EnergySelectList { get; set; }
        public IEnumerable<SelectListItem> CexSelectList { get; set; }
    }
}
