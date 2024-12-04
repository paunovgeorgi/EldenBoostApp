using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Application;
using EldenBoost.Core.Models.Platform;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Repository;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Tests.UnitTests
{
    [TestFixture]
    public class ApplicationServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private IApplicationService applicationService;

        [OneTimeSetUp]
        public void Setup()
        {
            repository = new Repository(data);
            applicationService = new ApplicationService(repository);
        }

        [Test]
        public async Task ApproveAuthorAsync_ShouldChangeIsApprovedToTrue()
        {
            //Arrange
            int applicationId = AuthorApplication.Id;

            //Assert current condition
            Assert.IsFalse(AuthorApplication.IsApproved);

            //Act
            await applicationService.ApproveAuthorAsync(applicationId);

            //Assert
            Assert.IsTrue(AuthorApplication.IsApproved);
        }

        [Test]
        public async Task ApproveBoosterAsync_ShouldChangeIsApprovedToTrue()
        {
            //Arrange
            int applicationId = BoosterApplication.Id;

            //Assert current condition
            Assert.IsFalse(BoosterApplication.IsApproved);

            //Act
            await applicationService.ApproveAuthorAsync(applicationId);

            //Assert
            Assert.IsTrue(BoosterApplication.IsApproved);
        }

        [Test]
        public async Task CreateAuthorApplicationAsync_ShouldSuccessfullyCreateAuthorNewApplication()
        {
            //Arrange
            int currentAuthorApplications = 1;
            int currentTotal = 2;
            int expectedTotal = 3;
            int expectedAuthorApplications = 2;
            string userId = "NewUserIdForApplication";
            var model = new ApplicationFormModel
            {
                Availabiliity = 9,
                Experience = "Written tons of articles",
                Country = "The best country"
            };

            //Assert current conditions
            Assert.That(data.Applications.Count, Is.EqualTo(currentTotal));
            Assert.That(data.Applications.Count(a => a.ApplicationType == Common.Enumerations.ApplicationType.Author), Is.EqualTo(currentAuthorApplications));

            //Act
            await applicationService.CreateAuthorApplicationAsync(userId, model);

            //Assert
            Assert.That(data.Applications.Count(), Is.EqualTo(expectedTotal));
            Assert.That(data.Applications.Count(a => a.ApplicationType == Common.Enumerations.ApplicationType.Author), Is.EqualTo(expectedAuthorApplications));
        }


        [Test]
        public async Task CreateBoosterApplicationAsync_ShouldSuccessfullyCreateBoosterNewApplication()
        {
            int currentBoosterApplications = 1;
            int currentTotal = 3;
            int expectedTotal = 4;
            int expectedBoosterApplications = 2;
            string userId = "NewUserIdForBoosterApplication";
            var model = new ApplicationFormModel
            {
                Availabiliity = 12,
                Experience = "10 years experience in professional gaming.",
                Country = "The best booster country"
            };
            var platform = new PlatformFormModel()
            {
                Id = 1,
                Name = "Pc",
                IsChecked = true,
            };
            model.SupportedPlatforms = new[] { platform };

            //Assert current conditions
            Assert.That(data.Applications.Count, Is.EqualTo(currentTotal));
            Assert.That(data.Applications.Count(a => a.ApplicationType == Common.Enumerations.ApplicationType.Booster), Is.EqualTo(currentBoosterApplications));

            //Act
            await applicationService.CreateBoosterApplicationAsync(userId, model);

            //Assert
            Assert.That(data.Applications.Count(), Is.EqualTo(expectedTotal));
            Assert.That(data.Applications.Count(a => a.ApplicationType == Common.Enumerations.ApplicationType.Booster), Is.EqualTo(expectedBoosterApplications));
        }

        [Test]
        public async Task RejectAsync_ShouldChangeIsRejected_ToTrue()
        {
            //Arrange
            AuthorApplication.IsApproved = false;
            int applictionId = AuthorApplication.Id;
            bool currentRejectedState = AuthorApplication.IsRejected;

            //Assert current conditions
            Assert.IsFalse(currentRejectedState);

            //Act
            await applicationService.RejectAsync(applictionId);

            //Assert
            Assert.IsTrue(AuthorApplication.IsRejected);

            //Cleanup
            AuthorApplication.IsRejected = false;
            AuthorApplication.IsApproved = true;
        }

        [Test]
        public async Task GetApplicationCountDataAsync_ShouldReturnCorrectData()
        {
            //Assert
            int expectedBoosterPending = 1;
            int expectedBoosterApproved = 1;
            int expectedAuthorPending = 1;
            int expectedAuthorApproved = 1;

            //Act
            var appData = await applicationService.GetApplicationCountDataAsync();

            //Assert
            Assert.That(appData.BoosterPending, Is.EqualTo(expectedBoosterPending));
            Assert.That(appData.BoosterApproved, Is.EqualTo(expectedBoosterApproved));
            Assert.That(appData.AuthorPending, Is.EqualTo(expectedAuthorPending));
            Assert.That(appData.AuthorApproved, Is.EqualTo(expectedAuthorApproved));
        }
    }
}
