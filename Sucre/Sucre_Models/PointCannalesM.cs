using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_Models
{
    public class PointCannalesM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<CanalM> CannalesM { get; set; } = new HashSet<CanalM>();
        public IEnumerable<SelectListItem> FreeCanalesSelectList { get; set; }

        public PointCannalesM()
        {
            //PointM = new PointM();
            //CannalesM = HashSet<CanalM>();
        }
    }
}
