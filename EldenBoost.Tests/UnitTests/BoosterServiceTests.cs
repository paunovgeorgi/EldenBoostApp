using EldenBoost.Core.Contracts;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Repository;

namespace EldenBoost.Tests.UnitTests
{
    [TestFixture]
    public class BoosterServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private IBoosterService boosterService;

        [OneTimeSetUp]
        public void Setup()
        {
            repository = new Repository(data);
            boosterService = new BoosterService(repository);
        }

        [Test]
        public async Task AllActiveBoostersToCardModelAsync_ShouldReturnNonEmptyList()
        {
            //Arrange
            int expectedCount = 1;

            //Act
            var boosters = await boosterService.AllActiveBoostersToCardModelAsync();

            //Assert
            Assert.IsNotEmpty(boosters);
            Assert.That(boosters.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task AllActiveBoostersToCardModelAsync_ShouldReturnEmptyList()
        {
            //Arrange
            Booster.IsDemoted = true;
            await data.SaveChangesAsync();

            //Act
            var boosters = await boosterService.AllActiveBoostersToCardModelAsync();

            //Assert
            Assert.IsEmpty(boosters);

            //Cleanup
            Booster.IsDemoted = false;
            await data.SaveChangesAsync();
        }

        [Test]
        public async Task BoosterExistsByUserIdAsync_ShouldReturnTrue_WithValidUserId()
        {
            //Arrange
            string userId = Booster.UserId;

            //Act
            bool result = await boosterService.BoosterExistsByUserIdAsync(userId);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task BoosterExistsByUserIdAsync_ShouldReturnFalse_WithInValidUserId()
        {
            //Arrange
            string userId = Author.UserId;

            //Act
            bool result = await boosterService.BoosterExistsByUserIdAsync(userId);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetBoosterByBoosterIdAsync_ShouldReturnCorrectBooster_WithValidBoosterId()
        {
            //Arrange
            int boosterId = Booster.Id;
            string expectedCountry = Booster.Country;
            double expectedRating = Booster.Rating;

            //Act
            var booster = await boosterService.GetBoosterByBoosterIdAsync(boosterId);

            //Assert
            Assert.IsNotNull(booster);
            Assert.That(booster.Country, Is.EqualTo(expectedCountry));
            Assert.That(booster.Rating, Is.EqualTo(expectedRating));
        }

        [Test]
        public async Task GetBoosterByBoosterIdAsync_ShouldReturnNull_WithIncorrectBoosterId()
        {
            //Arrange
            int incorrectId = 3000;

            //Act
            var booster = await boosterService.GetBoosterByBoosterIdAsync(incorrectId);

            //Assert
            Assert.IsNull(booster);
        }

        [Test]
        public async Task GetBoosterByUserIdAsync_ShouldReturnCorrectBooster_WithCorrectUserId()
        {
            //Arrange
            string userId = Booster.UserId;
            string expectedCountry = Booster.Country;
            double expectedRating = Booster.Rating;

            //Act
            var booster = await boosterService.GetBoosterByUserIdAsync(userId);

            //Assert
            Assert.IsNotNull(booster);
            Assert.That(booster.Country, Is.EqualTo(expectedCountry));
            Assert.That(booster.Rating, Is.EqualTo(expectedRating));
        }

        [Test]
        public async Task GetBoosterByUserIdAsync_ShouldReturnNull_WithIncorrectUserId()
        {
            //Arrange
            string incorrectUserId = Author.UserId;

            //Act
            var booster = await boosterService.GetBoosterByUserIdAsync(incorrectUserId);

            //Assert
            Assert.IsNull(booster);
        }

        [Test]
        public async Task GetBoosterIdAsync_ShouldReturnCorrectId()
        {
            //Arrange
            string userId = Booster.UserId;
            int expectedId = Booster.Id;

            //Act
            int result = await boosterService.GetBoosterIdAsync(userId);

            //Assert
            Assert.That(result, Is.EqualTo(expectedId));
        }

        [Test]
        public async Task GetBoosterIdAsync_ShouldReturnZero_WithIncorrectUserId()
        {
            //Arrange
            string incorrectId = Author.UserId;

            //Act
            int result = await boosterService.GetBoosterIdAsync(incorrectId);

            //Assert
            Assert.That(result, Is.EqualTo(0));
        }

        [Test]
        public async Task IsActiveAsync_ShouldReturnTrue_WhenBoosterIseDemoted_IsFalse()
        {
            //Arrange
            string boosterUserId = Booster.UserId;

            //Act
            bool result = await boosterService.IsActiveAsync(boosterUserId);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsActiveAsync_ShouldReturnFalse_WhenBoosterIsDemoted_IsTrue()
        {
            //Arrange
            string boosterUserId = Booster.UserId;
            Booster.IsDemoted = true;
            await data.SaveChangesAsync();

            //Act
            bool result = await boosterService.IsActiveAsync(boosterUserId);

            //Assert
            Assert.IsFalse(result);

            //Cleanup
            Booster.IsDemoted = false;
            await data.SaveChangesAsync();
        }

        [Test]
        public async Task RateAsync_ShouldWorkCorrectly()
        {
            //Arrange
            int currentRating = 0;
            int currentRatingCount = 0;
            int addedRating = 5;
            int expectedRating = 5;
            int expectedRatingCount = 1;
            int boosterId = Booster.Id;

            //Assert current conditions
            Assert.That(Booster.Rating, Is.EqualTo(currentRating));
            Assert.That(Booster.RatingCount, Is.EqualTo(currentRatingCount));

            //Act
            await boosterService.RateAsync(boosterId, addedRating);

            //Assert
            Assert.That(Booster.Rating, Is.EqualTo(expectedRating));
            Assert.That(Booster.RatingCount, Is.EqualTo(expectedRatingCount));

            //Cleanup
            Booster.Rating = 0;
            Booster.RatingCount = 0;
            await data.SaveChangesAsync();
        }
    }
}
