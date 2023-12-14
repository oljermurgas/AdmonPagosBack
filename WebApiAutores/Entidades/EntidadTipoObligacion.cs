using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;

namespace WebApiAutores.Entidades
{
    public class EntidadTipoObligacion
    {
        public int Id { get; set; }

        public bool Estado { get; set; }

        public int EntidadId { get; set;}
        public Entidad Entidades { get; set; }

        public int TipoObligacionId { get; set; }
        public TipoObligacion TipoObligaciones { get; set; }
        public int TipoTarifaId { get; set; }
        public TipoTarifa TipoTarifas { get; set; }

        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
       
    }
}
