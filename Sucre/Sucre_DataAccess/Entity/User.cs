using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sucre_DataAccess.Entity
{
    /// <summary>
    /// User
    /// </summary>
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(15)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(255)]
        public string Description { get; set; } = string.Empty;
        [Required]
        public int GroupId { get; set; }
        [MaxLength(15)]
        public string Password { get; set; } = string.Empty;

        [ForeignKey("GroupId")]
        public virtual GroupUser GroupUser { get; set; }
    }
}
