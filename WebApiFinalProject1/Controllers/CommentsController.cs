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
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Models.Comment comment)
        {
            var newComment = await _commentService.AddCommentAsync(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = newComment.CommentId }, newComment);
        }

        [HttpPut("{id}")]
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
