using AdminPagosApi.DTO;
using AdminPagosApi.Entidades;
using AutoMapper;
using Azure;
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
    [Route("AdmonPago/Api/TipoEmpresa")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoEmpresaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoEmpresaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoEmpresaDTO>>> Get()
        {
            var entidades = await context.TipoEmpresa
                                 .OrderBy(e => e.Codigo)
                                 .ToListAsync();
            var dtos = mapper.Map<List<TipoEmpresaDTO>>(entidades);
            return dtos;
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<TipoEmpresaDTO>> Get(string codigo)
        {
            var entidades = await context.TipoEmpresa.FirstOrDefaultAsync(x => x.Codigo.Contains(codigo));
            if (entidades == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoEmpresaDTO>(entidades);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenertipoempresa")]
        public async Task<ActionResult<TipoEmpresaDTO>> Get(int id)
        {
            var entidad = await context.TipoEmpresa.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoEmpresaDTO>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoEmpresaDTOCR tipoEmpresaDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(tipoEmpresaDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "El tipo empresa : >> " + tipoEmpresaDTOCR.Codigo + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<TipoEmpresa>(tipoEmpresaDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<TipoEmpresaDTO>(entidad);
                return new CreatedAtRouteResult("obtenertipoempresa", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoEmpresaDTO tipoEmpresaDTO)
        {
            var existe = await context.TipoEmpresa.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<TipoEmpresa>(tipoEmpresaDTO);
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
        public async Task<ActionResult>Patch(int id, JsonPatchDocument<TipoEmpresaDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var tipoEmpresaDB = await context.TipoEmpresa.FirstOrDefaultAsync(x => x.Id == id);
            
            if (tipoEmpresaDB == null)
            {
                return NotFound();
            }

            var tipoEmpresaDTO = mapper.Map<TipoEmpresaDTO> (tipoEmpresaDB);
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
            var existe = await context.TipoEmpresa.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.TipoEmpresa.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(TipoEmpresaDTOCR tipoEmpresaDTOCR)
        {
            var ValidarExistencia = await context.TipoEmpresa.AnyAsync(x => (x.Codigo == tipoEmpresaDTOCR.Codigo));
            return ValidarExistencia;
        }

    }
}
