using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TradingAutomaticoFx_Net.Infrastructure.Context
{
    public class ApplicationDbcontext : IdentityDbContext
    {
        public ApplicationDbcontext(DbContextOptions options) : base(options)
        {

        }
    }
}
