using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using PropertyAPI.Services;
using PropertyAPI.DTOs;
using PropertyAPI.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyAPI.Controllers
{
    public class PropertyControllerTests
    {
        private PropertyController _propertyController;
        private Mock<IPropertyService> _propertyService;

        [SetUp]
        public void SetUp()
        {
            _propertyService = new Mock<IPropertyService>();
            _propertyController = new PropertyController(_propertyService.Object);
            _propertyController.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        }

        [Test]
        public async Task GetProperties_Returns_Ok_When_Valid_Data()
        {
            var properties = new List<PropertyContent>();

            _propertyService.Setup(x => x.GetProperties()).ReturnsAsync(properties);

            var result = (ObjectResult)await _propertyController.GetProperties();

            Assert.That(result.Value, Is.EqualTo(properties));
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public async Task GetProperties_Returns_NotFound_When_Record_NotExists()
        {
            IEnumerable<PropertyContent> properties = null;

            _propertyService.Setup(x => x.GetProperties()).ReturnsAsync(properties);
            var result = (ObjectResult)await _propertyController.GetProperties();

            Assert.That(((Response)result.Value).IsSuccess, Is.False);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
        }

        [Test]
        public async Task SaveRecord_Returns_Ok_When_Valid_Data()
        {
            var propertyData = new PropertyContent()
            {
                Id = 1625006,
                Address = new Address{address1 = "000-X Moneta Way" , address2 = "", city = "San Francisco" 
                    , country = "USA", county = "test", state = "CA", zip = "94112", zipPlus4 = null, district = null },
                PhysicalData = new Physical(){YearBuilt = 1952},
                FinancialData = new Financial(){ListPrice = 189000.00M , MonthlyRent = 1599.00M }
            };

            _propertyService.Setup(x => x.SaveRecord(propertyData)).ReturnsAsync(new Response { IsSuccess = true });

            var result = (ObjectResult)await _propertyController.SaveRecord(propertyData);

            Assert.That(((Response)result.Value).IsSuccess, Is.True);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
        }

        [Test]
        public async Task SaveRecord_Returns_BadRequest_When_Action_Failed()
        {
            var propertyData = new PropertyContent();
            

            _propertyService.Setup(x => x.SaveRecord(propertyData)).ReturnsAsync(new Response { IsSuccess = false });

            var result = (ObjectResult)await _propertyController.SaveRecord(propertyData);

            Assert.That(((Response)result.Value).IsSuccess, Is.False);
            Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status400BadRequest));
        }
    }
}
