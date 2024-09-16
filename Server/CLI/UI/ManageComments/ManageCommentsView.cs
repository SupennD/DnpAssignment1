using Entities;

using RepositoryContracts;

namespace CLI.UI.ManageComments;

public class ManageCommentsView
{
    private readonly ICommentRepository _commentRepository;
    private readonly CreateCommentView _createCommentView;
    private readonly ListCommentsView _listCommentsView;
    private readonly SingleCommentView _singleCommentView;

    public ManageCommentsView(ICommentRepository commentRepository)
    {
        _createCommentView = new CreateCommentView(commentRepository);
        _listCommentsView = new ListCommentsView(commentRepository);
        _singleCommentView = new SingleCommentView(commentRepository);
        _commentRepository = commentRepository;
    }

    public async Task ShowMenuAsync()
    {
        bool back = false;

        while (!back)
        {
            Console.WriteLine("\nManage Comments Menu:");
            Console.WriteLine("1. Create Comment");
            Console.WriteLine("2. List Comments");
            Console.WriteLine("3. View Single Comment");
            Console.WriteLine("4. Update Comment");
            Console.WriteLine("5. Delete Comment");
            Console.WriteLine("0. Back");
            Console.Write("Select an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await _createCommentView.ShowCreateCommentViewAsync();
                    break;
                case "2":
                    await _listCommentsView.ShowListCommentViewAsync();
                    break;
                case "3":
                    await _singleCommentView.ViewSingleCommentAsync();
                    break;
                case "4":
                    await UpdateCommentAsync();
                    break;
                case "5":
                    await DeleteCommentAsync();
                    break;
                case "0":
                    back = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }

    private async Task UpdateCommentAsync()
    {
        try
        {
            Console.Write("\nEnter comment ID to update: ");

            int commentId = int.Parse(Console.ReadLine() ?? throw new ArgumentException("The comment ID is required."));
            Comment comment = await _commentRepository.GetSingleAsync(commentId);

            Console.Write("Enter new body: ");

            comment.Body = Console.ReadLine() ?? throw new ArgumentException("The body is required.");
            await _commentRepository.UpdateAsync(comment);

            Console.WriteLine("Comment updated successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    private async Task DeleteCommentAsync()
    {
        try
        {
            Console.Write("\nEnter comment ID to delete: ");

            int commentId = int.Parse(Console.ReadLine() ?? throw new ArgumentException("The comment ID is required."));
            await _commentRepository.DeleteAsync(commentId);

            Console.WriteLine("Comment deleted successfully.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
