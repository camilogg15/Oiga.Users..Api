using Microsoft.EntityFrameworkCore;
using Oiga.Users.Search.Api.Models;

namespace Oiga.Users.Search.Api.DBContext
{
    public class UserSearchContext : DbContext
    {
        public UserSearchContext(DbContextOptions<UserSearchContext> options) : base(options)
        { }
        public DbSet<User> UserSearch { get; set; }
    }
}
