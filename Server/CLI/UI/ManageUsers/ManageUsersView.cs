using Entities;
using RepositoryContracts;

namespace CLI.UI.ManageUsers
{
    public class ManageUsersView
    {
        private readonly CreateUserView _createUserView;
        private readonly ListUsersView _listUsersView;
        private readonly IUserRepository _userRepository;

        public ManageUsersView(IUserRepository userRepository)
        {
            _createUserView = new CreateUserView(userRepository);
            _listUsersView = new ListUsersView(userRepository);
            _userRepository = userRepository;
        }

        public async Task ShowMenuAsync()
        {
            bool back = false;

            while (!back)
            {
                Console.WriteLine("\nManage Users Menu:");
                Console.WriteLine("1. Create User");
                Console.WriteLine("2. List Users");
                Console.WriteLine("3. Update User");
                Console.WriteLine("4. Delete User");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await _createUserView.ShowCreateUserViewAsync();
                        break;
                    case "2":
                        await _listUsersView.ShowListUserViewAsync();
                        break;
                    case "3":
                        await UpdateUserAsync();
                        break;
                    case "4":
                        await DeleteUserAsync();
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

        private async Task UpdateUserAsync()
        {
            Console.Write("\nEnter user ID to update: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            User user = await _userRepository.GetSingleAsync(userId);
            if (user != null)
            {
                Console.Write("Enter new username: ");
                user.Name = Console.ReadLine();

                Console.Write("Enter new email: ");
                user.Password = Console.ReadLine();

                await _userRepository.UpdateAsync(user);
                Console.WriteLine("\nUser updated successfully.\n");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        private async Task DeleteUserAsync()
        {
            Console.Write("Enter user ID to delete: ");
            int userId = Convert.ToInt32(Console.ReadLine());

            User user = await _userRepository.GetSingleAsync(userId);
            if (user != null)
            {
                await _userRepository.DeleteAsync(userId);
                Console.WriteLine("User deleted successfully.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }
    }
}
