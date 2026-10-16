using PersonalFinanceMng.Domain.Entities;


namespace PersonalFinanceMng.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task AddAsync(User user, CancellationToken cancellationToken = default);
        Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default);
        Task SoftDeleteAsync(User user, CancellationToken cancellationToken = default);
        Task HardDeleteAsync(User user, CancellationToken cancellationToken = default);
        Task<bool> UserExistsAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
