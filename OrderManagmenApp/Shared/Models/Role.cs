using System.ComponentModel.DataAnnotations;

namespace OrderManagmen.Shared.Models
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        public List<User> Users { get; set; } = new List<User>();
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
