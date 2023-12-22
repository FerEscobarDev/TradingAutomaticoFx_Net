using System.ComponentModel.DataAnnotations;

namespace TradingAutomaticoFx_Net.Domain.Dtos
{
    public class UserRegisterDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string PasswordConfirm { get; set; }
    }
}
