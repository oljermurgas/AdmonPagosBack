namespace WebApiAutores.Entidades
{
    public class Departamento
    {
        public int Id { get; set; }
        public int CodDep { get; set; }
        public string Nombre { get; set; }
        public List<Municipio> Municipios { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}