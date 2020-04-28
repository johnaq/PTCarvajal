using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnica.Models
{
    public class LoginViewModel
    {
        [Required]
        public decimal Identificacion { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
