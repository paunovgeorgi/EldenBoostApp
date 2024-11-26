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
    }
}
