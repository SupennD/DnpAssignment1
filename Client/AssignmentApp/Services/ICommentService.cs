using DTOs;

namespace AssignmentApp.Services;

public interface ICommentService
{
    public Task<CommentDto> AddCommentAsync(CreateCommentDto request);
    public Task DeleteCommentAsync(int id);
}
