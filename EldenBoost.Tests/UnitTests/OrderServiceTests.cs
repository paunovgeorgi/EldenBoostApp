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
    }
}
