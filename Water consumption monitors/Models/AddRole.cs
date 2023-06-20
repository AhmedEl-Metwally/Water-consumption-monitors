using System.ComponentModel.DataAnnotations;

namespace Water_consumption_monitors.Models
{
    public class AddRole
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }

    }
}
