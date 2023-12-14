using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class CoordinadorPgnDTO
    {
        public int Id { get; set; }
        public string Coodinacion { get; set; }
        public string Responsable { get; set; }
        public string Direccion { get; set; }
        public string email { get; set; }
        public string Telefono { get; set; }
        public bool Estado { get; set; }
        public string JefeCoordinadorNombre { get; set; }
        public string JefeCoordinadorEmail { get; set; }

        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}
