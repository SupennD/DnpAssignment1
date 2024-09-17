using Entities;

using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class SingleCommentView
{
    private readonly ICommentRepository _commentRepository;

    public SingleCommentView(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task ViewSingleCommentAsync()
    {
        try
        {
            Console.Write("\nEnter comment ID: ");

            int commentId = int.Parse(Console.ReadLine() ?? throw new ArgumentException("The comment ID is required."));
            Comment comment = await _commentRepository.GetSingleAsync(commentId);

            Console.WriteLine(
                $"\nComment:\nID: {comment.Id}\nUser ID: {comment.UserId}\nPost ID: {comment.PostId}\nBody: {comment.Body}\n");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
