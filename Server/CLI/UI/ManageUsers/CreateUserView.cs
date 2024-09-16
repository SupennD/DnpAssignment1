using Entities;

using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository _userRepository;

    public CreateUserView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ShowCreateUserViewAsync()
    {
        Console.Write("Enter name: ");
        string? name = Console.ReadLine();

        Console.Write("Enter password: ");
        string? password = Console.ReadLine();


        if (name is null || password is null)
        {
            return;
        }

        User user = new() { Name = name, Password = password };

        await _userRepository.AddAsync(user);
        Console.WriteLine($"New User with id {user.Id} is created ");
    }
}
