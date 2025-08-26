using Moq;
using WebApiFinalProject1.Interface;
using WebApiFinalProject1.Models;
using WebApiFinalProject1.Service;

namespace BloggingTest.test_files
{
    public class UserServiceTests : GenericCrudTestBase<User>
    {
        private readonly Mock<IUserRepository> _mockUserRepo;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockUserRepo = new Mock<IUserRepository>();
            _userService = new UserService(_mockRepo.Object, _mockUserRepo.Object);
        }

        [Fact]
        public async Task GetUserStatsAsync_ShouldReturnStats()
        {
            var userId = 1;
            var expectedStats = new { UserId = 1, Username = "Anupam", PostCount = 5, CommentCount = 10 };

            _mockUserRepo.Setup(r => r.GetUserStatsAsync(userId)).ReturnsAsync(expectedStats);

            var result = await _userService.GetUserStatsAsync(userId);

            Assert.NotNull(result);
            Assert.Equal(1, result.GetType().GetProperty("UserId")?.GetValue(result));
            _mockUserRepo.Verify(r => r.GetUserStatsAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetUserStatsAsync_ShouldReturnNull_WhenUserNotFound()
        {
            var userId = 999;
            _mockUserRepo.Setup(r => r.GetUserStatsAsync(userId)).ReturnsAsync((object?)null);

            var result = await _userService.GetUserStatsAsync(userId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetUserLastActivityAsync_ShouldReturnDate()
        {
            var userId = 1;
            var expectedDate = DateTime.UtcNow;

            _mockUserRepo.Setup(r => r.GetUserLastActivityAsync(userId)).ReturnsAsync(expectedDate);

            var result = await _userService.GetUserLastActivityAsync(userId);

            Assert.Equal(expectedDate, result);
            _mockUserRepo.Verify(r => r.GetUserLastActivityAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetUserLastActivityAsync_ShouldReturnNull_WhenNoActivity()
        {
            var userId = 2;
            _mockUserRepo.Setup(r => r.GetUserLastActivityAsync(userId)).ReturnsAsync((DateTime?)null);

            var result = await _userService.GetUserLastActivityAsync(userId);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetUsersWithNoPostsAsync_ShouldReturnUsers()
        {
            var expectedUsers = new List<User>
            {
                new User { UserId = 1, Username = "NoPostUser1" },
                new User { UserId = 2, Username = "NoPostUser2" }
            };

            _mockUserRepo.Setup(r => r.GetUsersWithNoPostsAsync()).ReturnsAsync(expectedUsers);

            var result = await _userService.GetUsersWithNoPostsAsync();

            Assert.NotNull(result);
            Assert.Equal(2, new List<User>(result).Count);
            _mockUserRepo.Verify(r => r.GetUsersWithNoPostsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetUsersWithNoPostsAsync_ShouldReturnEmpty_WhenNoSuchUsers()
        {
            _mockUserRepo.Setup(r => r.GetUsersWithNoPostsAsync()).ReturnsAsync(new List<User>());

            var result = await _userService.GetUsersWithNoPostsAsync();

            Assert.Empty(result);
        }
    }
}