using App.Application.Services;
using App.Infra.Settings;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Threading.Tasks;

namespace App.Test
{
    [TestClass]
    public class TaxaJurosServiceTest
    {
        [TestMethod]
        public void Result_Should_Be_Equal()
        {
            //Arrange
            var service = new TaxaJurosService();
            var esperado = 0.01m;

            //Act
            var resultado = service.ObterTaxaJuros();

            //Assert
            Assert.AreEqual(esperado, resultado);
        }
    }
}
