using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

        [HttpGet("users/{id}")]
        public override IActionResult GetById(string id){
            return base.GetById(id);
        }

        [HttpPost("users")]
        public IActionResult CrearUsuario([FromBody]UsuarioValidator atributosUsuario)
        {
            Usuario nuevoUsuario = _userService.CreateUser(atributosUsuario);
            if (nuevoUsuario == null)
            {
                return BadRequest(new { message = "No se pudo crear el usuario." });
            }
            return Ok(nuevoUsuario);
        }


        [HttpPatch("users/{id}/editar")]
        public IActionResult EditarUsuario(string id, [FromBody]UsuarioValidator atributosUsuario)
        {
            var usuario = _userService.GetById(id);

            if (usuario == null)
            {
                return NotFound(new { message = "Usuario no encontrado." });
            }

            usuario = _userService.UpdateData(usuario, atributosUsuario);

            if (usuario == null)
            {
                return BadRequest(new { message = "El usuario no ha podido ser modificado." });
            }

            return Ok(usuario);
        }

        [HttpDelete("users/{id}/eliminar")]
        public IActionResult EliminarUsuario(string id)
        {
            var usuario = _userService.GetById(id);

            if (usuario == null)
            {
                return NotFound(new { message="Usuario no encontrado." });
            }

            if (_userService.DeteleUser(usuario))
            {
                return Ok(new { message = "Usuario eliminado." });
            }
            return BadRequest(new { message = "El usuario no ha podido ser eliminado." });
        }

        [HttpPatch("cuentas/{id}/editar")]
        public IActionResult EditarBalance([FromBody]CuentaValidator cuenta)
        {
            try
            {
                ulong? numCuenta = Convert.ToUInt64(cuenta.numCuenta);
                decimal? nuevoBalance = Convert.ToDecimal(
                    cuenta.nuevoBalance,
                    new NumberFormatInfo() { NumberDecimalSeparator = "." });

                ICuentaService cuentaService = new CuentaService(this.context);

                Cuenta cuentaOriginal = cuentaService.GetById(numCuenta);
                if (cuentaOriginal == null)
                {
                    return NotFound(new { message = "Cuenta no encontrada." });
                }

                cuentaService.SetBalance(cuentaOriginal, nuevoBalance);
                return Ok(new { numCuenta = cuentaOriginal.id, nuevoBalance = cuentaOriginal.balance });
            } catch (Exception)
            {
                return BadRequest(new { message = "Número de cuenta o balance en formato incorrecto." });
            }           
                        
        }

    }
}