using DTOs;

using Entities;

using Microsoft.AspNetCore.Mvc;

using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("comments")]
public class CommentController : ControllerBase
{
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public CommentController(ICommentRepository commentRepository, IPostRepository postRepository,
        IUserRepository userRepository)
    {
        _commentRepository = commentRepository;
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IResult> CreateAsync(CreateCommentDto commentDto)
    {
        Comment comment = await _commentRepository.AddAsync(new Comment
        {
            Body = commentDto.Body,
            User = await _userRepository.GetSingleAsync(commentDto.UserId),
            Post = await _postRepository.GetSingleAsync(commentDto.PostId)
        });
        return Results.Created($"comments/{comment.Id}",
            new CommentDto
            {
                Id = comment.Id, PostId = comment.Post.Id, UserId = comment.User.Id, Body = comment.Body
            });
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetSingleAsync([FromRoute] int id)
    {
        Comment comment = await _commentRepository.GetSingleAsync(id);
        return Results.Ok(new CommentDto { Id = comment.Id, Body = comment.Body });
    }

    [HttpGet]
    public async Task<IResult> GetManyAsync([FromQuery] int? userId, int? postId)
    {
        IQueryable<Comment> comments = _commentRepository.GetMany();
        if (userId is not null)
        {
            comments = comments.Where(c => c.User.Equals(userId));
        }

        if (postId is not null)
        {
            comments.Where(c => c.Post.Equals(postId));
        }

        IQueryable<CommentDto> commentDtos = comments.Select(c => new CommentDto
        {
            Id = c.Id, Body = c.Body, UserId = c.User.Id, PostId = c.Post.Id
        });
        return Results.Ok(commentDtos);
    }

    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateAsync(int id, CreateCommentDto commentDto)
    {
        await _commentRepository.UpdateAsync(new Comment
        {
            Id = id,
            Body = commentDto.Body,
            User = await _userRepository.GetSingleAsync(commentDto.UserId),
            Post = await _postRepository.GetSingleAsync(commentDto.PostId)
        });
        return Results.Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteAsync(int id)
    {
        await _commentRepository.DeleteAsync(id);
        return Results.Ok();
    }
}
