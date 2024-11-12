using DTOs;

namespace AssignmentApp.Services;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto createUserDto);
    public Task<bool> isValidUserAsync(int userId);
    public Task UpdateUserAsync(UserDto userDto);
}
