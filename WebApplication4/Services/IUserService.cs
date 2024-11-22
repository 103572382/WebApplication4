using WebApplication4.Dtos;

namespace WebApplication4.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(CreateUserDto dto);
        Task UpdateUserAsync(Guid id, UpdateUserDto dto);
    }

}
