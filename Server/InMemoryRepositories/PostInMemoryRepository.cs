using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private readonly List<Post> posts = [];

    public PostInMemoryRepository()
    {
        CreateDummyData();
    }

    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any()
            ? posts.Max(p => p.Id) + 1
            : 1;

        posts.Add(post);

        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        var existingPost = posts.SingleOrDefault(p => p.Id == post.Id);

        if (existingPost is null) throw new InvalidOperationException($"Post with ID '{post.Id}' not found");

        posts.Remove(existingPost);
        posts.Add(post);

        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var existingPost = posts.SingleOrDefault(p => p.Id == id);

        if (existingPost is null) throw new InvalidOperationException($"Post with ID '{id}' not found");

        posts.Remove(existingPost);

        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        var post = posts.SingleOrDefault(p => p.Id == id);

        if (post is null) throw new InvalidOperationException($"Post with ID '{id}' not found");

        return Task.FromResult(post);
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }

    private void CreateDummyData()
    {
        for (var i = 0; i < 4; i++)
        {
            var post = new Post { Body = $"Post body {i}", Title = $"Title {i}", UserId = i };

            AddAsync(post);
        }
    }
}
