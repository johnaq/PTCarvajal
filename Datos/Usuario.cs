﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace Datos
{
    public partial class Usuario
    {
        public short UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public short TipIdentificacionId { get; set; }
        public decimal Identificcion { get; set; }
        public string Password { get; set; }
        public string Correo { get; set; }

        public virtual TipoIdentificacion TipIdentificacion { get; set; }
    }
}