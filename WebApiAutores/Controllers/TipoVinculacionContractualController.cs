using AdminPagosApi.DTO;
using AdminPagosApi.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApiAutores;
using WebApiAutores.Entidades;

namespace AdminPagosApi.Controllers
{
    [ApiController]
    [Route("AdmonPago/Api/TipoVinculacionContractual")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoVinculacionContractualController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoVinculacionContractualController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoVinculacionContractualDTO>>> Get()
        {
            var entidades = await context.TipoVinculacionContractual.OrderBy(e => e.Codigo)
                                 .ToListAsync();
            var dtos = mapper.Map<List<TipoVinculacionContractualDTO>>(entidades);
            return dtos;
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<TipoVinculacionContractualDTO>> Get(string codigo)
        {
            var entidades = await context.TipoVinculacionContractual.FirstOrDefaultAsync(x => x.Codigo.Contains(codigo));
            if (entidades == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoVinculacionContractualDTO>(entidades);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenertipovinculacioncontractual")]
        public async Task<ActionResult<TipoVinculacionContractualDTO>> Get(int id)
        {
            var entidad = await context.TipoVinculacionContractual.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoVinculacionContractualDTO>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoVinculacionContractualDTOCR tipoVinculacionContractualDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(tipoVinculacionContractualDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "El tipo vinculacion contractual: >> " + tipoVinculacionContractualDTOCR.Codigo + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<TipoVinculacionContractual>(tipoVinculacionContractualDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<TipoVinculacionContractualDTO>(entidad);
                return new CreatedAtRouteResult("obtenertipovinculacioncontractual", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoVinculacionContractualDTO tipoVinculacionContractualDTO)
        {
            var existe = await context.TipoVinculacionContractual.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<TipoVinculacionContractual>(tipoVinculacionContractualDTO);
            //-----------------------------------------------------------------------------------------
            //entidad.UserName = userName;
            entidad.FechaModificacion = DateTime.Now;
            //-----------------------------------------------------------------------------------------
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.TipoVinculacionContractual.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.TipoVinculacionContractual.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(TipoVinculacionContractualDTOCR tipoVinculacionContractualDTOCR)
        {
            var ValidarExistencia = await context.TipoVinculacionContractual.AnyAsync(x => (x.Codigo == tipoVinculacionContractualDTOCR.Codigo));
            return ValidarExistencia;
        }

    }
}
