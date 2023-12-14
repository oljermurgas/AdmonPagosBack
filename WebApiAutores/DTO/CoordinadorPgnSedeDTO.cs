using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class CoordnadorPgnSedeDTO
    {
        public int Id { get; set; }

        public int CoordinacionPGNId { get; set; }

        public int SedeId { get; set; }

      //  public CoordinacionPGN CoordinacionPGNs { get; set; }
     //   public Sede Sedes { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        public string NombreSede { get; set; }
        public string DireccionSede { get; set; }

        // public List<CoordinacionPGN> CoordinacionPGNs { get; set; }

        //   public List<Sede> Sedes { get; set; }
    }
}
