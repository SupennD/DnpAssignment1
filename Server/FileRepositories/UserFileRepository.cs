using Entities;

using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository() : GenericFileRepository<User>("users.json"), IUserRepository
{
    public override async Task<User> AddAsync(User user)
    {
        List<User> users = await ReadFromJsonAsync();
        user.Id = users.Count > 0 ? users.Max(p => p.Id) + 1 : 1;
        users.Add(user);
        await WriteToJsonAsync(users);
        return user;
    }

    public override async Task UpdateAsync(User user)
    {
        List<User> users = await ReadFromJsonAsync();
        User? existingUser = users.SingleOrDefault(p => p.Id == user.Id);

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{user.Id}' not found");
        }

        users.Remove(existingUser);
        users.Add(user);

        await WriteToJsonAsync(users);
    }

    public override async Task DeleteAsync(int id)
    {
        List<User> users = await ReadFromJsonAsync();

        User? existingUser = users.SingleOrDefault(u => u.Id == id);

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        users.Remove(existingUser);

        await WriteToJsonAsync(users);
    }

    public override async Task<User> GetSingleAsync(int id)
    {
        List<User> users = await ReadFromJsonAsync();
        User? user = users.SingleOrDefault(c => c.Id == id);
        if (user is null)
        {
            throw new InvalidOperationException($"User with ID '{id}' not found");
        }

        return user;
    }

    public async Task<User> GetSingleByNameAsync(string name)
    {
        List<User> users = await ReadFromJsonAsync();
        User? existingUser = users.SingleOrDefault(user => user.Name.Equals(name));

        if (existingUser is null)
        {
            throw new InvalidOperationException($"User with Name '{name}' not found");
        }

        return existingUser;
    }

    public override IQueryable<User> GetMany()
    {
        List<User> users = ReadFromJsonAsync().Result;
        return users.AsQueryable();
    }
}
