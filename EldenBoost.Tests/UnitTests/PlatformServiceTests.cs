using EldenBoost.Core.Contracts;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Repository;

namespace EldenBoost.Tests.UnitTests
{
    [TestFixture]
    public class PlatformServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private IPlatformService platformService;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);
            platformService = new PlatformService(repository);
        }

        [Test]
        public async Task GetAllPlatformsAsync_ShouldGetTheCorrectPlatforms()
        {
            //Arrange
            int expectedCount = 1;
            string expectedName = Platform.Name;
            int expectedId = Platform.Id;

            //Act
            var platforms = await platformService.GetAllPlatformsAsync();

            //Assert
            Assert.That(expectedCount, Is.EqualTo(platforms.Count()), "platforms list should contain only 1 platform");
            Assert.That(expectedName, Is.EqualTo(platforms.First().Name), "The name of the retrieved platform should match the expected name");
            Assert.That(expectedId, Is.EqualTo(platforms.First().Id), "Platform id of the retrieved platform should match the expected id");
        }

    }
}
