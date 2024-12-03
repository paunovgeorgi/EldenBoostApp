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

        [Test]
        public async Task GetAuthorByUserIdAsync_ShouldReturnCorrectAuthor()
        {
            //Arrange
            string validUserId = Author.UserId;
            string expectedCountry = Author.Country;
            int expectedAuthorId = Author.Id;

            //Act
            var author = await authorService.GetAuthorByUserIdAsync(validUserId);

            //Assert
            Assert.NotNull(author);
            Assert.That(author.Country, Is.EqualTo(expectedCountry));
            Assert.That(author.Id, Is.EqualTo(expectedAuthorId));
        }

        [Test]
        public async Task GetAuthorByUserIdAsync_ShouldReturnNull_WithInvalidUserId()
        {
            //Arrange
            string invalidUserId = User.Id;

            //Act
            var author = await authorService.GetAuthorByUserIdAsync(invalidUserId);

            //Assert
            Assert.IsNull(author);
        }

        [Test]
        public async Task HasArticleAsync_ShouldReturnTrue_WithValidUserAndArticleIds()
        {
            //Arrange
            string authorUserId = Author.UserId;
            int articleId = Article.Id;

            //Act
            bool result = await authorService.HasArticleAsync(authorUserId, articleId);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task HasArticleAsync_ShouldReturnFalse_WithInvalidUserIdAndValidArticleId()
        {
            //Arrange
            string invalidUserId = User.Id;
            int articleId = Article.Id;

            //Act
            bool result = await authorService.HasArticleAsync(invalidUserId, articleId);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task HasArticleAsync_ShouldReturnFalse_WithValidUserId_WithNoArticles()
        {
            //Arrange
            string authorUserId = Author.UserId;
            int articleId = Article.Id;
            Article.AuthorId = 200;
            await data.SaveChangesAsync();

            //Act
            bool result = await authorService.HasArticleAsync(authorUserId, articleId);

            //Assert
            Assert.IsFalse(result);

            //Cleanup
            Article.AuthorId = Author.Id;
            await data.SaveChangesAsync();
        }

        [Test]
        public async Task IsActiveAsync_ShouldReturnTrue_WhenAuthorIseDemoted_IsFalse()
        {
            //Arrange
            string authorUserId = Author.UserId;

            //Act
            bool result = await authorService.IsActiveAsync(authorUserId);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsActiveAsync_ShouldReturnFalse_WhenAuthorIsDemoted_IsTrue()
        {
            //Arrange
            string authorUserId = Author.UserId;
            Author.IsDemoted = true;
            await data.SaveChangesAsync();

            //Act
            bool result = await authorService.IsActiveAsync(authorUserId);

            //Assert
            Assert.IsFalse(result);

            //Cleanup
            Author.IsDemoted = false;
            await data.SaveChangesAsync();
        }
    }
}
