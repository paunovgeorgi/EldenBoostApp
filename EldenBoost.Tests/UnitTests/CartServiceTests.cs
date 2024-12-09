using EldenBoost.Core.Contracts;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Repository;

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
            Assert.That(Cart.CartItems.Count(), Is.EqualTo(currentCount));

            //Act
            await cartService.AddToCartAsync(userId, serviceId, platformId, updatedPrice, hasStream, isExpress, optionId, sliderValue);
            
            //Assert
            Assert.IsNotEmpty(Cart.CartItems);
            Assert.That(Cart.CartItems.Count(), Is.EqualTo(expectedCount));

            Assert.That(Cart.CartItems.FirstOrDefault().Price, Is.EqualTo(updatedPrice));
        }

        [Test]
        public async Task ClearCartAsync_ShouldWorkCorrectly()
        {
            //Arrange
            int cartId = Cart.Id;
            int currentCount = 1;
            int expectedCount = 0;

            //Assert current
            Assert.IsNotEmpty(Cart.CartItems);
            Assert.That(Cart.CartItems.Count(), Is.EqualTo(currentCount));

            //Act
            await cartService.ClearCartAsync(cartId);

            //Assert
            Assert.IsEmpty(Cart.CartItems);
            Assert.That(Cart.CartItems.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task GetCartIdAsync_ShouldReturnCorrectId()
        {
            //Arrange
            string userId = User.Id;
            int expectedCartId = Cart.Id;

            //Act 
            int result = await cartService.GetCartIdAsync(userId);

            //Assert
            Assert.That(result, Is.EqualTo(expectedCartId));
        }

        [Test]
        public async Task GetCartIdAsync_ShouldReturnZero_WithWrongUserId()
        {
            //Arrange
            string userId = Booster.UserId;
            int expectedResult = 0;

            //Act 
            int result = await cartService.GetCartIdAsync(userId);

            //Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public async Task GetCartQuantityByUserIdAsync_ShouldReturnCorrectQuantity()
        {
            //Arrange
            string userId = User.Id;
            int expectedQuantity = 1;

            //Assert current conditions
            var current = await cartService.GetCartQuantityByUserIdAsync(userId);
            Assert.That(current, Is.Zero);

            //Act
            //await cartService.AddToCartAsync(userId, serviceId, platformId, updatedPrice, hasStream, isExpress, optionId, sliderValue);
            Cart.CartItems.Add(CartItem);
            await data.SaveChangesAsync();
            var result = await cartService.GetCartQuantityByUserIdAsync(userId);

            //Assert
            Assert.That(result, Is.EqualTo(expectedQuantity));
        }

        [Test]
        public async Task GetCartViewModelAsync_ShouldReturnCorrectModel()
        {
            //Arrange
            string userId = User.Id;
            decimal expectedPrice = CartItem.Price;
            int expectedCount = 1;

            //Act
            var model = await cartService.GetCartViewModelAsync(userId);

            //Assert
            Assert.That(model.TotalPrice, Is.EqualTo(expectedPrice));
            Assert.That(model.CartItems.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task RemoveItemAsync_ShouldRemoveItemFromDB()
        {
            //Arrange
            int cartItemId = CartItem.Id;


            //Assert current conditions
            var item = await data.CartItems.FindAsync(cartItemId);
            Assert.IsNotNull(item);

            //Act
            await cartService.RemoveItemAsync(cartItemId);

            //Assert
            var removedItem = await data.CartItems.FindAsync(cartItemId);
            Assert.IsNull(removedItem);
        }
    }
}
