using Oiga.Users.Register.Api.Models;
using System.Threading.Tasks;

namespace Oiga.Users.Register.Api.Repository
{
    public interface IUserRepository
    {
        Task InsertUserAsync(User user);
        Task SaveAsync();
    }
}
