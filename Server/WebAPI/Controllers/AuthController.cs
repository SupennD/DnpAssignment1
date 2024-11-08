using DTOs;

using Entities;

using Microsoft.AspNetCore.Mvc;

using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IResult> LoginAsync([FromBody] CreateUserDto createUserDto)
    {
        try
        {
            User user = await _userRepository.GetSingleByNameAsync(createUserDto.Name);

            if (!user.Password.Equals(createUserDto.Password))
            {
                return Results.Unauthorized();
            }

            return Results.Ok(new UserDto { Id = user.Id, Name = user.Name });
        }
        catch (Exception _)
        {
            return Results.Unauthorized();
        }
    }
}
