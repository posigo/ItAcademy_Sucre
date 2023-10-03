using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_Models
{
    public class PointTableM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = string.Empty;
        
        public string? EnergyName { get; set; }
        public string? ServiceStaff { get; set; } = string.Empty;
    }
}
