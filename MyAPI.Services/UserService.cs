using Microsoft.EntityFrameworkCore;
using MyAPI.Repositories.Entities;
using MyAPI.Repositories.Interfaces;
using MyAPI.Services.Common;
using MyAPI.Services.Exceptions;
using MyAPI.Services.Interfaces;
using MyAPI.Services.Models;

namespace MyAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHashHelper _hashHelper;

        public UserService(IUserRepository userRepository, IHashHelper hashHelper)
        {
            _userRepository = userRepository;
            _hashHelper = hashHelper;
        }

        public async Task<UserResponse> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetAll()
                .Where(x => x.UserId == id)
                .Select(x => new UserResponse
                {
                    UserId = x.UserId,
                    FullName = x.FullName,
                    Email = x.Email,
                    CreatedAt = x.CreatedAt,
                    IsActive = x.IsActive
                }).FirstOrDefaultAsync();

            if (user == null) throw AppExceptions.NotFoundId();

            return user;
        }

        public async Task<List<UserResponse>> GetAllAsync()
        {
            return await _userRepository.GetAll()
                .Select(x => new UserResponse
                {
                    UserId = x.UserId,
                    FullName = x.FullName,
                    Email = x.Email,
                    CreatedAt = x.CreatedAt,
                    IsActive = x.IsActive
                }).ToListAsync();
        }

        public async Task<User> AddUserAsync(AddUserRequest request)
        {
            // Check if email already exists
            var existingUser = await _userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Email == request.Email);
            if (existingUser != null) 
                throw AppExceptions.Conflict("Email already exists");

            var newUser = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                PasswordHash = _hashHelper.HashPassword(request.Password),
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            return await _userRepository.Insert(newUser);
        }

        public async Task<UserResponse> UpdateUserAsync(UpdateUserRequest request)
        {
            var user = await _userRepository.GetById(request.UserId);
            if (user == null) throw AppExceptions.NotFoundId();

            user.FullName = request.FullName;
            user.Email = request.Email;
            user.IsActive = request.IsActive;

            await _userRepository.Update(user);

            return new UserResponse
            {
                UserId = user.UserId,
                FullName = user.FullName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            };
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetById(id);
                if (user == null) throw AppExceptions.NotFoundId();

                await _userRepository.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> AuthorizeAsync(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                throw AppExceptions.NotFoundAccount();

            var user = await _userRepository.GetAll()
                .SingleOrDefaultAsync(x => x.Email == email);

            if (user == null) throw AppExceptions.NotFoundAccount();

            if (!_hashHelper.VerifyPassword(password, user.PasswordHash))
                throw AppExceptions.NotFoundAccount();

            return user;
        }
    }
}
