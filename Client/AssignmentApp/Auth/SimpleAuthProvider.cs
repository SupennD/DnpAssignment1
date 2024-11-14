using System.Security.Claims;
using System.Text.Json;

using DTOs;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace AssignmentApp.Auth;

public class SimpleAuthProvider(HttpClient httpClient, IJSRuntime jsRuntime) : AuthenticationStateProvider
{
    private UserDto? _userDto;

    public async Task LoginAsync(string username, string password)
    {
        HttpResponseMessage response =
            await httpClient.PostAsJsonAsync("auth/login", new CreateUserDto { Name = username, Password = password });
        string content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }

        _userDto = JsonSerializer.Deserialize<UserDto>(content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", content);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        _userDto = null;
        await jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_userDto is null)
        {
            string userAsJson = "";

            try
            {
                userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
            }
            catch (InvalidOperationException)
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }

            if (string.IsNullOrEmpty(userAsJson))
            {
                return new AuthenticationState(new ClaimsPrincipal());
            }

            _userDto = JsonSerializer.Deserialize<UserDto>(userAsJson,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, _userDto.Name), new Claim(ClaimTypes.NameIdentifier, _userDto.Id.ToString())
        };
        ClaimsIdentity identity = new(claims, "apiauth");
        ClaimsPrincipal claimsPrincipal = new(identity);
        return new AuthenticationState(claimsPrincipal);
    }
}
