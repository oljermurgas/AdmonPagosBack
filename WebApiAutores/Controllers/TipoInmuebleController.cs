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
    [Route("AdmonPago/Api/TipoInmueble")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoInmuebleController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoInmuebleController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoInmuebleDTO>>> Get()
        {
            var entidades = await context.TipoInmueble
                                 .OrderBy(e => e.Codigo)
                                 .ToListAsync();
            var dtos = mapper.Map<List<TipoInmuebleDTO>>(entidades);
            return dtos;
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<TipoInmuebleDTO>> Get(string codigo)
        {
            var entidades = await context.TipoInmueble.FirstOrDefaultAsync(x => x.Codigo.Contains(codigo));
            if (entidades == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoInmuebleDTO>(entidades);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenertipoinmueble")]
        public async Task<ActionResult<TipoInmuebleDTO>> Get(int id)
        {
            var entidad = await context.TipoInmueble.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoInmuebleDTO>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoInmuebleDTOCR tipoInmuebleDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(tipoInmuebleDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "El tipo inmueble: >> " + tipoInmuebleDTOCR.Codigo + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<TipoInmueble>(tipoInmuebleDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<TipoInmuebleDTO>(entidad);
                return new CreatedAtRouteResult("obtenertipoinmueble", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoInmuebleDTO tipoInmuebleDTO)
        {
            var existe = await context.TipoInmueble.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<TipoTarifa>(tipoInmuebleDTO);
            //-----------------------------------------------------------------------------------------
            //entidad.UserName = userName;
            entidad.FechaModificacion = DateTime.Now;
            //-----------------------------------------------------------------------------------------
            entidad.Id = id;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<TipoInmuebleDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var tipoEmpresaDB = await context.TipoInmueble.FirstOrDefaultAsync(x => x.Id == id);

            if (tipoEmpresaDB == null)
            {
                return NotFound();
            }

            var tipoEmpresaDTO = mapper.Map<TipoInmuebleDTO>(tipoEmpresaDB);
            patchDocument.ApplyTo(tipoEmpresaDTO, ModelState);

            var esValido = TryValidateModel(tipoEmpresaDTO);
            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(tipoEmpresaDTO, tipoEmpresaDB);
            tipoEmpresaDB.FechaModificacion = DateTime.Now;

            await context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.TipoInmueble.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.TipoInmueble.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(TipoInmuebleDTOCR tipoInmuebleDTOCR)
        {
            var ValidarExistencia = await context.TipoInmueble.AnyAsync(x => (x.Codigo == tipoInmuebleDTOCR.Codigo));
            return ValidarExistencia;
        }

    }
}
