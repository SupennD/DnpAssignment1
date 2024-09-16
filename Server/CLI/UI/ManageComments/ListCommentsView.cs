using Entities;

using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ListCommentsView
{
    private readonly ICommentRepository _commentRepository;

    public ListCommentsView(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public Task ShowListCommentViewAsync()
    {
        IQueryable<Comment> queryableComment = _commentRepository.GetMany();

        if (!queryableComment.Any())
        {
            Console.WriteLine("There are no comments.");
        }


        foreach (var comment in queryableComment)
        {
            Console.WriteLine("Id: " + comment.Id + " Body: " + comment.Body);
        }

        return Task.CompletedTask;
    }

}
