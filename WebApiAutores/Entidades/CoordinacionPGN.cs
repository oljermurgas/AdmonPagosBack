using AdminPagosApi.Entidades;
using Microsoft.AspNetCore.Identity;

namespace WebApiAutores.Entidades
{
    public class CoordinacionPGN
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
        public string UsuarioId { get; set; }
        public ApplicationUser Usuario { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}
