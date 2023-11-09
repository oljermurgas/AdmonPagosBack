using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class EntidadDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Identificacion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        public string Direccion { get; set; }

        public string Telefono { get; set; }
        public string Nivel { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public int TipoEmpresaId { get; set; } // Natural | Juridica
        public TipoEmpresa TipoEmpresas { get; set; }

        public int TipoEmpresaSectorId { get; set; } // Industrial | Minero | Impuesto | Privada
        public TipoEmpresaSector TipoEmpresaSectores { get; set; }

        public int TipoEmpresaNivelId { get; set; } //Municipal | Departamantal | Nacional
        public TipoEmpresaNivel TipoEmpresaNiveles { get; set; }

        public int DepartamentoId { get; set; }
        public Departamento Departamentos { get; set; }

        public int MunicipioId { get; set; }
        public Municipio Municipios { get; set; }

        public bool Estado { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}
