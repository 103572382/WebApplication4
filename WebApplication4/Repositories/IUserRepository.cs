namespace WebApplication4.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(Guid id);
        Task CreateAsync(User user);
        Task UpdateAsync(User user);
    }

}
