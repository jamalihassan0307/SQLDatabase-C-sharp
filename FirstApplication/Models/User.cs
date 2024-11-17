using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Models
{
    public class User
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [MinLength(5)]
        public string Password { get; set; }
    }
}
