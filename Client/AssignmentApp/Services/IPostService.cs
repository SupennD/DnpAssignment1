using DTOs;

namespace AssignmentApp.Services;

public interface IPostService
{
    public Task<PostDto> AddPostAsync(CreatePostDto createPostDto);
    Task<PostDto?> GetPostByIdAsync(int id);
}
