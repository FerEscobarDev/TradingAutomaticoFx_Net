using Microsoft.AspNetCore.Identity;

namespace TradingAutomaticoFx_Net.Domain.Entities
{
    public class User : IdentityUser
    {
        public required string Name { get; set; }
    }
}
