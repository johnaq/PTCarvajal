using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Models
{
    public class ActualizarUsuarioViewModel
    {
        [Required]
        public short UsuarioId { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El nombre debe ser de maximo 30 caracteres")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "El apellido debe ser de maximo 30 caracteres")]
        public string Apellido { get; set; }

        [Required]
        public short TipIdentificacionId { get; set; }

        [Required]
        public decimal Identificcion { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }
    }
}
