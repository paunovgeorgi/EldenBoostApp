using EldenBoost.Core.Contracts;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Repository;

namespace EldenBoost.Tests.UnitTests
{
    [TestFixture]
    public class PaymentServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private IPaymentService paymentService;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);
            paymentService = new PaymentService(repository);
        }

        [Test]
        public async Task CreatePaymentAsync_ShouldCreateNewPayment()
        {
            //Arrange
            string userId = Booster.UserId;
            int currentCount = 0;
            int expectedCount = 1;
            Assert.That(data.Payments.Count(), Is.EqualTo(currentCount));

            //Act
            await paymentService.CreatePaymentAsync(userId);

            //Assert
            Assert.That(expectedCount, Is.EqualTo(data.Payments.Count()), "After CreateReviewAsync reviews count should be = 2");
    }
}
