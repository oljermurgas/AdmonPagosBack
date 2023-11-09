namespace WebApiAutores.Entidades
{
    public class DepartamentoMunicipio
    {
        public int Id { get; set; }
        public decimal CodMun { get; set; }
        public string MunEsCapital { get; set; }
        public decimal MunZonaB { get; set; }
        public string Nombre { get; set; }
        public decimal DepartamentoId { get; set; }
        public Departamento Departamento { get; set; }
        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}