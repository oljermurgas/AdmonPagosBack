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
    [Route("AdmonPago/Api/Factura")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FacturaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public FacturaController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<FacturaRegistroDTO>>> Get()
        {
            var facturas = await context.FacturaRegistro
              .Include(x => x.FacturasTipoObligacion)
                  .ThenInclude(fto => fto.TipoObligacion)
              .Include(x => x.FacturaEstado)
              .Include(x => x.Entidad)
              .Include(x => x.Sede)
              .Where(x => x.FacturasTipoObligacion.Any())
              .OrderBy(e => e.FechaOportunoPago)
                  .ThenBy(s => s.Sede.Nombre)
              .ToListAsync();

            var dtos = mapper.Map<List<FacturaRegistroDTO>>(facturas);
            return dtos;
        }

        [HttpGet("FacturaDetalleConcepto/{facturaId}")]
        public IActionResult FacturaDetalleConcepto(int facturaId)
        {
            // Realizar la consulta para obtener los datos
            var query = from ftoc in context.FacturaTipoObligacionConceptos
                        join fto in context.FacturaTipoObligacion on ftoc.FacturaTipoObligacionId equals fto.Id
                        join tcf in context.TipoConceptoFacturacion on ftoc.TipoConceptoFacturacionId equals tcf.Id
                        join to in context.TipoObligacion on fto.TipoObligacionId equals to.Id
                        where fto.FacturaRegistroId == facturaId
                        orderby to.Descripcion, tcf.Descripcion, ftoc.Valor
                        select new
                        {
                            TipoObligacionDescripcion = to.Descripcion,
                            TipoConceptoFacturacionDescripcion = tcf.Descripcion,
                            ftoc.Valor,
                            ftoc.Id,
                            TipoObligacionId = to.Id,
                            TipoConceptoFacturacionId = tcf.Id
                        };

            var result = query.ToList();

            if (result.Any())
            {
                return Ok(result);
            }
            else
            {
                // Si no hay datos, devolver 0
                return Ok(0);
            }
        }



        [HttpGet("obtenerNumeroDelContrato")]
        public async Task<ActionResult<string>> ObtenerNumeroDelContador([FromQuery] int entidadId, [FromQuery] int sedeId)
        {
            try
            {
                var entidad = await context.SedeEntidades.FirstOrDefaultAsync(x => x.SedeId == sedeId && x.EntidadId == entidadId);

                if (entidad == null) {
                    return Ok("X");
                }

                return Ok(entidad.NumeroContrato.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("obtenerUltimaFactura")]
        public async Task<ActionResult<UltimaFacturaDTO>> ObtenerUltimaFactura([FromQuery] int entidadId, [FromQuery] int sedeId)
        {
            try
            {
                var entidad = await context.FacturaRegistro
                    .Where(x => x.SedeId == sedeId && x.EntidadId == entidadId)
                    .OrderByDescending(x => x.FechaEmision)
                    .FirstOrDefaultAsync();

                if (entidad == null)
                {
                    // Si no hay factura, devolver un resultado específico en lugar de un error
                    var ultimaFacturaVacia = new UltimaFacturaDTO
                    {
                        Fecha = DateTime.Now,
                        Valor = 0
                    };

                    return Ok(ultimaFacturaVacia);
                }

                var ultimaFactura = new UltimaFacturaDTO
                {
                    Fecha = (DateTime)entidad.FechaEmision,
                    Valor = entidad.ValorFactura
                };

                return Ok(ultimaFactura);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpGet("{id:int}", Name = "obtenerfactura")]
        public async Task<ActionResult<FacturaRegistroDTO>> Get(int id)
        {
            var entidad = await context.FacturaRegistro.FirstOrDefaultAsync(x => x.Id == id);
            if (entidad == null)
            {
                return NotFound();
            }
            var dtos = mapper.Map<FacturaRegistroDTO>(entidad);
            return dtos;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FacturaRegistroDTOCR facturaRegistroDTOCR)
        {
            try
            {
                var ValideExistencia = await ValidarExistencia(facturaRegistroDTOCR);
                if (ValideExistencia)
                {
                    return BadRequest(new { message = "La factura : >> " + facturaRegistroDTOCR.FacturaNumero + " << ya existe" });
                }

                var estadoInicialFactura = await context.FacturaEstado.FirstOrDefaultAsync(x => x.Codigo == "001");
                
                //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                var entidad = mapper.Map<FacturaRegistro>(facturaRegistroDTOCR);
                //-----------------------------------------------------------------------------------------
                entidad.FacturaEstadoId = estadoInicialFactura.Id;
                //entidad.UserName = userName;
                entidad.FechaCreacion = DateTime.Now;
                entidad.FechaModificacion = DateTime.Now;
                entidad.Estado = true;
                //-----------------------------------------------------------------------------------------
                context.Add(entidad);
                await context.SaveChangesAsync();
                var entidadDTO = mapper.Map<FacturaRegistroDTO>(entidad);
                return new CreatedAtRouteResult("obtenerfactura", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception Ex)
            {
                return BadRequest(Ex.Message);
            }
        }

        [HttpPost("detalles/{facturaId}")]
        public async Task<ActionResult> PostDetalle(int facturaId, [FromBody] FacturaTipoObligacionConceptosDTOCR facturaTipoObligacionConceptosDTOCR)
        {
            try
            {
                // 1. Validar si la factura existe
                var nuevoRegistroId = 0;
                var facturaExistente = await context.FacturaRegistro.FindAsync(facturaId);

                if (facturaExistente == null)
                {
                    return NotFound(new { message = "La factura no existe." });
                }

                // 2. Verificar la existencia de registros en la tabla FacturaTipoObligacion
                var facturaTipoObligacionExistente = await context.FacturaTipoObligacion
                    .FirstOrDefaultAsync(x => x.FacturaRegistroId == facturaId && x.TipoObligacionId == facturaTipoObligacionConceptosDTOCR.FacturaTipoObligacionId);

                if (facturaTipoObligacionExistente == null)
                {
                    // 3. Si no existe, agregamos el nuevo registro
                    nuevoRegistroId = await AgregarNuevoRegistroAsync(facturaId, facturaTipoObligacionConceptosDTOCR);
                }else
                {
                    nuevoRegistroId = facturaTipoObligacionExistente.Id;
                }


                // Resto del proceso
                var entidad = ConfigurarEntidad(facturaTipoObligacionConceptosDTOCR, nuevoRegistroId);

                context.Add(entidad);
                await context.SaveChangesAsync();

                var entidadDTO = mapper.Map<FacturaTipoObligacionConceptosDTO>(entidad);
                return new CreatedAtRouteResult("obtenerfactura", new { id = entidadDTO.Id }, entidadDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private async Task<int> AgregarNuevoRegistroAsync(int facturaId, FacturaTipoObligacionConceptosDTOCR facturaTipoObligacionConceptosDTOCR)
        {
            var nuevaFacturaTipoObligacion = new FacturaTipoObligacion
            {
                FacturaRegistroId = facturaId,
                TipoObligacionId = facturaTipoObligacionConceptosDTOCR.FacturaTipoObligacionId
                // Asignar otros valores según la necesidad
            };

            context.FacturaTipoObligacion.Add(nuevaFacturaTipoObligacion);
            await context.SaveChangesAsync();

            return nuevaFacturaTipoObligacion.Id;
        }

        private FacturaTipoObligacionConceptos ConfigurarEntidad(FacturaTipoObligacionConceptosDTOCR facturaTipoObligacionConceptosDTOCR, int nuevoRegistroId)
        {
            var entidad = mapper.Map<FacturaTipoObligacionConceptos>(facturaTipoObligacionConceptosDTOCR);
            entidad.FacturaTipoObligacionId = nuevoRegistroId;
            //-----------------------------------------------------------------------------------------
            //entidad.UserName = userName;
            entidad.FechaCreacion = DateTime.Now;
            entidad.FechaModificacion = DateTime.Now;
            //-----------------------------------------------------------------------------------------
            return entidad;
        }



        //[HttpPost("detalles/{facturaId}")]
        //public async Task<ActionResult> PostDetalle(int facturaId, [FromBody] FacturaTipoObligacionConceptosDTOCR facturaTipoObligacionConceptosDTOCR)
        //{
        //    try
        //    {
        //        var facturaExistente = await context.FacturaRegistro.FindAsync(facturaId);

        //        if (facturaExistente == null)
        //        {
        //            return NotFound(new { message = "La factura no existe." });
        //        }

        //        var estadoInicialFactura = await context.FacturaEstado.FirstOrDefaultAsync(x => x.Codigo == "001");

        //        //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
        //        var entidad = mapper.Map<FacturaTipoObligacionConceptos>(facturaTipoObligacionConceptosDTOCR);
        //        //-----------------------------------------------------------------------------------------
        //        //entidad.UserName = userName;
        //        entidad.FechaCreacion = DateTime.Now;
        //        entidad.FechaModificacion = DateTime.Now;
        //        //-----------------------------------------------------------------------------------------
        //        context.Add(entidad);
        //        await context.SaveChangesAsync();
        //        var entidadDTO = mapper.Map<FacturaTipoObligacionConceptosDTO>(entidad);
        //        return new CreatedAtRouteResult("obtenerfactura", new { id = entidadDTO.Id }, entidadDTO);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] FacturaRegistroDTO facturaRegistroDTO)
        {
            var existe = await context.FacturaRegistro.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            //var userName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
            var entidad = mapper.Map<FacturaRegistro>(facturaRegistroDTO);
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
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<FacturaRegistroDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var entidadDB = await context.FacturaRegistro.FirstOrDefaultAsync(x => x.Id == id);

            if (entidadDB == null)
            {
                return NotFound();
            }

            var entidadDTO = mapper.Map<FacturaRegistroDTO>(entidadDB);
            patchDocument.ApplyTo(entidadDTO, ModelState);

            var esValido = TryValidateModel(entidadDTO);
            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            mapper.Map(entidadDTO, entidadDB);
            entidadDB.FechaModificacion = DateTime.Now;

            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.FacturaRegistro.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }

            var entidad = await context.FacturaRegistro.FirstOrDefaultAsync(x => x.Id == id);
            entidad.Id = id;
            entidad.Estado = false;
            context.Entry(entidad).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        private async Task<bool> ValidarExistencia(FacturaRegistroDTOCR facturaRegistroDTOCR)
        {
            var ValidarExistencia = await context.FacturaRegistro.AnyAsync(x => (x.SedeId == facturaRegistroDTOCR.SedeId && x.EntidadId == facturaRegistroDTOCR.EntidadId  && x.FacturaNumero == facturaRegistroDTOCR.FacturaNumero));
            return ValidarExistencia;
        }

    }
}
