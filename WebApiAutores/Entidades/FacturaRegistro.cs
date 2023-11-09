using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebApiAutores.Entidades;

namespace AdminPagosApi.Entidades
{
    public class FacturaRegistro
    {
        public int Id { get; set; }
        public int SedeId { get; set; }
        public Sede Sede { get; set; }

        public int EntidadId { get; set; }
        public Entidad Entidad { get; set; }

        public string NumeroContrato { get; set; }  

        public string FacturaNumero { get; set; }
        public string ReferenciaPago { get; set; }
        public DateTime? FechaEmision  {get; set;}
        public DateTime? FechaPago { get; set; }
        public decimal ValorFactura { get; set; }
        public int FacturaEstadoId { get; set; }
        public FacturaEstado FacturaEstado { get; set; }  
        public string Nota { get; set; }    
        public DateTime? FechaUltimoPago { get; set; }
        public DateTime? FechaProximaFecha { get; set; }    

        public decimal ValorFacturaUltimoPago { get; set; }
        public string UrlFactura { get; set; }
        public bool Estado { get; set; }

        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        [JsonIgnore]
        public List<FacturaTipoObligacion> FacturasTipoObligacion { get; set; }

    }
}
