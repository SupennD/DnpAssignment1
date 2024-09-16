using Entities;

using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }

    public void ShowListPostView(IPostRepository postRepository)
    {
        IQueryable<Post> queryablePost = postRepository.GetMany();

        if (!queryablePost.Any())
        {
            Console.WriteLine("There are no posts");
        }


        foreach (var post in queryablePost)
        {
            Console.WriteLine("Id: " +post.Id + " Title: " + post.Title);
        }

    }
    
}
