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
        return result!;
    }

    public async Task<List<PostDto>> GetAllPostsAsync()
    {
        HttpResponseMessage response = await httpClient.GetAsync("posts");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to retrieve posts.");
        }

        List<PostDto>? posts = await response.Content.ReadFromJsonAsync<List<PostDto>>();
        return posts ?? new List<PostDto>();
    }

    public async Task<PostDto?> GetPostByIdAsync(int id)
    {
        try
        {
            return await httpClient.GetFromJsonAsync<PostDto>($"posts/{id}");
        }
        catch (HttpRequestException)
        {
            return null;
        }
    }
}
