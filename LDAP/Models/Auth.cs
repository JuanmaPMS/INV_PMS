using System.ComponentModel.DataAnnotations;

namespace LDAP.Models
{
    public class Auth
    {
        [Required]
        public string? User { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
