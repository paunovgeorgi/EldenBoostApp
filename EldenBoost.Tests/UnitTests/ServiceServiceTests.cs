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
        public void ActivateByIdAsync_SetsIsActive_ToTrue()
        {
            // Arrange
            Service.IsActive = false;

            // Act
            serviceService.ActivateByIdAsync(Service.Id);

            // Assert
            Assert.IsTrue(Service.IsActive);
        }

        [Test]
        public void AllAsync_ReturnsCorrect_NumberOfServices()
        {
            // Arrange
            int expected = 2;
            var model = new AllServicesQueryModel
            {
                SearchString = string.Empty,
                CurrentPage = 2,
                ServicesPerPage = 2,
                TotalServices = 2,
                ServiceSorting = Core.Models.Service.Enums.ServiceSorting.Newest,
            };

            // Act
            var services = serviceService.AllAsync(model).Result;

            // Assert
            Assert.That(expected, Is.EqualTo(services.TotalServicesCount));
        }

        [Test]
        public void AllAsync_SearchString_ReturnsCorrectServices()
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
            var services = serviceService.AllAsync(model).Result;

            // Assert
            Assert.That(expected, Is.EqualTo(services.TotalServicesCount));
        }

        [Test]
        public void AllServiceListViewModelFilteredAsync_ShouldReturnCorrectServices()
        {
            // Arrange
            int expectedNumber = 1;

            // Act
            var services = serviceService.AllServiceListViewModelFilteredAsync(s => s.ServiceType == Common.Enumerations.ServiceType.Slider).Result;

            // Assert
            Assert.That(expectedNumber, Is.EqualTo(services.Count()));
        }

        [Test]
        public void CreateServiceAsync_ShouldWorkProperly()
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
            serviceService.CreateServiceAsync(model);
            var services = serviceService.AllServiceListViewModelFilteredAsync(s => s.ServiceType == Common.Enumerations.ServiceType.Slider).Result;

            // Assert
            int expectedNumber = 2;
            Assert.That(expectedNumber, Is.EqualTo(services.Count()));
        }

        [Test]
        public void DeactivateByIdAsync_ShouldDeactivateService()
        {
            // Arrange
            Assert.IsTrue(Service2.IsActive);

            // Act
            serviceService.DeactivateByIdAsync(Service2.Id);

            // Assert
            Assert.IsFalse(Service2.IsActive);

            // Cleanup
            Service2.IsActive = true;
        }

        [Test]
        public void EditAsync_ShouldEditServiceCorrectly()
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
            serviceService.EditAsync(model);

            // Assert
            Assert.That(Service.Title, Is.Not.EqualTo("Game Completion"));
            Assert.That(Service.Title, Is.EqualTo("Game Completion - Edited"));
        }

        [Test]
        public void ExistsByIdAsync_ReturnsTrue_WhenIdIsCorrect()
        {
            // Arrange & Act
            bool result = serviceService.ExistsByIdAsync(Service.Id).Result;

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void ExistsByIdAsync_ReturnsFalse_WhenIdIsIncorrect()
        {
            // Arrange & Act
            bool result = serviceService.ExistsByIdAsync(5000).Result;

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetServiceDetailsViewModelByIdAsync_WorksCorrectly()
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
            var service = serviceService.GetServiceDetailsViewModelByIdAsync(Service.Id).Result;

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
        public void GetServiceEditViewModelByIdAsync_ShouldReturnCorrectService()
        {
            // Arrange
            int expectedId = Service2.Id;
            string expectedTitle = Service2.Title;

            // Act
            var service = serviceService.GetServiceEditViewModelByIdAsync(Service2.Id).Result;

            // Assert
            Assert.That(service.Id, Is.EqualTo(expectedId));
            Assert.That(service.Title, Is.EqualTo(expectedTitle));
        }
    }
}

