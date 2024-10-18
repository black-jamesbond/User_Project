using User_Project.DbContexts;
using User_Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace User_Project.Services
{
    public class UserRepository : IUserRepository
    {
        private UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> AddUserAsync(User User)
        {
            _context.Users.Add(User);
            await SaveChangesAsync();
            return User;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<User>> GetUserAsync()
        {
            return await _context.Users.OrderBy(c => c.Username).ToListAsync();
        }

        public async Task<User> GetUserByNameAsync(string Username) => await _context.Users.Where(c => c.Username == Username).FirstOrDefaultAsync();

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.OrderBy(c => c.Username).ToListAsync();
        }

        //public async Task<User> GetUser(string user)
        //{
        //    var person = await GetUserByNameAsync(user);

        //    var new_user = new User(1, "king", "loin", "@somthing");

        //    return new_user;
        //}

    }
}
