using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api1.Controllers
{
    [Route("api/taxaJuros")]
    public class TaxaJurosController : Controller
    {
        private readonly ITaxaJurosService _taxaJurosService;

        public TaxaJurosController(ITaxaJurosService taxaJurosService)
        {
            _taxaJurosService = taxaJurosService;
        }

        /// <summary>
        /// Obtem a taxa de juros 
        /// </summary>
        /// <returns>Retorna o valor da taxas de juros</returns>
        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetTaxaJuros()
        {
            var taxaJuros = _taxaJurosService.ObterTaxaJuros();
            return Ok(taxaJuros);
        }
    }
}
