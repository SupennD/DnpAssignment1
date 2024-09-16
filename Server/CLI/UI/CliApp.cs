using CLI.UI.ManagePosts;

using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    // private ManageCommentsView _manageCommentsView;
    private readonly ManagePostsView _managePostsView;
    // private ManageUsersView _manageUsersView;

    public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        // _manageUsersView = new ManageUsersView(userRepository);
        _managePostsView = new ManagePostsView(postRepository);
        // _manageCommentsView = new ManageCommentsView(commentRepository);
    }

    public async Task StartAsync()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Choose action:");
            Console.WriteLine("1. Manage users");
            Console.WriteLine("2. Manage posts");
            Console.WriteLine("3. Manage comments");
            Console.WriteLine("0. Exit");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    // _manageUsersView.startAsync();
                    break;
                case "2":
                    await _managePostsView.ShowMenuAsync();
                    break;
                case "3":
                    // _manageCommentsView.startAsync();
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
