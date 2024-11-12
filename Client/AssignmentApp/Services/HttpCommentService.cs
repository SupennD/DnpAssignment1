using DTOs;

namespace AssignmentApp.Services;

public class HttpCommentService(HttpClient httpClient) : ICommentService
{
    public async Task<CommentDto> AddCommentAsync(CreateCommentDto createCommentDto)
    {
        HttpResponseMessage response = await httpClient.PostAsJsonAsync("comments", createCommentDto);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }

        CommentDto? result = await response.Content.ReadFromJsonAsync<CommentDto>();

        if (result is null)
        {
            throw new Exception("The comment can't be added.");
        }

        return result;
    }

    public async Task DeleteCommentAsync(int id)
    {
        HttpResponseMessage response = await httpClient.DeleteAsync($"comments/{id}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(response.ReasonPhrase);
        }
    }
}
