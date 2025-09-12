using Microsoft.EntityFrameworkCore;
using MyAPI.Repositories.Entities;
using MyAPI.Repositories.Infrastructure;
using MyAPI.Repositories.Interfaces;

namespace MyAPI.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(MyAPIContext context) : base(context) { }

        public override async Task<User> GetById(int id)
        {
            return await Table.FirstOrDefaultAsync(x => x.UserId == id);
        }

        // Add custom methods here
        // Example:
        // public async Task<User> GetByEmailAsync(string email)
        // {
        //     return await Table.FirstOrDefaultAsync(x => x.Email == email);
        // }

        // public async Task<bool> UserExistsAsync(int id)
        // {
        //     return await Table.AnyAsync(x => x.UserId == id);
        // }
    }
}
