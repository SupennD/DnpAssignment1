using Entities;

using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository _postRepository;

    public SinglePostView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task ViewSinglePostAsync()
    {
        try
        {
            Console.Write("Enter post ID: ");
            int postId = int.Parse(Console.ReadLine() ?? throw new ArgumentException("The post ID is required."));

            Post post = await _postRepository.GetSingleAsync(postId);

            Console.WriteLine($"\nPost:\nID: {post.Id}\nTitle: {post.Title}\nBody: {post.Body}\n");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
