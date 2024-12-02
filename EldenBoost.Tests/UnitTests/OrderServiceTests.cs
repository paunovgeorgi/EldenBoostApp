using EldenBoost.Core.Contracts;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Tests.UnitTests
{
    [TestFixture]
    public class OrderServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private IOrderService orderService;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);
            orderService = new OrderService(repository);
        }

        [Test]
        public async Task AllByBoosterIdAsync_ShouldReturnCorrectOrders()
        {
            //Arrange
            int boosterId = Booster.Id;

            //Act
            var orders = await orderService.AllByBoosterIdAsync(boosterId);

            //Assert
            Assert.IsEmpty(orders);

            //Arrange
            Order.BoosterId = boosterId;
            await data.SaveChangesAsync();

            //Act
            var newOrders = await orderService.AllByBoosterIdAsync(boosterId);

            //Assert
            Assert.IsNotEmpty(newOrders);

            //Cleanup
            Order.BoosterId = null;
            await data.SaveChangesAsync();
        }

        [Test]
        public async Task AllByUserIdAsync_ShouldReturnCorrectOrders()
        {
            //Arrange
            string userId = User.Id;
            int expected = 1;

            //Act
            var orders = await orderService.AllByUserIdAsync(userId);

            //Assert
            Assert.IsNotEmpty(orders);
            Assert.That(orders.Count(), Is.EqualTo(expected));
        }


        [Test]
        public async Task AllOrdersAsync_ShouldReturnCorrectOrders()
        {
            //Arrange
            string userId = User.Id;
            int expected = 1;

            //Act
            var orders = await orderService.AllOrdersAsync();

            //Assert
            Assert.IsNotEmpty(orders);
            Assert.That(orders.Count(), Is.EqualTo(expected));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCorrectOrder()
        {
            //Arrange
            int orderId = Order.Id;
            string expectedStatus = "Pending";
            string expectedClientId = User.Id;
            string information = CartItem.Information!;

            //Act
            var order = await orderService.GetOrderByIdAsync(orderId);

            //Assert
            Assert.IsTrue(order != null);
            Assert.IsTrue(!string.IsNullOrEmpty(information));
            Assert.That(order!.Information, Is.EqualTo(information));
            Assert.That(order.ClientId, Is.EqualTo(expectedClientId));
            Assert.That(order.Status, Is.EqualTo(expectedStatus));
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnNull_WithWrongId()
        {
            //Arrange
            int orderId = 3500;

            //Act
            var order = await orderService.GetOrderByIdAsync(orderId);

            //Assert
            Assert.IsNull(order);
        }

    }
}
