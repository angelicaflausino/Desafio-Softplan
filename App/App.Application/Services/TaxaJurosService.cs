using App.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Services
{
    public class TaxaJurosService : ITaxaJurosService
    {
        public decimal ObterTaxaJuros()
        {
            var taxaJuros = 0.01m;
            return taxaJuros;
        }
    }
}
