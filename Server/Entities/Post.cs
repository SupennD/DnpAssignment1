namespace Entities;

public class Post
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
}
