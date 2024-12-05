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
    public class CartServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private ICartService cartService;

        [OneTimeSetUp]
        public void Setup()
        {
            repository = new Repository(data);
            cartService = new CartService(repository);
        }

        [Test]
        public async Task AddToCartAsync_ShouldWorkProperly()
        {
            //Arrange
            string userId = User.Id;
            int serviceId = Service2.Id;
            int platformId = Platform.Id;
            decimal? updatedPrice = 25m;
            bool? hasStream = true;
            bool? isExpress = false;
            int? optionId = null;
            int sliderValue = 10;

            int currentCount = 0;
            int expectedCount = 1;

            //Assert current conditions
            Assert.IsEmpty(Cart.CartItems);

            //Act
            await cartService.AddToCartAsync(userId, serviceId, platformId, updatedPrice, hasStream, isExpress, optionId, sliderValue);
            
            //Assert
            Assert.IsNotEmpty(Cart.CartItems);
            Assert.That(Cart.CartItems.FirstOrDefault().Price, Is.EqualTo(updatedPrice));
        }
    }
}
