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
    public class PostServiceTests : GenericCrudTestBase<Post>
    {
        private readonly Mock<IPostRepository> _mockPostRepo;
        private readonly PostService _postService;

        public PostServiceTests()
        {
            _mockPostRepo = new Mock<IPostRepository>();
            _postService = new PostService(_mockRepo.Object, _mockPostRepo.Object);
        }

        [Fact]
        public async Task GetPostsWithUserProfileAsync_ShouldReturnPosts()
        {
            // Arrange
            var fakeData = new List<object> { new { PostId = 1, Title = "Test Post", Username = "User1" } };
            _mockPostRepo.Setup(r => r.GetPostsWithUserProfileAsync())
                         .ReturnsAsync(fakeData);

            // Act
            var result = await _postService.GetPostsWithUserProfileAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            _mockPostRepo.Verify(r => r.GetPostsWithUserProfileAsync(), Times.Once);
        }
    }
}
