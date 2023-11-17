using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.Entidades
{
    public class Sede
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string IdentificacionHominis { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }

        public string CedulaCatastral { get; set; }
        public string MatriculaInnoviliaria { get; set; }

        public int TipoVinculacionContractualId { get; set; } //Arriendo | Contrato
        public TipoVinculacionContractual TipoVinculacionContractuales { get; set; }

        public int TipoInmuebleId { get; set; } //Arriendo | Contrato
        public TipoInmueble TipoInmuebles { get; set; }

        public int DepartamentoId { get; set; }
        public Departamento Departamentso { get; set; }

        public int MunicipioId { get; set; }
        public Municipio Municipios { get; set; }

        public bool Estado { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        public List<SedeContrato> SedeContratos { get; set; }
        public List<CoordinacionPGNSede> CoordinacionPGNSedes { get; set; }

    }
}
