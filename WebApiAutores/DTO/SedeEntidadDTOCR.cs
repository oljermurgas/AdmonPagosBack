using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class SedeEntidadDTOCR
    {
        public string Notas { get; set; }

        public int SedeId { get; set; }

        public int EntidadId { get; set; }

        public string NumeroContrato { get; set; }
        public string NumeroContador { get; set; }

    }
}
