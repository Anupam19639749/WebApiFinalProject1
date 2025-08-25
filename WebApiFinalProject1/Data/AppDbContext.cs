using Microsoft.EntityFrameworkCore;
using WebApiFinalProject1.Models;

namespace WebApiFinalProject1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(320);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.Username).IsUnique();
            });

            // Profile (One-to-One with User)
            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(p => p.ProfileId);
                entity.Property(p => p.Bio).HasMaxLength(500);
                entity.HasOne(p => p.User)
                      .WithOne(u => u.Profile)
                      .HasForeignKey<Profile>(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Post (One-to-Many with Post)
            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(p => p.PostId);
                entity.Property(p => p.Title).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Content).IsRequired().HasMaxLength(2000);
                entity.Property(p => p.PlatformPosted).IsRequired();

                entity.HasOne(p => p.User)
                      .WithMany(u => u.Posts)
                      .HasForeignKey(p => p.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Comment (One-to-Many with Post & User)
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.CommentId);
                entity.Property(c => c.Content).IsRequired().HasMaxLength(500);

                entity.HasOne(c => c.Post)
                      .WithMany(p => p.Comments)
                      .HasForeignKey(c => c.PostId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(c => c.User)
                      .WithMany(u => u.Comments)
                      .HasForeignKey(c => c.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed Data
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "anupam", Email = "anupam@example.com", PasswordHash = "hashed1", IsActive = true, Role = "Admin" },
                new User { UserId = 2, Username = "priya", Email = "priya@example.com", PasswordHash = "hashed2", IsActive = true, Role = "User" },
                new User { UserId = 3, Username = "rahul", Email = "rahul@example.com", PasswordHash = "hashed3", IsActive = true, Role = "User" }
            );

            modelBuilder.Entity<Profile>().HasData(
                new Profile { ProfileId = 1, UserId = 1, FirstName = "Anupam", LastName = "Agrawal", DateOfBirth = new DateTime(2000, 1, 1), Bio = "Tech enthusiast", Location = "India" },
                new Profile { ProfileId = 2, UserId = 2, FirstName = "Priya", LastName = "Sharma", DateOfBirth = new DateTime(1998, 5, 12), Bio = "Traveler and blogger", Location = "India" },
                new Profile { ProfileId = 3, UserId = 3, FirstName = "Rahul", LastName = "Verma", DateOfBirth = new DateTime(1995, 11, 30), Bio = "Food lover", Location = "India" }
            );

            modelBuilder.Entity<Post>().HasData(
                new Post { PostId = 1, UserId = 1, Title = "Getting Started with .NET 8", Content = "Content about .NET 8", CreatedAt = new DateTime(2024, 12, 1), PlatformPosted = "Reddit", Category = "Tech" },
                new Post { PostId = 2, UserId = 2, Title = "My Trip to Manali", Content = "Travel diary content", CreatedAt = new DateTime(2024, 11, 15), PlatformPosted = "Instagram", Category = "Travel" },
                new Post { PostId = 3, UserId = 3, Title = "Best Street Foods in Delhi", Content = "Food blog content", CreatedAt = new DateTime(2024, 10, 20), PlatformPosted = "Facebook", Category = "Food" }
            );

            modelBuilder.Entity<Comment>().HasData(
                new Comment { CommentId = 1, PostId = 1, UserId = 2, Content = "Great write-up!", IsApproved = true },
                new Comment { CommentId = 2, PostId = 1, UserId = 3, Content = "Very helpful, thanks!", IsApproved = true },
                new Comment { CommentId = 3, PostId = 2, UserId = 1, Content = "Looks amazing, Priya!", IsApproved = true },
                new Comment { CommentId = 4, PostId = 3, UserId = 1, Content = "Now I am hungry 😂", IsApproved = true }
            );
        }
    }
}
