using EldenBoost.Core.Contracts;
using EldenBoost.Core.Models.Service;
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
            Assert.That(expected, Is.EqualTo(services.TotalServicesCount))
        }
    }
}
