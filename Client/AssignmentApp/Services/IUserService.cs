using DTOs;

namespace AssignmentApp.Services;

public interface IUserService
{
    public Task<UserDto> AddUserAsync(CreateUserDto createUserDto);
    public Task UpdateUserAsync(UserDto userDto);
}
