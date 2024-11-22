using WebApplication4.Dtos;
using WebApplication4.Repositories;
using WebApplication4.Models;
using System;
using System.Threading.Tasks;

namespace WebApplication4.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetUserByIdAsync(id)
                   ?? throw new Exception("User not found.");
        }

        public async Task<User> CreateUserAsync(CreateUserDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Age = dto.Age,
                Address = dto.Address,
                DateOfBirth = dto.DateOfBirth,
                Ethnicity = dto.Ethnicity
            };

            await _userRepository.CreateAsync(user);
            return user;
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new Exception("User not found.");

            user.FirstName = dto.FirstName ?? user.FirstName;
            user.LastName = dto.LastName ?? user.LastName;
            user.Age = dto.Age ?? user.Age;
            user.Address = dto.Address ?? user.Address;
            user.Ethnicity = dto.Ethnicity ?? user.Ethnicity;

            await _userRepository.UpdateAsync(user);
        }
    }
}
