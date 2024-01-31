using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPagosApi.Entidades
{
    public class URol
    {

        [Required]
        public string RolName { get; set; }
        //[Required]
        //public string NormalizedName { get; set; }
    }
}
