using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Models
{
    public class UsuarioViewModel
    {
        public short UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string TipoIdentificacion { get; set; }
        public decimal Identificacion { get; set; }
        public short TipIdentificacionId { get; set; }
        public string Correo { get; set; }
        public string Password { get; set; }
    }
}
