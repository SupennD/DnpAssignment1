using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Comment> AddCommentAsync(Comment comment);
    Task UpdateCommentAsync(Comment comment);
    Task DeleteCommentAsync(Comment comment);
    Task<Comment> GetPostByIdAsync(int postId);
    IQueryable<Comment> GetComments();
}