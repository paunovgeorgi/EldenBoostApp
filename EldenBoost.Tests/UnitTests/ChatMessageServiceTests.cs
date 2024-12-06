using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Chat;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;

namespace EldenBoost.Tests.UnitTests
{
    [TestFixture]
    public class ChatMessageServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private IChatMessageService chatMessageService;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);
            chatMessageService = new ChatMessageService(repository);
        }

        [Test]
        public async Task SaveMessageAsync_ShouldCreateAndSaveNewMessage()
        {
            //Arrange
            Order.BoosterId = Booster.Id;
            int currentCount = 1;
            int expectedCount = 2;

            var message = new ChatMessageViewModel
            {
                SenderId = Booster.UserId,
                ReceiverId = User.Id,
                Message = "Hello, I'll be your booster",
                OrderId = Order.Id,
                Timestamp = DateTime.Now
            };

            //Assert
            Assert.That(data.ChatMessages.Count, Is.EqualTo(currentCount));

            //Act
            await chatMessageService.SaveMessageAsync(message);

            //Assert
            Assert.IsNotEmpty(data.ChatMessages);
            Assert.That(data.ChatMessages.Count, Is.EqualTo(expectedCount));
        }

        [Test]
        public async Task GetChatViewModelAsync_ShouldReturnCorrectModel()
        {
            //Arrange
            Order.BoosterId = Booster.Id;
        
            //Act
            int orderId = Order.Id;
            string receiverId = User.Id;
            int expectedMessagesCount = 1;
            string expectedMessage = ChatMessage.Message;

            //Act
            var model = await chatMessageService.GetChatViewModelAsync(orderId, receiverId);

            //Assert
            Assert.IsNotNull(model);
            Assert.That(model.Messages.Count, Is.EqualTo(expectedMessagesCount));
            Assert.That(model.Messages.FirstOrDefault().Message, Is.EqualTo(expectedMessage));
        }
    }
}
