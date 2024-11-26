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


    }
}
