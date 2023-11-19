using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sucre_DataAccess.Entities
{
    /// <summary>
    /// User
    /// </summary>
    [Table("AppUsers")]
    public class AppUser
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string? Name { get; set; }
        [MaxLength(255)]
        public string? Description { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        public int GroupId { get; set; }                

        [ForeignKey("GroupId")]
        public virtual GroupUser GroupUser { get; set; }
    }
}
