using AdminPagosApi.Entidades;
using Microsoft.Identity.Client;

namespace AdminPagosApi.DTO
{
    public class EntidadTipoObligacionDTO
    {
        public int Id { get; set; }

        public bool Estado { get; set; }

        public int EntidadId { get; set; }

        public int TipoObligacionId { get; set; }
        public int TipoTarifaId { get; set; }

        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        public TipoObligacionDTO TipoObligacion { get; set; }
    }
}
