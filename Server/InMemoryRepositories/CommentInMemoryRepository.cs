using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private readonly List<Comment> comments = new();

    public CommentInMemoryRepository()
    {
        createDummyData();
    }

    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any()
            ? comments.Max(c => c.Id) + 1
            : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }


    public Task UpdateAsync(Comment comment)
    {
        var existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment is null) throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found.");
        comments.Remove(existingComment);
        comments.Add(comment);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var commentToRemove = comments.SingleOrDefault(c => c.Id == id);
        if (commentToRemove is null) throw new InvalidOperationException($"Comment with ID '{id}' not found.");
        comments.Remove(commentToRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        var existingComment = comments.SingleOrDefault(c => c.Id == id);
        if (existingComment is null) throw new InvalidOperationException($"Comment with ID '{id}' not found.");
        return Task.FromResult(existingComment);
    }

    public IQueryable<Comment> GetMany()
    {
        return comments.AsQueryable();
    }

    private void createDummyData()
    {
        for (var i = 0; i < 4; i++)
        {
            var comment = new Comment { Body = $"Body{i}", PostId = i, UserId = i };
            AddAsync(comment);
        }
    }
}