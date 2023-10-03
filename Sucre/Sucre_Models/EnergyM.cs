using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_Models
{
    public class EnergyM
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(15)]
        public string EnergyName { get; set; } = string.Empty;
    }
}
