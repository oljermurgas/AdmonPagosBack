using AdminPagosApi.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class TipoVinculacionContractualDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }

    public class TipoTarifaDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }


    public class TipoPagoAdmonDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }


    public class TipoObligacionDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        public int TipoPagoAdmonId { get; set; }
        public TipoPagoAdmonDTO TipoPagoAdmon { get; set; }
    }

    public class TipoInmuebleDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }

    public class TipoEmpresaSectorDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }

    public class TipoEmpresaNivelDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }

    public class TipoConceptoFacturacionDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        public int TipoPagoAdmonId { get; set; }
        public TipoPagoAdmon TipoPagoAdmon { get; set; }
    }

    public class TipoCanalEnvioFacturaDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }

    public class TipoEmpresaDTO
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int UsuarioId { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }

    public class DepartamentoDTO
    {
        public int Id { get; set; }
        public decimal CodDep { get; set; } 
        public string Nombre { get; set; } 
    }

    public class MunicipioDTO
    {
        public int Id { get; set; }
        public decimal CodDep { get; set; }
        public string Nombre { get; set; }
        public decimal CodMun { get; set; }
        public Departamento Departamento { get; set; }
    }


    public class EntidadResultadoDTO
    {
        public string NumeroDelContador { get; set; }
        public UltimaFacturaDTO UltimaFactura { get; set; }
    }

    public class UltimaFacturaDTO
    {
        public DateTime Fecha { get; set; }
        public decimal Valor { get; set; }
    }
}
