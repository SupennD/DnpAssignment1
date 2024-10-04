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

    public CommentController(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    [HttpPost]
    public async Task<IResult> CreateAsync(CreateCommentDto commentDto)
    {
        Comment comment = await _commentRepository.AddAsync(new Comment
        {
            Body = commentDto.Body, UserId = commentDto.UserId, PostId = commentDto.PostId
        });
        return Results.Created($"comments/{comment.Id}", comment);
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
            comments = comments.Where(c => c.UserId.Equals(userId));
        }

        if (postId is not null)
        {
            comments.Where(c => c.PostId.Equals(postId));
        }

        IQueryable<CommentDto> commentDtos = comments.Select(c => new CommentDto
        {
            Id = c.Id, Body = c.Body, UserId = c.UserId, PostId = c.PostId
        });
        return Results.Ok(commentDtos);
    }

    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateAsync(int id, CreateCommentDto commentDto)
    {
        await _commentRepository.UpdateAsync(new Comment
        {
            Id = id, Body = commentDto.Body, UserId = commentDto.UserId, PostId = commentDto.PostId
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
