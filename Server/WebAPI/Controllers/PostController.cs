using DTOs;

using Entities;

using Microsoft.AspNetCore.Mvc;

using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("posts")]
public class PostController : ControllerBase
{
    private readonly IPostRepository _postRepository;

    public PostController(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    [HttpPost]
    public async Task<IResult> CreateAsync(CreatePostDto postDto)
    {
        Post createdPost = await _postRepository.AddAsync(new Post
        {
            UserId = postDto.UserId, Title = postDto.Title, Body = postDto.Body
        });
        return Results.Created($"posts/{createdPost.Id}", createdPost);
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetSingleAsync(int id)
    {
        Post post = await _postRepository.GetSingleAsync(id);
        return Results.Ok(new PostDto { Id = post.Id, UserId = post.UserId, Title = post.Title, Body = post.Body });
    }

    [HttpGet]
    public async Task<IResult> GetManyAsync([FromQuery] int? userId)
    {
        IQueryable<Post> posts = _postRepository.GetMany();

        if (userId is not null)
        {
            posts = posts.Where(p => p.UserId.Equals(userId));
        }

        IQueryable<PostDto> postDtos =
            posts.Select(p => new PostDto { Id = p.Id, UserId = p.UserId, Title = p.Title, Body = p.Body });

        return Results.Ok(postDtos);
    }

    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateAsync(int id, CreatePostDto postDto)
    {
        await _postRepository.UpdateAsync(new Post
        {
            Id = id, UserId = postDto.UserId, Title = postDto.Title, Body = postDto.Body
        });
        return Results.Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IResult> DeleteAsync(int id)
    {
        await _postRepository.DeleteAsync(id);
        return Results.Ok();
    }
}
