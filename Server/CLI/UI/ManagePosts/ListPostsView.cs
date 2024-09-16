using Entities;

using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository _postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public Task ShowListPostViewAsync()
    {
        IQueryable<Post> queryablePost = _postRepository.GetMany();

        if (!queryablePost.Any())
        {
            Console.WriteLine("There are no posts");
        }


        foreach (var post in queryablePost)
        {
            Console.WriteLine("Id: " + post.Id + " Title: " + post.Title);
        }

        return Task.CompletedTask;
    }

}
