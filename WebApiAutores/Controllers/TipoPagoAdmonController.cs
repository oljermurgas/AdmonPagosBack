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
    [Route("AdmonPago/Api/TipoPagoAdmon")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoPagoAdmonController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoPagoAdmonController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoPagoAdmonDTO>>> Get()
        {
            var entidades = await context.TipoPagoAdmon.OrderBy(e => e.Codigo)
                                 .ToListAsync();
            var dtos = mapper.Map<List<TipoPagoAdmonDTO>>(entidades);
            return dtos;
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<TipoPagoAdmonDTO>> Get(string codigo)
        {
            var entidades = await context.TipoPagoAdmon.FirstOrDefaultAsync(x => x.Codigo.Contains(codigo));
            if (entidades == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoPagoAdmonDTO>(entidades);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenertipopagoadmon")]
        public async Task<ActionResult<TipoPagoAdmonDTO>> Get(int id)
        {
            var entidad = await context.TipoPagoAdmon.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoPagoAdmonDTO>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoPagoAdmonDTOCR tipoPagoAdmonDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(tipoPagoAdmonDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "El tipo pago administrar: >> " + tipoPagoAdmonDTOCR.Codigo + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<TipoPagoAdmon>(tipoPagoAdmonDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<TipoPagoAdmonDTO>(entidad);
                return new CreatedAtRouteResult("obtenertipopagoadmon", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoPagoAdmonDTO tipoPagoAdmonDTO)
        {
            var existe = await context.TipoPagoAdmon.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<TipoTarifa>(tipoPagoAdmonDTO);
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
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<TipoPagoAdmonDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var tipoEmpresaDB = await context.TipoPagoAdmon.FirstOrDefaultAsync(x => x.Id == id);

            if (tipoEmpresaDB == null)
            {
                return NotFound();
            }

            var tipoEmpresaDTO = mapper.Map<TipoPagoAdmonDTO>(tipoEmpresaDB);
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
            var existe = await context.TipoPagoAdmon.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.TipoPagoAdmon.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(TipoPagoAdmonDTOCR tipoPagoAdmonDTOCR)
        {
            var ValidarExistencia = await context.TipoPagoAdmon.AnyAsync(x => (x.Codigo == tipoPagoAdmonDTOCR.Codigo));
            return ValidarExistencia;
        }

    }
}
