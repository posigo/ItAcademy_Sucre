using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_Models
{
    public class ParameterTypeM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Mnemo { get; set; } = string.Empty;
        public string UnitMeas { get; set; } = string.Empty;

    }
}
