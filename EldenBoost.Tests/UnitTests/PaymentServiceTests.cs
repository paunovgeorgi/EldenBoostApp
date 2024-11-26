using EldenBoost.Core.Contracts;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Models;
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

        [Test]
        public async Task ExistsByIdAsync_ShoudReturnTrue_WithCorrecId()
        {
            //Arrange
            int correctId = 1;

            //Act
            bool result = await paymentService.ExistsByIdAsync(correctId);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsByIdAsync_ShoudReturnFalse_WithIncorrecId()
        {
            //Arrange
            int incorrectId = 100;

            //Act
            bool result = await paymentService.ExistsByIdAsync(incorrectId);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task HasOrdersToRequestAsync_ShouldReturnTrue()
        {
            //Arrange
            Order.BoosterId = Booster.Id;
            Order.Status = "Completed";
            string boosterUserId = Booster.UserId;
            Booster.Orders.Add(Order);
            await data.SaveChangesAsync();

            //Act
            bool result = await paymentService.HasOrdersToRequestAsync(boosterUserId);

            //Assert
            Assert.IsTrue(result);

            //Cleanup
            Order.Status = "Pending";
        }

        [Test]
        public async Task HasOrdersToRequestAsync_ShouldReturnFalse()
        {
            //Arrange
            string boosterUserId = Booster.UserId;

            //Act
            bool result = await paymentService.HasOrdersToRequestAsync(boosterUserId);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task IsPendingAsync_ShouldReturnTrue()
        {
            //Arrange
            string boosterUserId = Booster.UserId;

            //Act
            bool result = await paymentService.IsPendingAsync(boosterUserId);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsPendingAsync_ShouldReturnFalse_WithNoUnpaidPayment()
        {
            //Arrange
            Booster.Payments.First().IsPaid = true;
            await data.SaveChangesAsync();
            string boosterUserId = Booster.UserId;

            //Act
            bool result = await paymentService.IsPendingAsync(boosterUserId);

            //Assert
            Assert.IsFalse(result);

            //Cleanup
            Booster.Payments.First().IsPaid = false;
            await data.SaveChangesAsync();
        }


        [Test]
        public async Task IsPendingAsync_ShouldReturnFalse_WithWrongUserId()
        {
            //Arrange
            string boosterUserId = "WrongUserId";

            //Act
            bool result = await paymentService.IsPendingAsync(boosterUserId);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task PayAsync_ShouldWorkCorrectly()
        {
            //Arrange
            Payment payment = Booster.Payments.First();
            Assert.IsFalse(payment.IsPaid);

            //Act
            await paymentService.PayAsync(payment.Id);

            //Assert
            Assert.IsTrue(payment.IsPaid);

            //Cleanup
            payment.IsPaid = false;
        }

        [Test]
        public async Task ReadyForRequstAsync_ShoudReturnCorrectAmount()
        {
            //Arrange
            Order.Status = "Completed";
            Order.BoosterId = Booster.Id;
            await data.SaveChangesAsync();
            string boosterUserId = Booster.UserId;
            decimal expectedAmount = Booster.Orders.Where(o => o.Booster!.UserId == boosterUserId &&
            o.IsPaid == false && o.Status == "Completed" && o.IsAddedToPayment == false)
                .Sum(o => o.BoosterPay);

            //Act
            decimal result = await paymentService.ReadyForRequstAsync(boosterUserId);

            //Assert
            Assert.That(expectedAmount, Is.EqualTo(result));

            //Cleanup
            Order.Status = "Pending";
        }

        [Test]
        public async Task RequestedAmountAsync_ShouldReturnCorrectAmount()
        {
            //Arrange
            string boosterUserId = Booster.UserId;
            decimal expectedAmounnt = Booster.Payments.First().Amount;

            //Act
            decimal result = await paymentService.RequestedAmountAsync(boosterUserId);

            //Assert
            Assert.That(expectedAmounnt, Is.EqualTo(result));
        }
    }
}
