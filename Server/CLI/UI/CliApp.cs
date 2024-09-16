using CLI.UI.ManageComments;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUsers;

using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    private readonly ManageUsersView _manageUsersView;
    private readonly ManagePostsView _managePostsView;
    private readonly ManageCommentsView _manageCommentsView;

    public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        _manageUsersView = new ManageUsersView(userRepository);
        _managePostsView = new ManagePostsView(postRepository);
        _manageCommentsView = new ManageCommentsView(commentRepository);
    }

    public async Task StartAsync()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nChoose action:");
            Console.WriteLine("1. Manage users");
            Console.WriteLine("2. Manage posts");
            Console.WriteLine("3. Manage comments");
            Console.WriteLine("0. Exit");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await _manageUsersView.ShowMenuAsync();
                    break;
                case "2":
                    await _managePostsView.ShowMenuAsync();
                    break;
                case "3":
                    await _manageCommentsView.ShowMenuAsync();
                    break;
                case "0":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }
    }
}
