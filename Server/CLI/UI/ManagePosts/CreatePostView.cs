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
        Console.WriteLine("Enter title: ");
        string? title = Console.ReadLine();

        Console.WriteLine("Enter body: ");
        string? body = Console.ReadLine();

        Console.WriteLine("Enter userID: ");
        int userId = Convert.ToInt32(Console.ReadLine());

        if (title is null || body is null)
        {
            return;
        }

        Post post = new Post { Body = body, UserId = userId, Title = title };

        await postRepository.AddAsync(post);
        Console.WriteLine($"Post with id {post.Id} is created ");
    }
}
