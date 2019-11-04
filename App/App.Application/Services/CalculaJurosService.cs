using App.Application.Interfaces;
using App.Infra.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class CalculaJurosService : ICalculaJurosService
    {
        private HttpClient _client;
        private readonly IOptions<AppSettings> _appSettings;

        public CalculaJurosService(HttpClient client, IOptions<AppSettings> appSettings)
        {
            _client = client;
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            _appSettings = appSettings;
        }

        public async Task<decimal> CalcularJuros(decimal valorInicial, int meses)
        {
            if (valorInicial == 0)
                return 0;
            if (meses == 0)
                return 0;

            var juros = await ObterJuros();
            var valorFinal = valorInicial;

            for (int tempo = 0; tempo < meses; tempo++)
            {
                valorFinal *= (1 + juros);
            }

            var resultado = string.Format("{0:N2}", valorFinal);
            return Convert.ToDecimal(resultado);
        }

        public async Task<decimal> ObterJuros()
        {
            var url = _appSettings.Value.ApiTaxaJurosUrl;
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            decimal resultado = JsonConvert.DeserializeObject<decimal>(content);
            return resultado;
        }
    }
}
