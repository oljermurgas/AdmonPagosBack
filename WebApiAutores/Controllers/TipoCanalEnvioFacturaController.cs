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
    [Route("AdmonPago/Api/TipoCanalEnvioFactura")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoCanalEnvioFacturaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public TipoCanalEnvioFacturaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<TipoCanalEnvioFacturaDTO>>> Get()
        {
            var entidades = await context.TipoCanalEnvioFactura
                                 .OrderBy(e => e.Codigo)
                                 .ToListAsync();
            var dtos = mapper.Map<List<TipoCanalEnvioFacturaDTO>>(entidades);
            return dtos;
        }

        [HttpGet("{codigo}")]
        public async Task<ActionResult<TipoCanalEnvioFacturaDTO>> Get(string codigo)
        {
            var entidades = await context.TipoCanalEnvioFactura.FirstOrDefaultAsync(x => x.Codigo.Contains(codigo));
            if (entidades == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoCanalEnvioFacturaDTO>(entidades);
            return dtos;
        }

        [HttpGet("{id:int}", Name = "obtenertipocanalenvio")]
        public async Task<ActionResult<TipoCanalEnvioFacturaDTO>> Get(int id)
        {
            var entidad = await context.TipoCanalEnvioFactura.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<TipoCanalEnvioFacturaDTO>(entidad);
            return dtos;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TipoCanalEnvioFacturaDTOCR tipoCanalEnvioFacturaDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(tipoCanalEnvioFacturaDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "El tipo canal de envio: >> " + tipoCanalEnvioFacturaDTOCR.Codigo + " << ya existe" });
                }

                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<TipoCanalEnvioFactura>(tipoCanalEnvioFacturaDTOCR);
                //-----------------------------------------------------------------------------------------
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<TipoCanalEnvioFacturaDTO>(entidad);
                return new CreatedAtRouteResult("obtenertipocanalenvio", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] TipoCanalEnvioFacturaDTO tipoCanalEnvioFacturaDTO)
        {
            var existe = await context.TipoCanalEnvioFactura.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<TipoCanalEnvioFactura>(tipoCanalEnvioFacturaDTO);
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
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<TipoCanalEnvioFacturaDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var tipoEmpresaDB = await context.TipoCanalEnvioFactura.FirstOrDefaultAsync(x => x.Id == id);

            if (tipoEmpresaDB == null)
            {
                return NotFound();
            }

            var tipoEmpresaDTO = mapper.Map<TipoCanalEnvioFacturaDTO>(tipoEmpresaDB);
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
            var existe = await context.TipoCanalEnvioFactura.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.TipoCanalEnvioFactura.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(TipoCanalEnvioFacturaDTOCR tipoCanalEnvioFacturaDTOCR)
        {
            var ValidarExistencia = await context.TipoCanalEnvioFactura.AnyAsync(x => (x.Codigo == tipoCanalEnvioFacturaDTOCR.Codigo));
            return ValidarExistencia;
        }

    }
}
