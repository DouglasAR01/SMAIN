using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServicio.Contexts;
using MicroServicio.Entities;
using MicroServicio.Services;
using MicroServicio.Validators;
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

        [HttpGet("users")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpPost("users")]
        public IActionResult CrearUsuario([FromBody]UsuarioValidator atributosUsuario)
        {
            if (!UsuarioValidator.ValidarDatosUsuario(atributosUsuario))
            {
                return BadRequest();
            }
            Usuario nuevoUsuario = _userService.CreateUser(atributosUsuario);
            if (nuevoUsuario == null)
            {
                return BadRequest();
            }
            return Ok(nuevoUsuario);
        }


        [HttpPatch("users/{id}/editar")]
        public IActionResult EditarUsuario(string id, [FromBody]UsuarioValidator atributosUsuario)
        {
            var usuario = _userService.GetById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            if (!UsuarioValidator.ValidarDatosUsuario(atributosUsuario))
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

        [HttpDelete("users/{id}/eliminar")]
        public IActionResult EliminarUsuario(string id)
        {
            var usuario = _userService.GetById(id);

            if (usuario == null)
            {
                return NotFound();
            }

            if (_userService.DeteleUser(usuario))
            {
                return Ok();
            }
            return BadRequest();
        }

        
    }
}