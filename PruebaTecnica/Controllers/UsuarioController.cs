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
    public class UsuarioController : ControllerBase
    {
        private readonly PruebaTecnicaContext _context;

        public UsuarioController(PruebaTecnicaContext context)
        {
            _context = context;
        }

        // GET: api/Usuario/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<UsuarioViewModel>> Listar()
        {
            var usuario = await _context.Usuario
                .Include(t => t.TipIdentificacion)
                .ToListAsync();
            return usuario.Select(c => new UsuarioViewModel
            {
                UsuarioId = c.UsuarioId,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Identificacion = c.Identificcion,
                Correo = c.Correo,
                TipoIdentificacion = c.TipIdentificacion.Codigo,
                TipIdentificacionId = c.TipIdentificacionId,
                Password = c.Password
            });
        }

        // GET: api/Usuario/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Usuario>> Mostrar(short id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(new UsuarioViewModel
            {
                UsuarioId = usuario.UsuarioId,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Identificacion = usuario.Identificcion,
                Correo = usuario.Correo,
            });
        }

        // PUT: api/Usuario/Actualizar
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar(ActualizarUsuarioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.UsuarioId <= 0)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuario.FirstOrDefaultAsync(c => c.UsuarioId == model.UsuarioId);

            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Nombre = model.Nombre;
            usuario.Apellido = model.Apellido;
            usuario.TipIdentificacionId = model.TipIdentificacionId;
            usuario.Identificcion = model.Identificcion;
            usuario.Correo = model.Correo;
            usuario.Password = model.Password;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepción
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Usuario/Crear
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost("[action]")]
        public async Task<ActionResult<Usuario>> Crear(CrearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Usuario usuario = new Usuario
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                TipIdentificacionId = model.TipIdentificacionId,
                Identificcion = model.Identificcion,
                Correo = model.Correo,
                Password = model.Password
            };

            _context.Usuario.Add(usuario);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //logs
                return BadRequest();
            }


            return Ok();
        }

        // DELETE: api/Usuario/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<Usuario>> Eliminar(short id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuario.Remove(usuario);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                return BadRequest();
            }
            return Ok(usuario);
        }

        // GET: api/Usuario/Login
        [HttpPost("[action]")]
        public async Task<ActionResult<Usuario>> Login(LoginViewModel model)
        {
            var usuario = await _context.Usuario.Where(x => x.Identificcion.Equals(model.Identificacion) && x.Password.Equals(model.Password)).FirstOrDefaultAsync();

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
