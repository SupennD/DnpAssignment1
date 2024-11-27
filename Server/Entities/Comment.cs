namespace Entities;

public class Comment
{
    private Comment() { }
    public int Id { get; set; }
    public required string Body { get; set; }
    public Post Post { get; set; }
    public User User { get; set; }
}
