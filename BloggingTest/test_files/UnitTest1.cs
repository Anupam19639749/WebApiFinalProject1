using System.Collections.Generic;
using WebApiFinalProject1.Models;
using Xunit;

namespace WebApiFinalProject1.test_files
{
    public class UnitTest1
    {
        [Fact]
        public void User_Model_Should_Store_Properties_Correctly()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                Username = "testuser",
                Email = "test@example.com",
                PasswordHash = "hashedpassword",
                IsActive = true,
                Role = "Admin"
            };

            // Act & Assert
            Assert.Equal(1, user.UserId);
            Assert.Equal("testuser", user.Username);
            Assert.Equal("test@example.com", user.Email);
            Assert.Equal("hashedpassword", user.PasswordHash);
            Assert.True(user.IsActive);
            Assert.Equal("Admin", user.Role);
        }

        [Fact]
        public void User_Model_Should_Allow_Profile_Assignment()
        {
            // Arrange
            var profile = new Profile
            {
                ProfileId = 10,
                FirstName = "John",
                LastName = "Doe"
            };

            var user = new User
            {
                UserId = 2,
                Username = "profileuser",
                Profile = profile
            };

            // Act & Assert
            Assert.NotNull(user.Profile);
            Assert.Equal("John", user.Profile.FirstName);
            Assert.Equal("Doe", user.Profile.LastName);
        }

        [Fact]
        public void User_Model_Should_Handle_Posts_Collection()
        {
            // Arrange
            var post1 = new Post { PostId = 1, Title = "Post 1" };
            var post2 = new Post { PostId = 2, Title = "Post 2" };

            var user = new User { UserId = 3, Username = "postuser" };
            user.Posts.Add(post1);
            user.Posts.Add(post2);

            // Act
            var count = user.Posts.Count;

            // Assert
            Assert.Equal(2, count);
            Assert.Contains(user.Posts, p => p.Title == "Post 1");
            Assert.Contains(user.Posts, p => p.Title == "Post 2");
        }

        [Fact]
        public void User_Model_Should_Handle_Comments_Collection()
        {
            // Arrange
            var comment1 = new Comment { CommentId = 1, Content = "Nice post!" };
            var comment2 = new Comment { CommentId = 2, Content = "Thanks for sharing!" };

            var user = new User { UserId = 4, Username = "commentuser" };
            user.Comments.Add(comment1);
            user.Comments.Add(comment2);

            // Act
            var count = user.Comments.Count;

            // Assert
            Assert.Equal(2, count);
            Assert.Contains(user.Comments, c => c.Content == "Nice post!");
            Assert.Contains(user.Comments, c => c.Content == "Thanks for sharing!");
        }
    }
}
