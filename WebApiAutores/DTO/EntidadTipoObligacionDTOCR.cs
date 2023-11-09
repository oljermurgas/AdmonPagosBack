using AdminPagosApi.Entidades;

namespace AdminPagosApi.DTO
{
    public class EntidadTipoObligacionDTOCR
    {

        public string NumeroContrato { get; set; }
        public string NumeroPagoElectronico { get; set; }

        public bool Estado { get; set; }

        public int EntidadId { get; set; }

        public int TipoObligacionId { get; set; }
        public int TipoTarifaId { get; set; }
        public int PeriodicidadFactura { get; set; }

    }
}
