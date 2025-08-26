using Moq;
using WebApiFinalProject1.Interface; // adjust namespace if needed

namespace BloggingTest.test_files
{
    public abstract class GenericCrudTestBase<TEntity> where TEntity : class, new()
    {
        protected readonly Mock<IBloggingAPI<TEntity>> _mockRepo;
        protected readonly IBloggingAPI<TEntity> _service;

        protected GenericCrudTestBase()
        {
            _mockRepo = new Mock<IBloggingAPI<TEntity>>();
            _service = _mockRepo.Object;
        }

        [Fact]
        public async Task AddAsync_ShouldReturnEntity()
        {
            var entity = new TEntity();
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<TEntity>())).ReturnsAsync(entity);

            var result = await _service.AddAsync(entity);

            Assert.NotNull(result);
            _mockRepo.Verify(r => r.AddAsync(It.IsAny<TEntity>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity()
        {
            var entity = new TEntity();
            _mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(entity);

            var result = await _service.GetByIdAsync(1);

            Assert.NotNull(result);
            _mockRepo.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnEntities()
        {
            var entities = new List<TEntity> { new TEntity(), new TEntity() };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);

            var result = await _service.GetAllAsync();

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnEntity()
        {
            // Arrange
            var entity = new TEntity();
            int entityId = 1; // fake ID for testing

            _mockRepo.Setup(r => r.UpdateAsync(entityId, It.IsAny<TEntity>()))
                     .ReturnsAsync(entity);

            // Act
            var result = await _service.UpdateAsync(entityId, entity);

            // Assert
            Assert.NotNull(result);
            _mockRepo.Verify(r => r.UpdateAsync(entityId, It.IsAny<TEntity>()), Times.Once);
        }


        [Fact]
        public async Task DeleteAsync_ShouldReturnTrue()
        {
            _mockRepo.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

            var result = await _service.DeleteAsync(1);

            Assert.True(result);
            _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
    }
}
