using MyAPI.Repositories.Entities;
using MyAPI.Repositories.Infrastructure;

namespace MyAPI.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        // Add custom methods here
        // Example:
        // Task<User> GetByEmailAsync(string email);
        // Task<bool> UserExistsAsync(int id);
    }
}
