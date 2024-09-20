using Entities;

using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : GenericFileRepository<User>("users.json"), IUserRepository
{
    public override async Task<User> AddAsync(User entity)
    {
        List<User> users = await ReadFromJsonAsync();
        int maxId = users.Count > 0 ? users.Max(c => c.Id) + 1 : 1;
        users.Add(entity);
        await WriteToJsonAsync(users);
        return entity;
    }

    public override async Task UpdateAsync(User entity)
    {
        List<User> users = await ReadFromJsonAsync();
        User? userToUpdate = users.SingleOrDefault(c => c.Id == entity.Id);
        if (userToUpdate == null)
        {
            throw new InvalidOperationException($"User with ID '{entity.Id}' not found");
        }

        users.Remove(userToUpdate);
        users.Add(entity);
        await WriteToJsonAsync(users);
    }

    public override async Task DeleteAsync(int id)
    {
        List<User> users = await ReadFromJsonAsync();
        User? userToUpdate = users.SingleOrDefault(c => c.Id == id);
        if (userToUpdate == null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        users.Remove(userToUpdate);
        await WriteToJsonAsync(users);
    }

    public override async Task<User> GetSingleAsync(int id)
    {
        List<User> users = await ReadFromJsonAsync();
        User? userToUpdate = users.SingleOrDefault(c => c.Id == id);
        if (userToUpdate == null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        return userToUpdate;
    }

    public override IQueryable<User> GetMany()
    {
        List<User> users = ReadFromJsonAsync().Result;
        return users.Count < 0 ? throw new InvalidOperationException("No users found ") : users.AsQueryable();
    }
}
