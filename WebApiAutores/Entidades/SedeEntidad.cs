using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class SedeEntidad // Sede con las entidades que se debe realizar pagos
    {
        public int Id { get; set; }

        public string Notas { get; set; }

        public int SedeId { get; set;}
        public Sede Sedes { get; set; }

        public int EntidadId { get; set; }
        public Entidad Entidades { get; set; }

        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;

    }
}
