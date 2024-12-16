using Microsoft.AspNetCore.Mvc;
using EvalApi.Src.Core.Services;
using EvalApi.Src.Views.Dto;
using EvalApi.Src.Models;


namespace EvalApi.Src.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    // Créer un post
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDto createPostDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var post = await _postService.CreatePostAsync(new PostModel
            {
                UserId = createPostDto.userId,
                Title = createPostDto.title,
                Body = createPostDto.body
            });

            return CreatedAtAction(nameof(GetPostById), new { postId = post.Id }, post);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    // Récupérer un post par ID
    [HttpGet("{postId}")]
    public async Task<IActionResult> GetPostById(int postId)
    {
        try
        {
            var post = await _postService.GetPostByIdAsync(postId);
            return Ok(post);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

    // Récupérer tous les posts pour un utilisateur
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetPostsByUserId(int userId)
    {
        var posts = await _postService.GetPostsByUserIdAsync(userId);
        return Ok(posts);
    }

    // Mettre à jour un post
    [HttpPut("{postId}")]
    public async Task<IActionResult> UpdatePost(int postId, [FromBody] PostDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var updatedPost = await _postService.UpdatePostAsync(new PostModel
            {
                Id = postId,
                UserId = postDto.userId,
                Title = postDto.title,
                Body = postDto.body
            });

            return Ok(updatedPost);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    // Supprimer un post
    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(int postId)
    {
        try
        {
            await _postService.DeletePostAsync(postId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
    }

     [HttpGet("error")]
    public IActionResult ThrowError()
    {
        throw new Exception("Ceci est une erreur test.");
    }
}
