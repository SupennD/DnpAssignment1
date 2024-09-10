using Entities;

namespace RepositoryContracts;

public interface ICommentRepository
{
    Task<Post> AddComment(ICommentRepository commentRepository);
    Task UpdateAsync(ICommentRepository commentRepository);
    Task DeleteAsync(ICommentRepository commentRepository);
    Task<Post> GetPostByIdAsync(Guid postId);
    IQueryable<Post> GetComments();
}