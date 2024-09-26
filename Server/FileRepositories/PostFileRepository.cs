using Entities;

using RepositoryContracts;

namespace FileRepositories;

public class PostFileRepository() : GenericFileRepository<Post>("posts.json"), IPostRepository
{
    public override async Task<Post> AddAsync(Post post)
    {
        List<Post> posts = await ReadFromJsonAsync();
        post.Id = posts.Count > 0 ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        await WriteToJsonAsync(posts);
        return post;
    }

    public override async Task UpdateAsync(Post post)
    {
        List<Post> posts = await ReadFromJsonAsync();
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id);

        if (existingPost is null) throw new InvalidOperationException($"Post with ID '{post.Id}' not found");

        posts.Remove(existingPost);
        posts.Add(post);

        await WriteToJsonAsync(posts);
    }

    public override async Task DeleteAsync(int id)
    {
        List<Post> posts = await ReadFromJsonAsync();

        Post? existingPost = posts.SingleOrDefault(p => p.Id == id);

        if (existingPost is null) throw new InvalidOperationException($"Post with ID '{id}' not found");

        posts.Remove(existingPost);

        await WriteToJsonAsync(posts);
    }

    public override async Task<Post> GetSingleAsync(int id)
    {
        List<Post> posts = await ReadFromJsonAsync();
        Post? post = posts.SingleOrDefault(p => p.Id == id);
        if (post is null) throw new InvalidOperationException($"Post with ID '{id}' not found");
        return post;
    }

    public override IQueryable<Post> GetMany()
    {
        List<Post> posts = ReadFromJsonAsync().Result;
        return posts.AsQueryable();
    }
}
