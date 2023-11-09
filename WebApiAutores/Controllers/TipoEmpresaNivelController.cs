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
    [Route("AdmonPago/Api/TipoEmpresaNivel")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoEmpresaNivelController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoEmpresaNivelController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoEmpresaNivelDTO>>> Get()
        {
            var entidades = await context.TipoEmpresaNivel
                                 .OrderBy(e => e.Codigo)
                                 .ToListAsync();
            var dtos = mapper.Map<List<TipoEmpresaNivelDTO>>(entidades);
            return dtos;
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<TipoEmpresaNivelDTO>> Get(string codigo)
        {
            var entidades = await context.TipoEmpresaNivel.FirstOrDefaultAsync(x => x.Codigo.Contains(codigo));
            if (entidades == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoEmpresaNivelDTO>(entidades);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenertipoempresanivel")]
        public async Task<ActionResult<TipoEmpresaNivelDTO>> Get(int id)
        {
            var entidad = await context.TipoEmpresaNivel.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoEmpresaNivelDTO>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoEmpresaNivelDTOCR tipoEmpresaNivelDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(tipoEmpresaNivelDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "El tipo empresa nivel: >> " + tipoEmpresaNivelDTOCR.Codigo + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<TipoEmpresaNivel>(tipoEmpresaNivelDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<TipoEmpresaNivelDTO>(entidad);
                return new CreatedAtRouteResult("obtenertipoempresanivel", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoEmpresaNivelDTO tipoEmpresaNivelDTO)
        {
            var existe = await context.TipoEmpresaNivel.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<TipoEmpresaNivel>(tipoEmpresaNivelDTO);
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
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<TipoEmpresaNivelDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var tipoEmpresaNivelDB = await context.TipoEmpresaNivel.FirstOrDefaultAsync(x => x.Id == id);

            if (tipoEmpresaNivelDB == null)
            {
                return NotFound();
            }

            var tipoEmpresaNivelDTO = mapper.Map<TipoEmpresaNivelDTO>(tipoEmpresaNivelDB);
            patchDocument.ApplyTo(tipoEmpresaNivelDTO, ModelState);

            var esValido = TryValidateModel(tipoEmpresaNivelDTO);
            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(tipoEmpresaNivelDTO, tipoEmpresaNivelDB);
            tipoEmpresaNivelDB.FechaModificacion = DateTime.Now;

            await context.SaveChangesAsync();
            return NoContent();
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.TipoEmpresaNivel.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.TipoEmpresaNivel.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(TipoEmpresaNivelDTOCR tipoEmpresaNivelDTOCR)
        {
            var ValidarExistencia = await context.TipoEmpresaNivel.AnyAsync(x => (x.Codigo == tipoEmpresaNivelDTOCR.Codigo));
            return ValidarExistencia;
        }

    }
}
