using User_Project.Entities;

namespace User_Project.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUserAsync();
        Task<User> GetUserByNameAsync(string Username);
        Task<User> AddUserAsync(User User);
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<User>> GetAllAsync();
    }
}
