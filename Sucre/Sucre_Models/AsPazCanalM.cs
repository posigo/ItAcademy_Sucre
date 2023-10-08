using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sucre_Models
{
    /// <summary>
    /// АС и ПАЗ
    /// </summary>    
    public class AsPazCanalM
    {
        public AsPazM AsPazM { get; set; }
        public int CanalId { get; set; }
        public string CanalName { get; set; }
        
    }
}
