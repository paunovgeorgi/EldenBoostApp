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
    }
}
