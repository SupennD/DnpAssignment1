using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    // private ManageCommentsView _manageCommentsView;
    // private ManagePostsView _managePostsView;
    // private ManageUsersView _manageUsersView;

    public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        // _manageUsersView = new ManageUsersView(userRepository);
        // _managePostsView = new ManagePostsView(postRepository);
        // _manageCommentsView = new ManageCommentsView(commentRepository);
    }

    public Task StartAsync()
    {
        bool running = true;

        while (running)
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
                    // _managePostsView.startAsync();
                    break;
                case "3":
                    // _manageCommentsView.startAsync();
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice, try again.");
                    break;
            }
        }


        return Task.CompletedTask;
    }
}
