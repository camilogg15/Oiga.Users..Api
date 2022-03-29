using Oiga.Users.Search.Api.Helpers;
using Oiga.Users.Search.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Oiga.Users.Search.Api.Repository
{
    public interface IUserSearchRepository
    {
        Task InsertUserAsync(User user);
        PagedHelper<User> SearchUsers(string name, int pageNumber);
        Task SaveAsync();
    }
}
