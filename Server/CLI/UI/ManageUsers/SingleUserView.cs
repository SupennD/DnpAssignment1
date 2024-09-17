using Entities;

using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class SingleUserView
{
    private readonly IUserRepository _userRepository;

    public SingleUserView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ViewSingleUserAsync()
    {
        try
        {
            Console.Write("\nEnter user ID: ");

            int userId = int.Parse(Console.ReadLine() ?? throw new ArgumentException("The user ID is required."));
            User user = await _userRepository.GetSingleAsync(userId);

            Console.WriteLine($"\nUser:\nID: {user.Id}\nUsername: {user.Name}\n");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
