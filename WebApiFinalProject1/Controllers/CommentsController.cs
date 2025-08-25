using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiFinalProject1.Service;

namespace WebApiFinalProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;
        public CommentsController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpGet("user/{userId}/comments")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetCommentsByUser(int userId)
        {
            var result = await _commentService.GetCommentsByUserAsync(userId);
            return Ok(result);
        }

        [HttpGet("post/{postId}/comments-with-user")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> GetCommentsForPostWithUser(int postId)
        {
            var result = await _commentService.GetCommentsForPostWithUserAsync(postId);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddComment(Models.Comment comment)
        {
            var newComment = await _commentService.AddCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.CommentId }, newComment);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateComment(int id, Models.Comment comment)
        {
            var updatedComment = await _commentService.UpdateCommentAsync(id, comment);
            if (updatedComment == null)
            {
                return NotFound();
            }
            return Ok(updatedComment);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentService.DeleteCommentAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
