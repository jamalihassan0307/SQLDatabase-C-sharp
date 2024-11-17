using System.ComponentModel.DataAnnotations;

namespace FirstApplication.Models
{
    public class UserUpdateModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string Name { get; set; }
        
        public string Password { get; set; }
    }
}
