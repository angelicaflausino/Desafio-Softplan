using App.Application.Services;
using App.Infra.Settings;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Test
{
    [TestClass]
    public class CalculaJurosServiceTest
    {
        private HttpClient _httpClient;
        private IOptions<AppSettings> _settings;

        public CalculaJurosServiceTest()
        {
            _httpClient = new HttpClient() { BaseAddress = new System.Uri("http://localhost:5002") };
            _settings = Options.Create<AppSettings>(new AppSettings { ApiTaxaJurosUrl = "http://localhost:5001/api/taxaJuros" });
        }

        [TestMethod]
        public async Task Result_Should_Be_Equal()
        {
            //Arrange
            var valorInicial = 100m;
            var meses = 5;
            var esperado = 105.10m;
            var service = new CalculaJurosService(_httpClient, _settings);

            //Act
            var valorFinal = await service.CalcularJuros(valorInicial, meses);

            //Assert
            Assert.AreEqual(esperado, valorFinal);
        }

        [TestMethod]
        public async Task Result_Should_Be_Zero()
        {
            //Arrange
            var valorInicial = 0m;
            var meses = 5;
            var esperado = 0;
            var service = new CalculaJurosService(_httpClient, _settings);

            //Act
            var valorFinal = await service.CalcularJuros(valorInicial, meses);

            //Assert
            Assert.AreEqual(esperado, valorFinal);
        }

        [TestMethod]
        public async Task Result_Should_Be_GreaterThan()
        {
            //Arrange
            var valorInicial = 200m;
            var meses = 7;
            var service = new CalculaJurosService(_httpClient, _settings);

            //Act
            var valorFinal = await service.CalcularJuros(valorInicial, meses);

            //Assert
            Assert.IsTrue(valorFinal > 0);
        }
    }
}
