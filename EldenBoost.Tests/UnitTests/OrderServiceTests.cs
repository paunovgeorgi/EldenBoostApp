using EldenBoost.Core.Contracts;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;

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

        [Test]
        public async Task AllOrdersFilteredAsync_ShouldReturnCorrectOrder()
        {
            //Arrange
            string expectedStatus = "Pending";
            int expectedAmount = 1;

            //Act
            var orders = await orderService.AllOrdersFilteredAsync(o => o.Status == expectedStatus);

            //Assert
            Assert.IsNotEmpty(orders);
            Assert.That(orders.Count(), Is.EqualTo(expectedAmount));
        }

        [Test]
        public async Task ArchiveAsync_ShouldWorkCorrectly()
        {
            //Arrange
            int orderId = Order.Id;

            //Assert
            Assert.IsFalse(Order.IsArchived);

            //Act
            await orderService.ArchiveAsync(orderId);

            //Assert
            Assert.IsTrue(Order.IsArchived);
        }

        [Test]
        public async Task AssignBoosterAsync_ShouldWorkCorrectly()
        {
            //Arrange
            int orderId = Order.Id;
            int boosterId = Booster.Id;

            Assert.IsNull(Order.BoosterId);

            //Act
            await orderService.AssignBoosterAsync(orderId, boosterId);

            //Assert
            Assert.IsNotNull(Order.BoosterId);
            Assert.That(Order.BoosterId, Is.EqualTo(boosterId));

            //Cleanup
            Order.BoosterId = null;
            Order.Status = "Pending";
            await data.SaveChangesAsync();
        }

        [Test]
        public async Task CompleteAsync_ShouldChangeOrderStatusTo_Completed()
        {
            //Arrange
            int orderId = Order.Id;
            string currentStatus = "Pending";
            string updatedStatus = "Completed";

            Assert.That(Order.Status, Is.EqualTo(currentStatus));

            //Act
            await orderService.CompleteAsync(orderId);

            //Assert
            Assert.That(Order.Status, Is.EqualTo(updatedStatus));

            //Cleanup
            Order.Status = "Pending";
            await data.SaveChangesAsync();
        }

        [Test]
        public async Task CreateOrdersFromCartAsync_ShouldCreateOrdersFromCartItems()
        {
            // Arrange
            Service.PurchaseCount = 1;
            Cart cart = new Cart
            {
                Id = 1,
                ClientId = User.Id
            };
            cart.CartItems.Add(CartItem);
            // Act
            await orderService.CreateOrdersFromCartAsync(cart.Id, User.Id);

            // Assert
            Assert.That(Service.PurchaseCount, Is.EqualTo(2));
            Assert.That(data.Orders.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task ExistsByIdAsync_ShouldReturnTrue_WithCorrectId()
        {
            //Arrange
            int orderId = Order.Id;

            //Act
            bool exists = await orderService.ExistsByIdAsync(orderId);

            //Assert
            Assert.IsTrue(exists);
        }

        [Test]
        public async Task ExistsByIdAsync_ShouldReturnFalse_WithIncorrectId()
        {
            //Arrange
            int orderId = 100;

            //Act
            bool exists = await orderService.ExistsByIdAsync(orderId);

            //Assert
            Assert.IsFalse(exists);
        }

        [Test]
        public async Task GetOrderCountDataAsync_ShouldReturnCorrectData()
        {
            //Arrange
            int pending = 2;
            int working = 0;
            int completed = 0;
            int total = 2;

            //Act
            var orderData = await orderService.GetOrderCountDataAsync();

            //Assert
            Assert.That(orderData.Pending, Is.EqualTo(pending));
            Assert.That(orderData.Working, Is.EqualTo(working));
            Assert.That(orderData.Completed, Is.EqualTo(completed));
            Assert.That(orderData.Total, Is.EqualTo(total));

            Order.Status = "Completed";
            await data.SaveChangesAsync();

            var newData = await orderService.GetOrderCountDataAsync();

            Assert.That(newData.Completed, Is.EqualTo(1));

            //Cleanup
            Order.Status = "Pending";
            await data.SaveChangesAsync();
        }

        [Test]
        public async Task GetOrderDetailsAsync_ShouldReturnCorrectOrderDetails()
        {
            //Arrange
            int orderId = Order.Id;
            string expectedServiceTitle = Order.Service.Title;
            decimal expectedBoosterPay = Order.BoosterPay;

            //Act
            var order = await orderService.GetOrderDetailsAsync(orderId);

            //Assert
            Assert.IsNotNull(order);
            Assert.That(order.ServiceName, Is.EqualTo(expectedServiceTitle));
            Assert.That(order.BoosterPay, Is.EqualTo(expectedBoosterPay));
        }

        [Test]
        public async Task GetOrderDetailsAsync_ReturnsNull_WithInvalid()
        {
            //Arrange
            int invalidId = 100;

            //Act
            var order = await orderService.GetOrderDetailsAsync(invalidId);

            //Assert
            Assert.IsNull(order);
        }

        [Test]
        public async Task GetOrderWithBoosterByOrderIdAsync_ShouldWorkCorrectly()
        {
            //Arrange
            Booster.Orders.Add(Order);
            Order.BoosterId = Booster.Id;
            await data.SaveChangesAsync();

            //Act
            var order = await orderService.GetOrderWithBoosterByOrderIdAsync(Order.Id);

            //Assert
            Assert.IsNotNull(order);
            Assert.IsNotNull(order.Booster);
            Assert.That(order.BoosterId, Is.EqualTo(Booster.Id));
            Assert.That(order.Id, Is.EqualTo(Order.Id));

            //CleanUp
            Booster.Orders.Remove(Order);
            Order.BoosterId = null;
            await data.SaveChangesAsync();
        }

        [Test]
        public async Task GetOrderWithBoosterByOrderIdAsync_ShouldReturnNull_WithInvalidId()
        {
            //Arrange
            int invalidId = 100;

            //Act
            var order = await orderService.GetOrderWithBoosterByOrderIdAsync(invalidId);

            //Assert
            Assert.IsNull(order);
        }

        [Test]
        public async Task GetOrderWithClientByIdAsync_ShouldWorkCorrectly()
        {
            //Arrange
            int orderId = Order.Id;
            string expectedClientId = User.Id;

            //Act
            var order = await orderService.GetOrderWithClientByIdAsync(orderId);

            //Assert
            Assert.IsNotNull(order);
            Assert.IsNotNull(order.Client);
            Assert.That(order.Client.Id, Is.EqualTo(expectedClientId));
        }

        [Test]
        public async Task GetOrderWithClientByIdAsync_ShouldReturnNull_WithInvalidOrderId()
        {
            //Arrange
            int invalidId = 100;

            //Act
            var order = await orderService.GetOrderWithClientByIdAsync(invalidId);

            //Assert
            Assert.IsNull(order);
        }

        [Test]
        public async Task HasBoosterWithIdAsync_ShouldReturnTrue()
        {
            //Arrange
            Order.BoosterId = Booster.Id;
            Booster.Orders.Add(Order);
            await data.SaveChangesAsync();

            //Act
            var result = await orderService.HasBoosterWithIdAsync(Order.Id, Booster.Id);

            //Assert
            Assert.IsTrue(result);

            //Cleanup
            Booster.Orders.Remove(Order);
            Order.BoosterId = null;
            await data.SaveChangesAsync();
        }

        [Test]
        public async Task HasBoosterWithIdAsync_ShouldReturnFalse_WithValidOrderId_WithoutExpectedBooster()
        {
            //Arrange
            int orderId = Order.Id;
            int boosterId = Booster.Id;

            //Act
            var result = await orderService.HasBoosterWithIdAsync(orderId, boosterId);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task HasBoosterWithIdAsync_ShouldReturnFalse_WithInvalidOrderId()
        {
            //Arrange
            int invalidOrderId = 100;
            int boosterId = Booster.Id;

            //Act
            var result = await orderService.HasBoosterWithIdAsync(invalidOrderId, Booster.Id);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task HasBoosterWithIdAsync_ShouldReturnFalse_WithValidOrderId_AndInvalidBoosterId()
        {
            //Arrange
            Order.BoosterId = Booster.Id;
            await data.SaveChangesAsync();
            int orderId = Order.Id;
            int invalidBoosterId = 100;

            //Act
            var result = await orderService.HasBoosterWithIdAsync(orderId, invalidBoosterId);

            //Assert
            Assert.IsFalse(result);

            //Cleanup
            Order.BoosterId = null;
        }

        [Test]
        public async Task IsTakenAsync_ShouldReturnTrue_WithCorrectOrderId()
        {
            //Arrange
            Order.BoosterId = Booster.Id;
            int orderId = Order.Id;

            //Act
            bool result = await orderService.IsTakenAsync(Order.Id);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsTakenAsync_ShouldReturnFalse_WithOrderWithoutBooster()
        {
            //Arrange
            int orderId = Order.Id;

            //Act
            bool result = await orderService.IsTakenAsync(orderId);

            //Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task NumberOfOrdersByClientIdAsync_ShouldWorkCorrectly()
        {
            //Arrange
            int expected = 2;
            string clientId = User.Id;
            string check = Order.ClientId;

            //Act
            int result = await orderService.NumberOfOrdersByClientIdAsync(clientId);

            //Assert
            Assert.That(result, Is.EqualTo(expected));

            //Arrange
            Order.ClientId = "differentClient";
            await data.SaveChangesAsync();
            int modifiedExpected = 1;

            //Act
            int modifiedResult = await orderService.NumberOfOrdersByClientIdAsync(clientId);

            //Assert
            Assert.That(modifiedExpected, Is.EqualTo(modifiedResult));

            //Cleanup
            Order.ClientId = User.Id;
            await data.SaveChangesAsync();
        }

        [Test]
        public async Task TotalPaidByClientIdAsync_ShouldReturnCorrectAmount()
        {
            //Arrange
            
            decimal expected = 600;
            string clienId = User.Id;

            //Act
            decimal result = await orderService.TotalPaidByClientIdAsync(clienId);

            //Assert
            Assert.That(result, Is.EqualTo(expected));

        }

        [Test]
        public async Task GetOrderCountDataAsync_ShouldReturnCorrectOrdersData()
        {
            //Arrange
            Order.Status = "Completed";
            await data.SaveChangesAsync();
            int expectedPending = 1;
            int expectedWorking = 0;
            int expectedCompleted = 1;
            int expectedTotal = 2;

            //Act
            var orderData = await orderService.GetOrderCountDataAsync();

            //Assert
            Assert.That(orderData.Pending, Is.EqualTo(expectedPending));
            Assert.That(orderData.Working, Is.EqualTo(expectedWorking));
            Assert.That(orderData.Completed, Is.EqualTo(expectedCompleted));
            Assert.That(orderData.Total, Is.EqualTo(expectedTotal));

            //Cleanup
            Order.Status = "Pending";
            await data.SaveChangesAsync();
        }
    }
}
