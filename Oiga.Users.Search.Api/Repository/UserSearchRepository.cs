using Oiga.Users.Search.Api.DBContext;
using Oiga.Users.Search.Api.Helpers;
using Oiga.Users.Search.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Oiga.Users.Search.Api.Repository
{
    public class UserSearchRepository : IUserSearchRepository
    {
        private readonly UserSearchContext _dbContext;
        const int MAX_PAGE_SIZE = 10;

        public UserSearchRepository(UserSearchContext dbContext)
        {
            _dbContext = dbContext;
        }
        public PagedHelper<User> SearchUsers(string name, int pageNumber)
        {
            string wordWithoutAccents = Regex.Replace(name.Normalize(NormalizationForm.FormD), @"[^a-zA-z0-9 ]+", "");
            var words = wordWithoutAccents.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);                       
            return PagedHelper<User>.ToPagedList(
                _dbContext.UserSearch.Where(x => words.Contains(x.FirstName) || words.Contains(x.LastName) || words.Contains(x.UserName)),
                pageNumber,
                MAX_PAGE_SIZE);
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
