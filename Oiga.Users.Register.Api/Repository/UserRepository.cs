using Oiga.Users.Register.Api.DBContext;
using Oiga.Users.Register.Api.Models;
using System.Threading.Tasks;

namespace Oiga.Users.Register.Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _dbContext;

        public UserRepository(UserContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task InsertUserAsync(User user)
        {
            await _dbContext.AddAsync(user);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
