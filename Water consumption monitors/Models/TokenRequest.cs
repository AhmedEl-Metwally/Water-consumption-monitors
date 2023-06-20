using System.ComponentModel.DataAnnotations;

namespace Water_consumption_monitors.Models
{
    public class TokenRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
