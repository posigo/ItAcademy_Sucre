using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_Models
{
    public class PointM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(255)]
        public string? Description { get; set; } = string.Empty;
        [Required]
        public int EnergyId { get; set; }
        [Required]
        public int CexId { get; set; }
        [MaxLength(20)]
        public string? ServiceStaff { get; set; } = string.Empty;
    }
}
