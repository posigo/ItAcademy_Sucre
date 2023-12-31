﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sucre_DataAccess.Entity
{
    /// <summary>
    /// АС и ПАЗ
    /// </summary>
    [Table("AsPazs")]
    public class AsPaz
    {
        [Key]
        public int Id { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal A_High { get; set; }
        public decimal W_High { get; set; }
        public decimal W_Low { get; set; }
        public decimal A_Low { get; set;}
        public bool A_HighEin { get; set; }
        public bool W_HighEin { get; set; }
        public bool W_LowEin { get; set; }
        public bool A_LowEin { get; set; }
        public bool A_HighType { get; set; }
        public bool W_HighType { get; set; }
        public bool W_LowType { get;set; }
        public bool A_LowType { get; set; }
        
        public int CanalId { get; set; }
        public virtual Canal? Canal { get; set; }
    }
}
