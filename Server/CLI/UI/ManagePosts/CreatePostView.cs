using Entities;

using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository postRepository;
    public CreatePostView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    public async Task ShowCreatePostViewAsync()
    {
        Console.Write("Enter title: ");
        string title = Console.ReadLine() ?? throw new ArgumentException("The title is required.");

        Console.Write("Enter body: ");
        string body = Console.ReadLine() ?? throw new ArgumentException("The body is required.");

        Console.Write("Enter userID: ");
        int userId = Convert.ToInt32(Console.ReadLine() ?? throw new ArgumentException("The userID is required."));

        Post post = new() { Body = body, UserId = userId, Title = title };

        await postRepository.AddAsync(post);
        Console.WriteLine($"Post with id {post.Id} is created ");
    }
}
