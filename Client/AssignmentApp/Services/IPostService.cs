using DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssignmentApp.Services;

public interface IPostService
{
    Task<PostDto> AddPostAsync(CreatePostDto createPostDto);
    Task<List<PostDto>> GetAllPostsAsync();
}
