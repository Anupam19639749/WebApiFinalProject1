using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiFinalProject1.Models;
using WebApiFinalProject1.Service;

namespace WebApiFinalProject1.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;
        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(Post post)
        {
            var newPost = await _postService.AddPostAsync(post);
            return CreatedAtAction(nameof(GetPostById), new { id = newPost.PostId }, newPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, Post post)
        {
            var updatedPost = await _postService.UpdatePostAsync(id, post);
            if (updatedPost == null)
            {
                return NotFound();
            }
            return Ok(updatedPost);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _postService.DeletePostAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
