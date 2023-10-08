using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sucre_Models
{
    /// <summary>
    /// каналы учёта
    /// </summary>    
    public class CanalTableM
    {
        public CanalM canalM {  get; set; }
        
        public string? ParameterTypeName { get; set; }

        public CanalTableM()
        {
            canalM = new CanalM();
        }

    }
}
