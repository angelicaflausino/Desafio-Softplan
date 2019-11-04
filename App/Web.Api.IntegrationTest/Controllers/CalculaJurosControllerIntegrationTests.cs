using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json;
using App.Application.Interfaces;
using Moq;
using Web.Api2.Controllers;

namespace Web.Api.IntegrationTest.Controllers
{
    public class CalculaJurosControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Api2.Startup>>
    {
        private readonly CustomWebApplicationFactory<Api2.Startup> _factory;
        private readonly string _baseUrl = "http://localhost:5002";

        public CalculaJurosControllerIntegrationTests(CustomWebApplicationFactory<Api2.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void Can_Initialize_Controller()
        {
            //Arrange
            var mock = new Mock<ICalculaJurosService>();

            //Act
            var controller = new CalculaJurosController(mock.Object);

            //Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public async Task Get_Endpoints_Return_Success_And_Correct_Content_Type()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync($"{_baseUrl}/api/calculajuros");

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
            var response = await client.GetAsync($"{_baseUrl}/api/calculajuros");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<decimal>(content);

            //Assert
            Assert.True(result.GetType() == typeof(decimal));
        }
    }
}
