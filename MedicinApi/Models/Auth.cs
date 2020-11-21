using System.ComponentModel.DataAnnotations;

namespace MedicinApi.Models
{
    public class Auth
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
