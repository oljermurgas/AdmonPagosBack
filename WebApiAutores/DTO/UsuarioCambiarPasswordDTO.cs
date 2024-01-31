using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPagosApi.DTO
{
    public class UsuarioCambiarPasswordDTO
    { 
        public string passwordAnterior { get; set; }
        public string nuevaPassword { get; set; }
    }
}
