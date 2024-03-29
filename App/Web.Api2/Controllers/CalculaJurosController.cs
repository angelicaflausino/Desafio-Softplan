﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using App.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Api2.Controllers
{
    [Route("api/calculajuros")]
    public class CalculaJurosController : Controller
    {
        private readonly ICalculaJurosService _calculaJurosService;

        public CalculaJurosController(ICalculaJurosService calculaJurosService)
        {
            _calculaJurosService = calculaJurosService;
        }

        /// <summary>
        /// Obtem o cálculo de juros composto
        /// </summary>
        /// <param name="valorInicial"></param>
        /// <param name="meses"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> GetCalcularJuros([FromQuery]decimal valorInicial, [FromQuery]int meses)
        {
            var resultado = await _calculaJurosService.CalcularJuros(valorInicial, meses);
            return Ok(resultado);
        }
    }
}
