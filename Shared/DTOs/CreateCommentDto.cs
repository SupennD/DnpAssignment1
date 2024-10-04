namespace DTOs;

public class CreateCommentDto
{
    public int PostId { get; set; }
    public int UserId { get; set; }
    public required string Body { get; set; }
}
