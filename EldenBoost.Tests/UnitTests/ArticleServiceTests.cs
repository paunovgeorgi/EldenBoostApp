using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Article;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EldenBoost.Tests.UnitTests
{
    [TestFixture]
    public class ArticleServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private IArticleService articleService;

        [OneTimeSetUp]
        public void Setup()
        {
            repository = new Repository(data);
            articleService = new ArticleService(repository);    
        }

        [Test]
        public async Task AllByAuthorIdAsync_ShouldReturnCorrectList()
        {
            //Arrange
            int expectedCount = 1;
            int authorId = Author.Id;

            //Act
            var articles = await articleService.AllByAuthorIdAsync(authorId);

            //Assert
            Assert.IsNotEmpty(articles);
            Assert.That(articles.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task CreateAsync_ShouldCreateNewArticle()
        {
            //Arrange
            string userId = Author.UserId;
            int currentCount = 1;
            int expectedCount = 2;
            var model = new ArticleFormModel
            {
                Content = "This is my article content and I hope it's long enough to be accepted.<script>harmful stuff</script>",
                ArticleType = Common.Enumerations.ArticleType.Guide,
                Title = "My newly created article",
                ImageURL = "Article Image"
            };

            //Assert current conditions
            Assert.That(data.Articles.Count(), Is.EqualTo(currentCount));

            //Act
            await articleService.CreateAsync(model, userId);

            //Assert
            Assert.That(data.Articles.Count(), Is.EqualTo(expectedCount));

            //Arrange
            var article = await data.Articles.FirstOrDefaultAsync(a => a.Title == "My newly created article");
            string expectedContent = "This is my article content and I hope it's long enough to be accepted.";

            //Assert content sanitization
            Assert.That(article!.Content, Is.EqualTo(expectedContent));
        }

        [Test]
        public async Task EditArticleAsync_ShouldWorkCorrectly()
        {
            //Arrange
            var model = new ArticleEditViewModel
            {
                Id = Article.Id,
                Content = Article.Content,
                ArticleType = Article.ArticleType,
                Title = "Toughest Bosses in Elden Ring - Edited",
                ImageURL = Article.ImageURL
            };

            string currentTitle = "Toughest Bosses in Elden Ring";
            string expecterTitle = model.Title;

            //Assert current conditions
            Assert.That(Article.Title, Is.EqualTo(currentTitle));

            //Act
            await articleService.EditArticleAsync(model);

            //Assert
            Assert.That(Article.Title, Is.EqualTo(expecterTitle));

            //Cleanup
            Article.Title = currentTitle;
            await data.SaveChangesAsync();
        }

        [Test]
        public async Task GetAllArticlesListViewModelAsync_ShouldReturnCorrectList()
        {
            //Arrange
            int expectedCount = 2;

            //Act
            var articles = await articleService.GetAllArticlesListViewModelAsync();

            //Assert
            Assert.IsNotEmpty(articles);
            Assert.That(articles.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task GetArticleCountDataAsync_ShouldReturnCorrectData()
        {
            //Arrange
            int expectedNews = 1;
            int expectedGuides = 1;

            //Act
            var articleData = await articleService.GetArticleCountDataAsync();

            //Assert
            Assert.That(articleData.News, Is.EqualTo(expectedNews));
            Assert.That(articleData.Guides, Is.EqualTo(expectedGuides));
        }

        [Test]
        public async Task GetArticleEditModelByIdAsync_ShouldReturnCorrectModel()
        {
            //Arrange
            var expectedModel = new ArticleEditViewModel
            {
                Id = Article.Id,
                Content = Article.Content,
                ArticleType = Article.ArticleType,
                Title = Article.Title,
                ImageURL = Article.ImageURL
            };
            int articleId = Article.Id;

            //Act
            var editModel = await articleService.GetArticleEditModelByIdAsync(articleId);

            //Assert
            Assert.IsNotNull(editModel);
            Assert.That(editModel.Id, Is.EqualTo(expectedModel.Id));
            Assert.That(editModel.Content, Is.EqualTo(expectedModel.Content));
            Assert.That(editModel.ArticleType, Is.EqualTo(expectedModel.ArticleType));
            Assert.That(editModel.Title, Is.EqualTo(expectedModel.Title));
            Assert.That(editModel.ImageURL, Is.EqualTo(expectedModel.ImageURL));
        }

        [Test]
        public async Task GetArticleReadModelAsync_ShouldReturnCorrectModel()
        {
            //Arrange
            var expectedModel = new ArticleReadViewModel
            {
                Id = Article.Id,
                Title = Article.Title,
                Content = Article.Content,
                ImgUrl = Article.ImageURL,
                PublishDate = Article.ReleaseDate.ToString("d"),
                Author = Article.Author.User.Nickname
            };
            int articleId = Article.Id;

            //Act
            var readModel = await articleService.GetArticleReadModelAsync(articleId);

            //Assert
            Assert.IsNotNull(readModel);
            Assert.That(readModel.Id, Is.EqualTo(expectedModel.Id));
            Assert.That(readModel.Title, Is.EqualTo(expectedModel.Title));
            Assert.That(readModel.Content, Is.EqualTo(expectedModel.Content));
            Assert.That(readModel.ImgUrl, Is.EqualTo(expectedModel.ImgUrl));
            Assert.That(readModel.PublishDate, Is.EqualTo(expectedModel.PublishDate));
            Assert.That(readModel.Author, Is.EqualTo(expectedModel.Author));
        }


    }
}
