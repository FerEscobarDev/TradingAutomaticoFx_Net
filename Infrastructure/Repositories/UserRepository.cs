using TradingAutomaticoFx_Net.Domain.Entities;
using TradingAutomaticoFx_Net.Infrastructure.Repositories.Interfaces;

namespace TradingAutomaticoFx_Net.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserRepository(IGenericRepository<User> genericRepository)
        {
            _userRepository = genericRepository;
        }
    }
}
