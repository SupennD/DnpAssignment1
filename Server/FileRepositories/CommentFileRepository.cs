using Entities;

using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository() : GenericFileRepository<Comment>("comments.json"), ICommentRepository
{
    public override async Task<Comment> AddAsync(Comment comment)
    {
        List<Comment> comments = await ReadFromJsonAsync();
        comment.Id = comments.Count > 0 ? comments.Max(p => p.Id) + 1 : 1;
        comments.Add(comment);
        await WriteToJsonAsync(comments);
        return comment;
    }

    public override async Task UpdateAsync(Comment comment)
    {
        List<Comment> comments = await ReadFromJsonAsync();
        Comment? existingComment = comments.SingleOrDefault(p => p.Id == comment.Id);

        if (existingComment is null) throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");

        comments.Remove(existingComment);
        comments.Add(comment);

        await WriteToJsonAsync(comments);
    }

    public override async Task DeleteAsync(int id)
    {
        List<Comment> comments = await ReadFromJsonAsync();

        Comment? existingComment = comments.SingleOrDefault(c => c.Id == id);

        if (existingComment is null) throw new InvalidOperationException($"Comment with ID '{id}' not found");

        comments.Remove(existingComment);

        await WriteToJsonAsync(comments);
    }

    public override async Task<Comment> GetSingleAsync(int id)
    {
        List<Comment> comments = await ReadFromJsonAsync();
        Comment? comment = comments.SingleOrDefault(c => c.Id == id);
        if (comment is null) throw new InvalidOperationException($"Comment with ID '{id}' not found");
        return comment;
    }

    public override IQueryable<Comment> GetMany()
    {
        List<Comment> comments = ReadFromJsonAsync().Result;
        return comments.AsQueryable();
    }
}
