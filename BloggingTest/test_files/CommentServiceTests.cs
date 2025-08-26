using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;
using WebApiFinalProject1.Service;

namespace BloggingTest.test_files
{
    public class CommentServiceTests : GenericCrudTestBase<Comment>
    {
        private readonly Mock<ICommentRepository> _mockCommentRepo;
        private readonly CommentService _commentService;

        public CommentServiceTests()
        {
            _mockCommentRepo = new Mock<ICommentRepository>();
            _commentService = new CommentService(_mockRepo.Object, _mockCommentRepo.Object);
        }

        [Fact]
        public async Task GetCommentsForPostWithUserAsync_ShouldReturnComments()
        {
            // Arrange
            int postId = 1;
            var fakeData = new List<object> { new { CommentId = 1, Content = "Nice post", UserId = 10 } };
            _mockCommentRepo.Setup(r => r.GetCommentsForPostWithUserAsync(postId))
                            .ReturnsAsync(fakeData);

            // Act
            var result = await _commentService.GetCommentsForPostWithUserAsync(postId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            _mockCommentRepo.Verify(r => r.GetCommentsForPostWithUserAsync(postId), Times.Once);
        }

        [Fact]
        public async Task GetCommentsByUserAsync_ShouldReturnComments()
        {
            // Arrange
            int userId = 10;
            var fakeData = new List<object> { new { CommentId = 1, Content = "Great article", PostId = 5 } };
            _mockCommentRepo.Setup(r => r.GetCommentsByUserAsync(userId))
                            .ReturnsAsync(fakeData);

            // Act
            var result = await _commentService.GetCommentsByUserAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            _mockCommentRepo.Verify(r => r.GetCommentsByUserAsync(userId), Times.Once);
        }
    }
}
