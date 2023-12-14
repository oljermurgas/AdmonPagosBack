using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class SedeEntidadDTO
    {
        public int Id { get; set; }

        public string Notas { get; set; }

        public Entidad Entidades { get; set; }

        public string NumeroContrato { get; set; }
        public string NumeroContador { get; set; }
    }
}
