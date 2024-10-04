namespace DTOs;

public class CreatePostDto
{
    public int UserId { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
}
