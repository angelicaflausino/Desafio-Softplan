using System;
using System.Collections.Generic;
using System.Text;

namespace App.Infra.Settings
{
    public class AppSettings
    {
        public string DevelopUrl { get; set; }
        public string ProductionUrl { get; set; }
        public string ApiTaxaJurosUrl { get; set; }
        public decimal TaxaJurosBase { get; set; }
        public string GitHubUrl { get; set; }
    }
}
