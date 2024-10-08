using Entities;

using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class CreateCommentView
{
    private readonly ICommentRepository _commentRepository;

    public CreateCommentView(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task ShowCreateCommentViewAsync()
    {
        Console.Write("Enter body: ");
        string body = Console.ReadLine() ?? throw new ArgumentException("The body is required.");

        Console.Write("Enter postID: ");
        int postID = Convert.ToInt32(Console.ReadLine() ?? throw new ArgumentException("The postID is required."));

        Console.Write("Enter userID: ");
        int userId = Convert.ToInt32(Console.ReadLine() ?? throw new ArgumentException("The userID is required."));

        Comment comment = new() { Body = body, PostId = postID, UserId = userId };

        await _commentRepository.AddAsync(comment);
        Console.WriteLine($"New comment with id {comment.Id} is created");
    }
}
