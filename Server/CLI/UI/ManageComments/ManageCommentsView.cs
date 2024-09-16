using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageComments
{
    public class ManageCommentsView
    {
        private readonly CreateCommentView _createCommentView;
        private readonly ListCommentsView _listCommentsView;
        private readonly ICommentRepository _commentRepository;

        public ManageCommentsView(ICommentRepository commentRepository)
        {
            _createCommentView = new CreateCommentView(commentRepository);
            _listCommentsView = new ListCommentsView(commentRepository);
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
                Console.WriteLine("3. Update Comment");
                Console.WriteLine("4. Delete Comment");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await _createCommentView.ShowCreateCommentViewAsync();
                        break;
                    case "2":
                        await _listCommentsView.ShowListCommentViewAsync();
                        break;
                    case "3":
                        await UpdateCommentAsync();
                        break;
                    case "4":
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
            Console.Write("\nEnter comment ID to update: ");
            int commentId = Convert.ToInt32(Console.ReadLine());

            Comment comment = await _commentRepository.GetSingleAsync(commentId);
            if (comment != null)
            {
                Console.Write("Enter new body: ");
                comment.Body = Console.ReadLine();

                await _commentRepository.UpdateAsync(comment);
                Console.WriteLine("\nComment updated successfully.\n");
            }
            else
            {
                Console.WriteLine("Comment not found.");
            }
        }

        private async Task DeleteCommentAsync()
        {
            Console.Write("Enter comment ID to delete: ");
            int commentId = Convert.ToInt32(Console.ReadLine());

            Comment comment = await _commentRepository.GetSingleAsync(commentId);
            if (comment != null)
            {
                await _commentRepository.DeleteAsync(commentId);
                Console.WriteLine("Comment deleted successfully.");
            }
            else
            {
                Console.WriteLine("Comment not found.");
            }
        }
    }
}
