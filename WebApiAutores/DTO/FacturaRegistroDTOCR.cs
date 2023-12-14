using AdminPagosApi.Entidades;

namespace AdminPagosApi.DTO
{
    public class FacturaRegistroDTOCR
    {
        public int SedeId { get; set; }
        public Sede Sede { get; set; }

        public int EntidadId { get; set; }
        public Entidad Entidad { get; set; }

        public string NumeroContrato { get; set; }

        public string FacturaNumero { get; set; }
        public string ReferenciaPago { get; set; }
        public DateTime? FechaEmision { get; set; }
        public DateTime? FechaOportunoPago { get; set; }
        public decimal ValorFactura { get; set; }
        public int FacturaEstadoId { get; set; }
        public FacturaEstado FacturaEstado { get; set; }
        public string Nota { get; set; }
        public DateTime? FechaPeriodoFacturaInicio { get; set; }
        public DateTime? FechaPeriodoFacturaFin { get; set; }
        public bool PagoInmediato { get; set; }

        public decimal ValorFacturaUltimoPago { get; set; }
        public string UrlFactura { get; set; }

        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
