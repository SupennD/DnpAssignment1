using System.Globalization;

using Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using RepositoryContracts;

namespace EFCRepository;

public class EfcUserRepository : IUserRepository
{
    private readonly AppContext ctx;

    public EfcUserRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }


    public async Task<User> AddAsync(User user)
    {
        EntityEntry<User> entityEntry = await ctx.Users.AddAsync(user);
        await ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task UpdateAsync(User user)
    {
        if (!(await ctx.Users.AnyAsync(p => p.Id == user.Id)))
        {
            throw new InvalidOperationException($"User with id {user.Id} not found");

        }
        ctx.Users.Update(user);
        await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var existing = await ctx.Users.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new InvalidOperationException($"User with id {id} not found");

        }
        ctx.Users.Remove(existing);
        await ctx.SaveChangesAsync();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        var existing = await ctx.Users.SingleOrDefaultAsync(p => p.Id == id);
        if (existing == null)
        {
            throw new InvalidOperationException($"User with id {id} not found");

        }
        return existing;
    }

    public async Task<User> GetSingleByNameAsync(string name)
    {
        var existing = await ctx.Users.SingleOrDefaultAsync(p => p.Name == name);
        if (existing == null)
        {
            throw new InvalidOperationException($"User with name : {name} not found");

        }
        return existing;
    }

    public IQueryable<User> GetMany()
    {
        return ctx.Users.AsQueryable();
    }
}
