﻿using AdminPagosApi.Entidades;
using System.ComponentModel.DataAnnotations;
using WebApiAutores.Entidades;

namespace AdminPagosApi.DTO
{
    public class CoordinadorPgnDTOCR
    {
        public string Coodinacion { get; set; }
        public string Responsable { get; set; }
        public string Direccion { get; set; }
        public string email { get; set; }
        public string Telefono { get; set; }
        public string JefeCoordinadorNombre { get; set; }
        public string JefeCoordinadorEmail { get; set; }
    }
}
