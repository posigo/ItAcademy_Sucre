﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sucre_Models
{
    public class CexM
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Управление
        /// </summary>
        [MaxLength(35)]        
        public string? Management { get; set; } = string.Empty;
        /// <summary>
        /// цех
        /// </summary>
        [MaxLength(35)]
        public string? CexName { get; set; } = string.Empty;
        /// <summary>
        /// участок
        /// </summary>
        [MaxLength(50)]
        public string? Area { get; set; } = string.Empty;
        /// <summary>
        /// устанорвка
        /// </summary>
        [MaxLength(50)]
        public string? Device { get; set; } = string.Empty;
        /// <summary>
        /// локация
        /// </summary>
        [MaxLength(70)]
        public string? Location { get; set; } = string.Empty;
    }
}
