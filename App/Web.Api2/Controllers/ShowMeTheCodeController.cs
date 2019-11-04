using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Infra.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Web.Api2.Controllers
{
    [Route("api/showmethecode")]
    public class ShowMeTheCodeController : Controller
    {
        private readonly IOptions<AppSettings> _appSettings;

        public ShowMeTheCodeController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        /// <summary>
        /// Obtem a Url do repositório do projeto
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        public IActionResult GetShowMeTheCode()
        {
            var url = _appSettings.Value.GitHubUrl;
            return Ok(url);
        }
    }
}
