using EldenBoost.Core.Contracts;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Repository;

namespace EldenBoost.Tests.UnitTests
{
    [TestFixture]
    public class UserServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private IUserService userService;
        private IOrderService orderService;
        private IBoosterService boosterService;
        private IAuthorService authorService;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);
            orderService = new OrderService(repository);
            boosterService = new BoosterService(repository);
            authorService = new AuthorService(repository);
            userService = new UserService(repository, boosterService, authorService, orderService);
        }

        [Test]
        public async Task AllAsync_ShouldReturn3Users()
        {
            // Arrange
            int expectedCount = 3;

            // Act
            var users = await userService.AllAsync();

            // Assert
            Assert.That(users.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task AllAsync_ReturnListShouldContainUser()
        {
            // Arrange & Act
            var users = await userService.AllAsync();

            // Assert
            Assert.That(users.Any(u => u.Nickname == User.Nickname));
        }

        [Test]
        public async Task GetProfilePictureAsync_ShouldRetrieveTheCorrectProfilePicture()
        {
            // Arrange
            string expectedPic = User.ProfilePicture!;

            // Act
            string retrievedPic = await userService.GetProfilePictureByUseIdAsync(User.Id);

            // Assert
            Assert.That(retrievedPic, Is.EqualTo(expectedPic));
        }

        [Test]
        public async Task GetProfilePictureAsync_ShouldRetrieveIncorrectProfilePicture()
        {
            // Arrange
            string expectedPic = "WrongPicture";

            // Act
            string retrievedPic = await userService.GetProfilePictureByUseIdAsync(User.Id);

            // Assert
            Assert.That(expectedPic, Is.Not.SameAs(retrievedPic));
        }

        [Test]
        public async Task ChangeProfilePicture_ShouldWork()
        {
            // Arrange
            string expectedPic = "NewClientPicture";

            // Act
            await userService.ChangeProfilePictureAsync(User.Id, expectedPic);

            // Assert
            Assert.That(User.ProfilePicture, Is.EqualTo(expectedPic));
        }

        [Test]
        public async Task GetUserNicknameAsync_ShouldReturnCorrectNickname()
        {
            // Arrange
            string expected = User2.Nickname;

            // Act
            string retrieved = await userService.GetUserNicknameAsync("BoosterUserId");

            // Assert
            Assert.That(expected, Is.EqualTo(retrieved));
        }

        [Test]
        public async Task HasOrdersAsync_ShouldReturn_True()
        {
            // Arrange & Act
            bool result = await userService.HasOrdersAsync(User.Id);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task HasOrdersAsync_ShouldReturn_False()
        {
            // Arrange & Act
            bool result = await userService.HasOrdersAsync(User2.Id);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task DemoteAsync_ShouldWorkCorrectlyWithBooster()
        {
            // Arrange
            Booster.IsDemoted = false;

            // Act
            await userService.DemoteAsync(Booster.UserId);

            // Assert
            Assert.IsTrue(Booster.IsDemoted);
        }

        [Test]
        public async Task DemoteAsync_ShouldWorkCorrectlyWithAuthor()
        {
            // Arrange
            Author.IsDemoted = false;

            // Act
            await userService.DemoteAsync(Author.UserId);

            // Assert
            Assert.IsTrue(Author.IsDemoted);
        }

        [Test]
        public async Task ReinstateAsync_ShouldWorkCorrectlyWithBooster()
        {
            // Arrange
            Booster.IsDemoted = true;

            // Act
            await userService.ReinstateAsync(Booster.UserId);

            // Assert
            Assert.IsFalse(Booster.IsDemoted);
        }

        [Test]
        public async Task ReinstateAsync_ShouldWorkCorrectlyWithAuthor()
        {
            // Arrange
            Author.IsDemoted = true;

            // Act
            await userService.ReinstateAsync(Author.UserId);

            // Assert
            Assert.IsFalse(Author.IsDemoted);
        }

        [Test]
        public async Task GetUserCountDataAsync_ShouldReturnCorrectData()
        {
            //Arrange
            int expectedClients = 1;
            int expectedBoosters = 1;
            int expectedAuthors = 1;
            int expectedTotal = 3;

            //Act
            var data = await userService.GetUserCountDataAsync();

            //Assert
            Assert.That(data.Clients, Is.EqualTo(expectedClients));
            Assert.That(data.Boosters, Is.EqualTo(expectedBoosters));
            Assert.That(data.Authors, Is.EqualTo(expectedAuthors));
            Assert.That(data.Total, Is.EqualTo(expectedTotal));
        }
    }
}

