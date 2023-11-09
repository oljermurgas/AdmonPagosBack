namespace WebApiAutores.Entidades
{
    public class Municipio
    {
        public int Id { get; set; }
        public int CodMun { get; set; }
        public string Nombre { get; set; }

        public int CodDep { get; set; }

        public Departamento Departamento { get; set; }

        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}