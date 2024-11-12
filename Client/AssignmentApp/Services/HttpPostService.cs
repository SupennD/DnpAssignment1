using DTOs;

namespace AssignmentApp.Services;

public class HttpPostService(HttpClient httpClient) : IPostService
{
    public async Task<PostDto> AddPostAsync(CreatePostDto createPostDto)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("posts", createPostDto);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }

        PostDto? result = await response.Content.ReadFromJsonAsync<PostDto>();
        return result;
    }
}
