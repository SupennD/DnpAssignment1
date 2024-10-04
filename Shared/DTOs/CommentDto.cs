namespace DTOs;

public class CommentDto
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public required string Body { get; set; }

}
