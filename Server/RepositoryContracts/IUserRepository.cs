using Entities;

namespace RepositoryContracts;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User> GetSingleAsync(int id);
    Task<User> GetSingleByNameAsync(string name);
    IQueryable<User> GetMany();
}
