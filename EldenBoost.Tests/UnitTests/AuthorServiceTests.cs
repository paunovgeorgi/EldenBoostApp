using EldenBoost.Core.Contracts;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Repository;

namespace EldenBoost.Tests.UnitTests
{
    [TestFixture]
    public class AuthorServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private IAuthorService authorService;

        [OneTimeSetUp]
        public void Setup()
        {
            repository = new Repository(data); 
            authorService = new AuthorService(repository);
        }

        [Test]
        public async Task ExistsByUserIdAsync_ShouldReturnTrue_WithValidUserId()
        {
            //Arrange
            string userId = Author.UserId;

            //Act
            bool result = await authorService.ExistsByUserIdAsync(userId);

            //Assert
            Assert.True(result);
        }

        [Test]
        public async Task ExistsByUserIdAsync_ShouldReturnFalse_WithInvalidUserId()
        {
            //Arrange
            string invalidId = Booster.UserId;

            //Act
            bool result = await authorService.ExistsByUserIdAsync(invalidId);

            //Assert
            Assert.False(result);
        }

    }
}
