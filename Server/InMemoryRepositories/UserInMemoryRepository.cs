using Entities;

using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> users = [];
    

    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any()
            ? users.Max(x => x.Id) + 1
            : 1;

        users.Add(user);

        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(x => x.Id == user.Id);

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        User? existingUser = users.SingleOrDefault(x => x.Id == id);

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        users.Remove(existingUser);

        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? existingUser = users.SingleOrDefault(x => x.Id == id);

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        return Task.FromResult(existingUser);
    }

    public Task<User> GetSingleByNameAsync(string name)
    {
        User? existingUser = users.SingleOrDefault(user => user.Name.Equals(name));

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with Name '{name}' not found");
        }

        return Task.FromResult(existingUser);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }
}
