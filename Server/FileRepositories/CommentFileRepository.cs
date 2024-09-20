using System.Text.Json;

using Entities;

using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository: GenericFileRepository<Comment> ,ICommentRepository
{
    protected override string FilePath { get; } = "comments.json";
    public override async Task<Comment> AddAsync(Comment comment)
    {
        List<Comment> comments = await ReadFromJsonAsync();
        int maxId = comments.Count > 0 ? comments.Max(x => x.Id) + 1 : 1;
        comments.Add(comment);
        await WriteToJsonAsync(comments);
        return comment;
    }

    public override async Task UpdateAsync(Comment comment)
    {
        List<Comment> comments = await ReadFromJsonAsync();
        
        var existingComment = comments.SingleOrDefault(c => c.Id == comment.Id);
        if (existingComment is null) throw new InvalidOperationException($"Comment with ID '{comment.Id}' not found");
        
        comments.Remove(existingComment);
        comments.Add(comment);
        
        await WriteToJsonAsync(comments);
        
    }

    public override async Task DeleteAsync(int id)
    {
        List<Comment> comments = await ReadFromJsonAsync();
        
        var existingComment = comments.SingleOrDefault(c => c.Id == id);

        if (existingComment is null) throw new InvalidOperationException($"Comment with ID '{id}' not found");

        comments.Remove(existingComment);
        await WriteToJsonAsync(comments);
    }

    public override async Task<Comment> GetSingleAsync(int id)
    {
        List<Comment> comments = await ReadFromJsonAsync();
        
        var existingComment = comments.SingleOrDefault(c => c.Id == id);

        if (existingComment is null) throw new InvalidOperationException($"Comment with ID '{id}' not found");

        await WriteToJsonAsync(comments);
        return existingComment;
    }

    public override IQueryable<Comment> GetMany()
    {
        List<Comment> comments = ReadFromJsonAsync().Result;
        return comments.AsQueryable();
    }
}
