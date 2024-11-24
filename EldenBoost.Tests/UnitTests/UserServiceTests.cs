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
    public class UserServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private IUserService userService;
        private IOrderService orderService;
        private IBoosterService boosterService;
        private IAuthorService authorService;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);
            orderService = new OrderService(repository);
            boosterService = new BoosterService(repository);
            authorService = new AuthorService(repository);  
            userService = new UserService(repository,boosterService, authorService, orderService);
        }

        [Test]
        public void AllAsync_ShouldReturn3Users()
        {
            int expectedCount = 3;
            var users = userService.AllAsync().Result;

            Assert.That(users.Count(), Is.EqualTo(expectedCount));
        }

        [Test]
        public void AllAsync_ReturnListShouldContainUser()
        {
            var users = userService.AllAsync().Result;
            Assert.That(users.Any(u => u.Nickname == User.Nickname));
        }

        [Test]
        public void GetProfilePictureAsync_ShouldRetrieveTheCorrectProfilePicture()
        {
            string expectedPic = User.ProfilePicture!;
            string retrievedPic = userService.GetProfilePictureByUseIdAsync(User.Id).Result;
            Assert.That(retrievedPic, Is.EqualTo(expectedPic));
        }

        [Test]
        public void GetProfilePictureAsync_ShouldRetrieveIncorrectProfilePicture()
        {
            string expectedPic = "WrongPicture";
            string retrievedPic = userService.GetProfilePictureByUseIdAsync(User.Id).Result;
            Assert.That(expectedPic, Is.Not.SameAs(retrievedPic));
        }

        [Test]
        public void ChangeProfilePicutre_ShouldWork()
        {
            string expectedPic = "NewClientPicture";
            userService.ChangeProfilePictureAsync(User.Id, expectedPic);
            Assert.That(User.ProfilePicture, Is.EqualTo(expectedPic));
        }

        [Test]
        public void GetUserNicknameAsync_ShouldReturnCorrectNickname()
        {
            string expected = User2.Nickname;
            string retrieved = userService.GetUserNicknameAsync("BoosterUserId").Result;
            Assert.That(expected, Is.EqualTo(retrieved));
        }

        [Test]
        public void HasOrdersAsync_ShouldReturn_True()
        {
            bool result = userService.HasOrdersAsync(User.Id).Result;
            Assert.IsTrue(result);
        }

        [Test]
        public void HasOrdersAsync_ShouldReturn_False()
        {
            bool result = userService.HasOrdersAsync(User2.Id).Result;
            Assert.IsFalse(result);
        }
    }
}
