using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyBlazorApp.Shared
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(64)]
        public byte[] PasswordHash { get; set; } = new byte[64];
        [Required]
        [MaxLength(128)]
        public byte[] PasswordSalt { get; set; } = new byte[128];

        public List<Role> Roles { get; set; } = new List<Role>();
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
