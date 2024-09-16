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
    Console.Write("Enter post ID: ");
    int postId = Convert.ToInt32(Console.ReadLine());

    Post post = await _postRepository.GetSingleAsync(postId);

    Console.WriteLine($"\nPost:\nID: {post.Id}\nTitle: {post.Title}\nBody: {post.Body}\n");
  }
}
