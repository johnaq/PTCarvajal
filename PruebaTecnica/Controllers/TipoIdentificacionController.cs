using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Datos;
using PruebaTecnica.Models;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoIdentificacionController : ControllerBase
    {
        private readonly PruebaTecnicaContext _context;

        public TipoIdentificacionController(PruebaTecnicaContext context)
        {
            _context = context;
        }

        // GET: api/TipoIdentificacion/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<TipoIdentificacionViewModel>> Listar()
        {
            var tipoIdentificacion = await _context.TipoIdentificacion.ToListAsync();
            return tipoIdentificacion.Select(c => new TipoIdentificacionViewModel
            {
                Id = c.TipIdentificacionId,
                Nombre = c.Codigo+" - "+c.Descripcion
            });
        }
    }
}
