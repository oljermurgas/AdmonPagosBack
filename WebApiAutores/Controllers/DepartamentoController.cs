using AdminPagosApi.DTO;
using AdminPagosApi.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApiAutores;
using WebApiAutores.Entidades;

namespace AdminPagosApi.Controllers
{
    [ApiController]
    [Route("AdmonPago/Api/Departamento")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepartamentoController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public DepartamentoController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartamentoDTO>>> Get()
        {
            var entidades = await context.Departamentos.OrderBy(e => e.Nombre)
                                 .ToListAsync();
            var dtos = mapper.Map<List<DepartamentoDTO>>(entidades);
            return dtos;
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<List<MunicipioDTO>>> Get(int codigo)
        {
           var entidades = await context.Municipio
                .Where(x => x.CodDep == codigo)
                .OrderBy(x => x.Nombre)
                .ToListAsync();

            if (entidades == null || entidades.Count == 0)
            {
                return NotFound();
            }

            var dtos = mapper.Map<List<MunicipioDTO>>(entidades);

            return dtos;
        }


    }
}
