using DTOs;

using Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("posts")]
public class PostController : ControllerBase
{
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public PostController(IPostRepository postRepository, IUserRepository userRepository)
    {
        _postRepository = postRepository;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IResult> CreateAsync(CreatePostDto postDto)
    {
        Post createdPost = await _postRepository.AddAsync(new Post
        {
            User = await _userRepository.GetSingleAsync(postDto.UserId), Title = postDto.Title, Body = postDto.Body
        });
        return Results.Created($"posts/{createdPost.Id}",
            new PostDto
            {
                Id = createdPost.Id,
                UserId = createdPost.User.Id,
                Title = createdPost.Title,
                Body = createdPost.Body
            });
    }

    [HttpGet("{id:int}")]
    public async Task<IResult> GetSingleAsync(int id)
    {
        PostDto? postDto = await _postRepository
            .GetMany()
            .Where(p => p.Id == id)
            .Include(p => p.User)
            .Include(p => p.Comments)
            .Select(p => new PostDto { Id = p.Id, UserId = p.User.Id, Title = p.Title, Body = p.Body })
            .FirstOrDefaultAsync();

        return postDto == null ? Results.NotFound() : Results.Ok(postDto);
    }

    [HttpGet]
    public async Task<IResult> GetManyAsync([FromQuery] int? userId)
    {
        IQueryable<Post> posts = _postRepository.GetMany();

        if (userId is not null)
        {
            posts = posts.Where(p => p.User.Id.Equals(userId));
        }

        IQueryable<PostDto> postDtos =
            posts.Select(p => new PostDto { Id = p.Id, UserId = p.User.Id, Title = p.Title, Body = p.Body });

        return Results.Ok(postDtos);
    }

    [HttpPut("{id:int}")]
    public async Task<IResult> UpdateAsync(int id, CreatePostDto postDto)
    {
        await _postRepository.UpdateAsync(new Post
        {
            Id = id,
            User = await _userRepository.GetSingleAsync(postDto.UserId),
            Title = postDto.Title,
            Body = postDto.Body
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
