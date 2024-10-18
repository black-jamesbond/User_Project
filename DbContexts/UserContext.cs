using Microsoft.EntityFrameworkCore;
using User_Project.Entities;

namespace User_Project.DbContexts
{
    public class UserContext : DbContext
    {
        
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
    
}
