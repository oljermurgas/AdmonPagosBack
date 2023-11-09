using System.Text.Json.Serialization;
namespace AdminPagosApi.Entidades
{
    public class FacturaTipoObligacion
    {
        public int Id { get; set; }
        public int FacturaRegistroId { get; set; }
        //[JsonIgnore]
        //public FacturaRegistro FacturaRegistro { get; set; }

        public int TipoObligacionId  { get; set; }
        public TipoObligacion TipoObligacion { get; set; }

        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

    }
}
