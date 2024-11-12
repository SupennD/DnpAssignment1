using DTOs;

namespace AssignmentApp.Services;

public interface IPostService
{
    Task<PostDto> AddPostAsync(CreatePostDto createPostDto);
    Task<List<PostDto>> GetAllPostsAsync();
    Task<PostDto?> GetPostByIdAsync(int id);
}
