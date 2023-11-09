using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPagosApi.Controllers
{
    [ApiController]
    [Route("api/inicio")]
    public class InicioController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "OMC, Aplicación corriendo";
        }
    }
}
