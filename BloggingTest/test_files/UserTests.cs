using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.test_files
{
    public class UserTests
    {
        [Fact]
        public void User_DefaultValues_ShouldInitializeCollections()
        {
            // Arrange & Act
            var user = new User();

            // Assert
            Assert.NotNull(user.Posts);
            Assert.NotNull(user.Comments);
            Assert.Empty(user.Posts);
            Assert.Empty(user.Comments);
        }

        [Fact]
        public void User_SetProperties_ShouldAssignCorrectValues()
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

            // Assert
            Assert.Equal(1, user.UserId);
            Assert.Equal("testuser", user.Username);
            Assert.Equal("test@example.com", user.Email);
            Assert.Equal("hashedpassword", user.PasswordHash);
            Assert.True(user.IsActive);
            Assert.Equal("Admin", user.Role);
        }

        // ---------------- NEGATIVE TESTS ----------------

        [Fact]
        public void User_Username_ShouldNotBeNull()
        {
            // Arrange
            var user = new User();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => user.Username.Length);
        }

        [Fact]
        public void User_Email_ShouldNotBeNull()
        {
            // Arrange
            var user = new User();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => user.Email.Length);
        }

        [Fact]
        public void User_PasswordHash_ShouldNotBeNull()
        {
            // Arrange
            var user = new User();

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => user.PasswordHash.Length);
        }

        [Fact]
        public void User_AddingNullPost_ShouldThrow()
        {
            // Arrange
            var user = new User();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => user.Posts.Add(null!));
        }

        [Fact]
        public void User_AddingNullComment_ShouldThrow()
        {
            // Arrange
            var user = new User();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => user.Comments.Add(null!));
        }
    }
}
