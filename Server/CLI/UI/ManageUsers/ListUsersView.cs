using Entities;

using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository _userRepository;

    public ListUsersView(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task ShowListUserViewAsync()
    {
        IQueryable<User> queryableUser = _userRepository.GetMany();

        if (!queryableUser.Any())
        {
            Console.WriteLine("There are no users");
        }


        foreach (var user in queryableUser)
        {
            Console.WriteLine("Id: " + user.Id + " Name: " + user.Name);
        }

        return Task.CompletedTask;
    }

}
