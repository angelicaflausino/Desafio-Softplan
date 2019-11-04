using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface ICalculaJurosService
    {
        Task<decimal> ObterJuros();
        Task<decimal> CalcularJuros(decimal valorInicial, int meses);
    }
}
