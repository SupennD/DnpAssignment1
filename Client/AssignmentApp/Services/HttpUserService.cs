using DTOs;

using Exception = System.Exception;

namespace AssignmentApp.Services;

public class HttpUserService(HttpClient httpClient) : IUserService
{
    public async Task<UserDto> AddUserAsync(CreateUserDto createUserDto)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("users", createUserDto);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }

        UserDto? result = await response.Content.ReadFromJsonAsync<UserDto>();
        return result!;
    }

    public async Task<bool> isValidUserAsync(int userId)
    {
        HttpResponseMessage response = await httpClient.GetAsync($"users/{userId}");
        return response.IsSuccessStatusCode;
    }

    public Task UpdateUserAsync(UserDto userDto)
    {
        throw new NotImplementedException();
    }
}
