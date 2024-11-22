namespace WebApplication4.Repositories
{
    public interface IAccountRepository
    {
        Task<Account> GetByEmailAsync(string email);
        Task CreateAsync(Account account);
        Task UpdateAsync(Account account);
    }
}
