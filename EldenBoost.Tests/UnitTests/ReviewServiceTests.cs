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
    public class ReviewServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private IReviewService reviewService;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);
            reviewService = new ReviewService(repository);
        }

        [Test]
        public async Task CreateReviewAsync_ShouldCreateNewReview()
        {
            //Arrange
            string content = "This is my new review!";
            string userId = User.Id;
            int currentCount = 1;
            int expectedCount = 2;
            Assert.AreEqual(currentCount, data.Reviews.Count());

            //Act
            await reviewService.CreateReviewAsync(content, userId);

            //Assert
            Assert.That(expectedCount, Is.EqualTo(data.Reviews.Count()), "After CreateReviewAsync reviews count should be = 2");
        }

        [Test]
        public async Task GetLatestReviewsAsync_ShouldReturnCorrectOrderOfReviews()
        {
            //Arrange
            string content = "Latest Review that should be first in the list";
            await reviewService.CreateReviewAsync(content, User.Id);

            //Act
            var reviews = await reviewService.GetLatestReviewsAsync();

            //Assert
            Assert.That(content, Is.EqualTo(reviews.First().Content));
        }
    }
}
