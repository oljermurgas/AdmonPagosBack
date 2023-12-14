using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class FacturaTipoObligacionConceptosDTOCR
    {
        public int FacturaTipoObligacionId { get; set; }
        public int TipoConceptoFacturacionId { get; set; }
        public decimal Valor { get; set;  }
    }
}
