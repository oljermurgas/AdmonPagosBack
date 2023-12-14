using AdminPagosApi.Entidades;

namespace AdminPagosApi.DTO
{
    public class EntidadTipoObligacionDTOCR
    {
        public bool Estado { get; set; }

        public int EntidadId { get; set; }

        public int TipoObligacionId { get; set; }
        public int TipoTarifaId { get; set; }

    }
}
