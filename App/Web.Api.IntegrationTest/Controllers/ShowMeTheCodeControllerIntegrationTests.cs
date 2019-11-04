using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Xunit.Abstractions;
using Newtonsoft.Json;
using Web.Api2.Controllers;
using Microsoft.Extensions.Options;
using App.Infra.Settings;

namespace Web.Api.IntegrationTest.Controllers
{
    public class ShowMeTheCodeControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory<Api2.Startup>>
    {
        private readonly CustomWebApplicationFactory<Api2.Startup> _factory;
        private readonly string _baseUrl = "http://localhost:5002";

        public ShowMeTheCodeControllerIntegrationTests(CustomWebApplicationFactory<Api2.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public void Can_Initialize_Controller()
        {
            //Arrange
            IOptions<AppSettings> appSettings = Options.Create<AppSettings>(new AppSettings { GitHubUrl = "https://github.com/angelicaflausino/desafio-softplan" });

            //Act
            var controller = new ShowMeTheCodeController(appSettings);

            //Assert
            Assert.NotNull(controller);
        }

        [Fact]
        public async Task Get_Endpoints_Return_Success_And_Correct_ContentType()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync($"{_baseUrl}/api/showmethecode");

            //Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Get_Result_Should_BeString_Type()
        {
            //Arrange
            var client = _factory.CreateClient();

            //Act
            var response = await client.GetAsync($"{_baseUrl}/api/showmethecode");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<string>(content);

            //Assert
            Assert.True(result.GetType() == typeof(string));
        }

        [Fact]
        public async Task Get_Result_Should_Be_Valid_Url()
        {
            //Arrange
            var client = _factory.CreateClient();
            var type = typeof(string);

            //Act
            var response = await client.GetAsync($"{_baseUrl}/api/showmethecode");
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<string>(content);
            var isValidUrl = Uri.IsWellFormedUriString(result, UriKind.Absolute);

            //Assert
            Assert.True(isValidUrl);
        }
    }
}
