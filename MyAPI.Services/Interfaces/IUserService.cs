using MyAPI.Repositories.Entities;
using MyAPI.Services.Models;

namespace MyAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> GetByIdAsync(int id);
        Task<List<UserResponse>> GetAllAsync();
        Task<User> AddUserAsync(AddUserRequest request);
        Task<UserResponse> UpdateUserAsync(UpdateUserRequest request);
        Task<bool> DeleteUserAsync(int id);
        Task<User> AuthorizeAsync(string email, string password);
    }
}
