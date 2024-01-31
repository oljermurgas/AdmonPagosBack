using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPagosApi.DTO
{
    public class PaginacionDTO
    {
        public int pagina { get; set; } = 1;
        private int cantidadRegistroPorPagina = 10;
        private readonly int cantidadMaximaRegistrosporPaginas = 50;
        
        public int CantidadRegistrosPorPagina
        {
            get => cantidadRegistroPorPagina;
            set
            {
                cantidadRegistroPorPagina = (value > cantidadMaximaRegistrosporPaginas) ? cantidadMaximaRegistrosporPaginas: value;
            }
        }
    }
}
