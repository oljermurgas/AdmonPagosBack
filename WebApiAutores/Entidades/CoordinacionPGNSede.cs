using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class CoordinacionPGNSede
    {
        public int Id { get; set; }

        public int CoordinacionPGNId { get; set; }
        public CoordinacionPGN CoordinacionPGNs { get; set; }

        public int SedeId { get; set;}
        public Sede Sedes { get; set; }

        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}
