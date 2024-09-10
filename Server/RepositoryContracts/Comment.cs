using Entities;

namespace RepositoryContracts;

public interface Comment
{
    Task<Post> AddComment(Post post);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(Comment comment);
    Task<Post> GetPostByIdAsync(Guid postId);
    IQueryable<Post> GetPosts();
}