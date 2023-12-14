using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class FacturaTipoObligacionConceptosDTO
    {
        public int Id { get; set; }
        public int FacturaTipoObligacionId { get; set; }
        public FacturaTipoObligacion FacturaTipoObligacion { get; set; }
        public int TipoConceptoFacturacionId { get; set; }
        public TipoConceptoFacturacion TipoConceptoFacturacion { get; set; }
        public decimal Valor { get; set; }
        public string Nota { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
