namespace AdminPagosApi.Entidades
{
    public class FacturaDocumentos
    {
        public int Id { get; set; }
        public int FacturaRegistroId { get; set; }
        public FacturaRegistro  FacturaRegistro { get; set; }

        public int TipoDocumentosId { get; set; }
        public TipoDocumentos TipoDocumentos { get; set; }

        public string Nota { get; set; }

        public string url { get; set; }
       
        public bool Estado { get; set; }
        public string NombreArchivo { get; set; }

        public int UsuarioId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
