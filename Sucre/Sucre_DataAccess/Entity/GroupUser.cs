//using Microsoft.Analytics.Interfaces;
//using Microsoft.Analytics.Types.Sql;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sucre_DataAccess.Entity
{
    /// <summary>
    /// Группа user
    /// </summary>
    [Table("GroupUsers")]
    public class GroupUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;

        public virtual ICollection<User> Users { get; set;}
        public virtual ICollection<Report> Reports { get; set;}

        public GroupUser()
        {
            this.Users = new HashSet<User>();
            this.Reports = new HashSet<Report>();
        }

    }
}