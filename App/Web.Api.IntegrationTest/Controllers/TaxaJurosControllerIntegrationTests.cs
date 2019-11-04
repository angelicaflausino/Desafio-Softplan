using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json;
using Web.Api1.Controllers;
using Moq;
using App.Application.Interfaces;

namespace Web.Api.IntegrationTest.Controllers
{
    public class TaxaJurosControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Api1.Startup>>
    {
        private readonly CustomWebApplicationFactory<Api1.Startup> _factory;
        private readonly string _baseUrl = "http://localhost:5001";

        public TaxaJurosControllerIntegrationTests(CustomWebApplicationFactory<Api1.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void Can_Initialize_Controller()
        {
            //Arrange
            var mock = new Mock<ITaxaJurosService>();

            //Act
            var controller = new TaxaJurosController(mock.Object);

            //Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public async Task Get_Endpoints_Return_Success_And_Correct_ContentType()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync($"{_baseUrl}/api/taxaJuros");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Get_Result_Should_Be_Decimal_Type()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync($"{_baseUrl}/api/taxaJuros");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<decimal>(content);

            //Assert
            Assert.True(result.GetType() == typeof(decimal));
        }
    }
}
