using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> users = new();

    public UserInMemoryRepository()
    {
        createDummyUsers();
    }

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
        var existingUser = users.SingleOrDefault(x => x.Id == user.Id);
        if (existingUser is null) throw new InvalidOperationException($"User with id {user.Id} not found");
        users.Remove(existingUser);
        users.Add(user);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var existingUser = users.SingleOrDefault(x => x.Id == id);
        if (existingUser is null) throw new InvalidOperationException($"User with id {id} not found");
        users.Remove(existingUser);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        var existingUser = users.SingleOrDefault(x => x.Id == id);
        if (existingUser is null) throw new InvalidOperationException($"User with id {id} not found");
        return Task.FromResult(existingUser);
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }

    private void createDummyUsers()
    {
        for (var i = 0; i < 4; i++)
        {
            var user = new User { Name = $"user{i}", Password = $"password{i}" };
            AddAsync(user);
        }
    }
}