using System.ComponentModel.DataAnnotations;

namespace Water_consumption_monitors.Dto
{
    public class RegisterDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
