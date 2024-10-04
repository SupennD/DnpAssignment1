using DTOs;

using Entities;

using Microsoft.AspNetCore.Mvc;

using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IResult> CreateAsync(CreateUserDto userDto)
    {
        User createdUser = await _userRepository.AddAsync(new User
        {
            Name = userDto.Name, Password = userDto.Password
        });
        return Results.Created($"users/{createdUser.Id}", createdUser);
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetSingleAsync(int id)
    {
        User user = await _userRepository.GetSingleAsync(id);
        return Results.Ok(new UserDto { Id = user.Id, Name = user.Name });
    }

    [HttpGet]
    public async Task<IResult> GetManyAsync([FromQuery] string? name)
    {
        IQueryable<User> users = _userRepository.GetMany();

        // Filter users by the "name" query parameter
        if (!string.IsNullOrEmpty(name))
        {
            users = users.Where(u => u.Name.ToLower().Contains(name.ToLower()));
        }

        IQueryable<UserDto> userDtos = users.Select(u => new UserDto { Id = u.Id, Name = u.Name });

        return Results.Ok(userDtos);
    }

    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateAsync(int id, CreateUserDto userDto)
    {
        await _userRepository.UpdateAsync(new User { Id = id, Name = userDto.Name, Password = userDto.Password });
        return Results.Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteAsync(int id)
    {
        await _userRepository.DeleteAsync(id);
        return Results.Ok();
    }
}
