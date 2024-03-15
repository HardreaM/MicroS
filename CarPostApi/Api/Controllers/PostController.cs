using Api.Controllers.CarPost.Requests;
using Api.Controllers.CarPost.Responses;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Intefaces;

namespace Api.Controllers;

[Route("public/posts")]
public class PostController : ControllerBase
{
    private readonly ICreatePost _createPost;

    public PostController(ICreatePost createPost)
    {
        _createPost = createPost;
    }

    [HttpGet]
    [ProducesResponseType<PostListResponse>(200)]
    public async Task<IActionResult> GetPostListAsync()
    {
        var posts = await _createPost.GetPostListAsync();

        return Ok(posts);
    }


    [HttpPost]
    [ProducesResponseType(200)]
    public async Task<ActionResult> CreatePostAsync([FromBody] PostRequest request)
    {
        var response = await _createPost.CreatePostAsync(new Post
        {
            UserId = request.UserId,
            Title = request.Title,
            Content = request.Content,
            CarId = request.CarId
        });

        return Ok(response);
    }

    [HttpPut]
    [ProducesResponseType<PostResponse>(200)]
    public async Task<ActionResult> UpdatePostAsync([FromBody] PostRequest request)
    {
        var response = await _createPost.UpdatePostAsync(new Post
        {
            UserId = request.UserId,
            Title = request.Title,
            Content = request.Content,
            CarId = request.CarId
        });

        return Ok(new PostResponse
        {
            Id = response.Id,
            UserId = response.UserId,
            CarId = response.CarId,
            Title = response.Title,
            Content = response.Content
        });
    }
}