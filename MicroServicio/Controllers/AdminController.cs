using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServicio.Contexts;
using MicroServicio.Entities;
using MicroServicio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicio.Controllers
{
    [ApiController]
    [Authorize(Roles = "a")]
    [Route("api/[controller]")]
    public class AdminController : UsuarioController
    {

        public AdminController(AppDbContext context, IUserService userService):base(context,userService){}

        [HttpPatch]
        [Route("{id}/editar")]
        public IActionResult EditarUsuario(string id, [FromBody]Usuario atributosUsuario)
        {
            var usuario = _userService.GetById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            if (ValidarDatosUsuario(atributosUsuario))
            {
                return BadRequest();
            }
            usuario = _userService.UpdateData(usuario, atributosUsuario);

            if (usuario == null)
            {
                return BadRequest();
            }

            return Ok(atributosUsuario);
        }

        /*[HttpDelete]
        [Route]
        public IActionResult eliminarUsuario()
        {
            
        }
        */

        // Retorna true si todas las validaciones se cumplen
        private Boolean ValidarDatosUsuario(Usuario usuario)
        {
            if(!(usuario.role.Equals('a') || usuario.role.Equals('u')))
            {
                return false;
            }

            if (!ulong.TryParse(usuario.cedula, out _))
            {
                return false;
            }

            return true;
        }
    }
}