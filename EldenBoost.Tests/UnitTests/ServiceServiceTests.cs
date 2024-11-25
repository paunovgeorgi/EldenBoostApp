using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldenBoost.Tests.UnitTests
{
    [TestFixture]
    public class ServiceServiceTests : UnitTestsBase
    {
        private IRepository repository;
        private IServiceService serviceService;

        [OneTimeSetUp]
        public void SetUp()
        {
            repository = new Repository(data);
            serviceService = new ServiceService(repository);
        }

        [Test]
        public async Task ActivateByIdAsync_SetsIsActive_ToTrue()
        {
            // Arrange
            Service.IsActive = false;

            // Act
            await serviceService.ActivateByIdAsync(Service.Id);

            // Assert
            Assert.IsTrue(Service.IsActive);
        }

        [Test]
        public async Task AllAsync_ReturnsCorrect_NumberOfServices()
        {
            // Arrange
            int expected = 3;
            var model = new AllServicesQueryModel
            {
                SearchString = string.Empty,
                CurrentPage = 1,
                ServicesPerPage = 3,
                TotalServices = 3,
                ServiceSorting = Core.Models.Service.Enums.ServiceSorting.Newest,
            };

            // Act
            var services = await serviceService.AllAsync(model);

            // Assert
            Assert.That(expected, Is.EqualTo(services.TotalServicesCount));
        }

        [Test]
        public async Task AllAsync_SearchString_ReturnsCorrectServices()
        {
            // Arrange
            int expected = 1;
            var model = new AllServicesQueryModel
            {
                SearchString = "Desired amount",
                CurrentPage = 1,
                ServicesPerPage = 1,
                TotalServices = 1,
                ServiceSorting = Core.Models.Service.Enums.ServiceSorting.Newest,
            };

            // Act
            var services = await serviceService.AllAsync(model);

            // Assert
            Assert.That(expected, Is.EqualTo(services.TotalServicesCount));
        }

        [Test]
        public async Task AllServiceListViewModelFilteredAsync_ShouldReturnCorrectServices()
        {
            // Arrange
            int expectedNumber = 1;

            // Act
            var services = await serviceService.AllServiceListViewModelFilteredAsync(s => s.ServiceType == Common.Enumerations.ServiceType.Slider);

            // Assert
            Assert.That(expectedNumber, Is.EqualTo(services.Count()));
        }

        [Test]
        public async Task CreateServiceAsync_ShouldWorkProperly()
        {
            // Arrange
            ServiceFormViewModel model = new ServiceFormViewModel()
            {
                Title = "Service 3",
                Description = "Third service",
                Price = 30m,
                ImageURL = "Service Image",
                ServiceType = Common.Enumerations.ServiceType.Slider,
                MaxAmount = 50
            };

            // Act
            await serviceService.CreateServiceAsync(model);
            var services = await serviceService.AllServiceListViewModelFilteredAsync(s => s.ServiceType == Common.Enumerations.ServiceType.Slider);

            // Assert
            int expectedNumber = 2;
            Assert.That(expectedNumber, Is.EqualTo(services.Count()));

            // Cleanup
            var createdService = data.Services.FirstOrDefault(s => s.Title == model.Title && s.Description == model.Description);
            if (createdService != null)
            {
                data.Services.Remove(createdService);
            }
        }

        [Test]
        public async Task DeactivateByIdAsync_ShouldDeactivateService()
        {
            // Arrange
            Assert.IsTrue(Service2.IsActive);

            // Act
            await serviceService.DeactivateByIdAsync(Service2.Id);

            // Assert
            Assert.IsFalse(Service2.IsActive);

            // Cleanup
            Service2.IsActive = true;
        }

        [Test]
        public async Task EditAsync_ShouldEditServiceCorrectly()
        {
            // Arrange
            Assert.That(Service.Title, Is.EqualTo("Game Completion"));

            ServiceEditViewModel model = new ServiceEditViewModel()
            {
                Id = 1,
                Title = "Game Completion - Edited",
                Description = "You will get a 100% game completion and all the items we acquire along the way!",
                Price = 300.00M,
                ImageURL = "/images/service/gameCompletion.jpg",
                MaxAmount = 0,
                ServiceType = ServiceType.Basic
            };

            // Act
            await serviceService.EditAsync(model);

            // Assert
            Assert.That(Service.Title, Is.Not.EqualTo("Game Completion"));
            Assert.That(Service.Title, Is.EqualTo("Game Completion - Edited"));
        }

        [Test]
        public async Task ExistsByIdAsync_ReturnsTrue_WhenIdIsCorrect()
        {
            // Arrange & Act
            bool result = await serviceService.ExistsByIdAsync(Service.Id);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public async Task ExistsByIdAsync_ReturnsFalse_WhenIdIsIncorrect()
        {
            // Arrange & Act
            bool result = await serviceService.ExistsByIdAsync(5000);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public async Task GetServiceDetailsViewModelByIdAsync_WorksCorrectly()
        {
            // Arrange
            int expectedId = Service.Id;
            string expectedTitle = Service.Title;
            string expectedDescription = Service.Description;
            string expectedImage = Service.ImageURL!;
            decimal expectedPrice = Service.Price;
            ServiceType expectedServiceType = Service.ServiceType;
            int expectedMaxAmount = Service.MaxAmount;

            // Act
            var service = await serviceService.GetServiceDetailsViewModelByIdAsync(Service.Id);

            // Assert
            Assert.That(service!.Id, Is.EqualTo(expectedId));
            Assert.That(service.Title, Is.EqualTo(expectedTitle));
            Assert.That(service.Description, Is.EqualTo(expectedDescription));
            Assert.That(service.ImageURL, Is.EqualTo(expectedImage));
            Assert.That(service.Price, Is.EqualTo(expectedPrice));
            Assert.That(service.ServiceType, Is.EqualTo(expectedServiceType));
            Assert.That(service.MaxAmount, Is.EqualTo(expectedMaxAmount));
        }

        [Test]
        public async Task GetPopularServicesAsync_ShouldReturnServices_InCorrectOrder()
        {
            // Arrange
            Service2.PurchaseCount += 10;
            data.Services.Update(Service2);
            await data.SaveChangesAsync();

            // Act
            var services = await serviceService.GetPopularServicesAsync();

            // Assert
            Assert.That(services.First().Title, Is.EqualTo(Service2.Title), "Service2 should be first in the list.");
        }

        [Test]
        public async Task GetServiceEditViewModelByIdAsync_ShouldReturnCorrectService()
        {
            // Arrange
            int expectedId = Service2.Id;
            string expectedTitle = Service2.Title;

            // Act
            var service =  await serviceService.GetServiceEditViewModelByIdAsync(Service2.Id);

            // Assert
            Assert.That(service.Id, Is.EqualTo(expectedId));
            Assert.That(service.Title, Is.EqualTo(expectedTitle));
        }

        [Test]
        public async Task GetServiceViewModelByIdAsync_ShouldReturnCorrectService()
        {
            // Arrange
            int expectedId = Service2.Id;
            string expectedTitle = Service2.Title;

            // Act
            var service = await serviceService.GetServiceViewModelByIdAsync(Service2.Id);

            // Assert
            Assert.That(service.Id, Is.EqualTo(expectedId));
            Assert.That(service.Title, Is.EqualTo(expectedTitle));
        }

        [Test]
        public async Task GetServiceOptionsAsync_ShouldReturnCorrectOptions()
        {
            //Arrange
            int expectedAmount = 2;
            string optionName = ServiceOption1.Name;

            //Act
            var options = await serviceService.GetServiceOptionsAsync(ServiceWithOptions.Id);

            //Assert
            Assert.That(options.Count, Is.EqualTo(expectedAmount));
            Assert.IsTrue(options.Any(o => o.Name == optionName));
            Assert.IsFalse(options.Any(o => o.Name == "WrongName"));
        }

        [Test]
        public async Task LastThreeServicesAsync_ShouldReturnCorrectOrderOfServices()
        {
            //Arrange
            int expectedId = 4;

            //Act
            var services = await serviceService.LastThreeServicesAsync();

            //Assert
            Assert.That(services.First().Id, Is.EqualTo(expectedId));
        }
    }
}

