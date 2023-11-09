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
    [Route("AdmonPago/Api/TipoEmpresaSector")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoEmpresaSectorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoEmpresaSectorController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoEmpresaSectorDTO>>> Get()
        {
            var entidades = await context.TipoEmpresaSector
                                 .OrderBy(e => e.Codigo)
                                 .ToListAsync();
            var dtos = mapper.Map<List<TipoEmpresaSectorDTO>>(entidades);
            return dtos;
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<TipoEmpresaSectorDTO>> Get(string codigo)
        {
            var entidades = await context.TipoEmpresaSector.FirstOrDefaultAsync(x => x.Codigo.Contains(codigo));
            if (entidades == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoEmpresaSectorDTO>(entidades);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenertipoempresasector")]
        public async Task<ActionResult<TipoEmpresaSectorDTO>> Get(int id)
        {
            var entidad = await context.TipoEmpresaSector.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoEmpresaSectorDTO>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoEmpresaSectorDTOCR tipoEmpresaSectorDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(tipoEmpresaSectorDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "El tipo empresa: >> " + tipoEmpresaSectorDTOCR.Codigo + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<TipoEmpresaSector>(tipoEmpresaSectorDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<TipoEmpresaSectorDTO>(entidad);
                return new CreatedAtRouteResult("obtenertipoempresasector", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoEmpresaSectorDTO tipoEmpresaSectorDTO)
        {
            var existe = await context.TipoEmpresaSector.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<TipoTarifa>(tipoEmpresaSectorDTO);
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
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<TipoEmpresaSectorDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var tipoEmpresaDB = await context.TipoEmpresaSector.FirstOrDefaultAsync(x => x.Id == id);

            if (tipoEmpresaDB == null)
            {
                return NotFound();
            }

            var tipoEmpresaDTO = mapper.Map<TipoEmpresaSectorDTO>(tipoEmpresaDB);
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
            var existe = await context.TipoEmpresaSector.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.TipoEmpresaSector.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(TipoEmpresaSectorDTOCR tipoEmpresaSectorDTOCR)
        {
            var ValidarExistencia = await context.TipoEmpresaSector.AnyAsync(x => (x.Codigo == tipoEmpresaSectorDTOCR.Codigo));
            return ValidarExistencia;
        }

    }
}
