using Microsoft.EntityFrameworkCore;
using Oiga.Users.Register.Api.Models;

namespace Oiga.Users.Register.Api.DBContext
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        { }
        public DbSet<User> Users { get; set; }

    }
}
