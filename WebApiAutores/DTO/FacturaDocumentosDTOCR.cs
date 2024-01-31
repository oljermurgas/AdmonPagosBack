
using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class FacturaDocumentosDTOCR
    {
        public int FacturaRegistroId { get; set; }
        public int TipoDocumentosId { get; set; }
        public string Nota { get; set; }
        public string url { get; set; }
        public string NombreArchivo { get; set; }

    }
}
