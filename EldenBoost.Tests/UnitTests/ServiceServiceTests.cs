using EldenBoost.Common.Enumerations;
using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Service;
using EldenBoost.Core.Services;
using EldenBoost.Infrastructure.Data.Models;
using EldenBoost.Infrastructure.Data.Repository;
using System;
using System.Collections.Generic;
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
            Service.IsActive = false;
            serviceService.ActivateByIdAsync(Service.Id);
            Assert.IsTrue(Service.IsActive);
        }

        [Test]
        public void AllAsync_ReturnsCorrect_NumberOfServices()
        {
            int expected = 2;
            var model = new AllServicesQueryModel
            {
                SearchString = string.Empty,
                CurrentPage = 2,
                ServicesPerPage = 2,
                TotalServices = 2,
                ServiceSorting = Core.Models.Service.Enums.ServiceSorting.Newest,
            };
            var services = serviceService.AllAsync(model).Result;
            Assert.That(expected, Is.EqualTo(services.TotalServicesCount));
        }

        [Test]
        public void AllAsync_SearchString_ReturnsCorrectServices()
        {
            int expected = 1;
            var model = new AllServicesQueryModel
            {
                SearchString = "Desired amount",
                CurrentPage = 1,
                ServicesPerPage = 1,
                TotalServices = 1,
                ServiceSorting = Core.Models.Service.Enums.ServiceSorting.Newest,
            };
            var services = serviceService.AllAsync(model).Result;
            Assert.That(expected, Is.EqualTo(services.TotalServicesCount));
        }

        [Test]
        public void AllServiceListViewModelFilteredAsync_ShouldReturnCorrectServices()
        {
            int expectedNumber = 1;
            var services = serviceService.AllServiceListViewModelFilteredAsync(s => s.ServiceType == Common.Enumerations.ServiceType.Slider).Result;
            Assert.That(expectedNumber, Is.EqualTo(services.Count()));
        }

        [Test]
        public void CreateServiceAsync_ShouldWorkProperly()
        {
            ServiceFormViewModel model = new ServiceFormViewModel()
            {
                Title = "Service 3",
                Description = "Third service",
                Price = 30m,
                ImageURL = "Service Image",
                ServiceType = Common.Enumerations.ServiceType.Slider,
                MaxAmount = 50
            };

            serviceService.CreateServiceAsync(model);

            int expectedNumber = 2;
            var services = serviceService.AllServiceListViewModelFilteredAsync(s => s.ServiceType == Common.Enumerations.ServiceType.Slider).Result;
            Assert.That(expectedNumber, Is.EqualTo(services.Count()));
        }

        [Test]
        public void DeactivateByIdAsync_ShouldDeactivateService()
        {
            Assert.IsTrue(Service2.IsActive);

            serviceService.DeactivateByIdAsync(Service2.Id);

            Assert.IsFalse(Service2.IsActive);
        }

        [Test]
        public void EditAsync_ShouldEditServiceCorrectly()
        {
            Assert.That(Service.Title, Is.EqualTo("Game Completion"));

            ServiceEditViewModel model = new ServiceEditViewModel()
            {
                Id = 1,
                Title = "Game Completion - Edited",
                Description = "You will get a 100% game completion and all the items we aquire along the way!",
                Price = 300.00M,
                ImageURL = "/images/service/gameCompletion.jpg",
                MaxAmount = 0,
                ServiceType = ServiceType.Basic
            };

            serviceService.EditAsync(model);

            Assert.That(Service.Title, Is.Not.EqualTo("Game Completion"));
            Assert.That(Service.Title, Is.EqualTo("Game Completion - Edited"));

        }
    }
}
