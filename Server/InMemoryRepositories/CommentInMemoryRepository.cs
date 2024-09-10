using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    List<Comment> comments = new();
    
    public Task<Comment> AddCommentAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(c => c.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateCommentAsync(Comment comment)
    {
        throw new NotImplementedException();
    }

    public Task DeleteCommentAsync(Comment comment)
    {
        throw new NotImplementedException();
    }

    public Task<Comment> GetPostByIdAsync(int postId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Comment> GetComments()
    {
        throw new NotImplementedException();
    }
}