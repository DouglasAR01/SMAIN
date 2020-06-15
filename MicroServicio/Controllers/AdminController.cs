using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MicroServicio.Contexts;
using MicroServicio.Entities;
using MicroServicio.Services;
using MicroServicio.SwaggerExamples.Requests;
using MicroServicio.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace MicroServicio.Controllers
{
    [ApiController]
    [Authorize(Roles = "a")]
    [Route("api/[controller]")]
    public class AdminController : UsuarioController
    {

        public AdminController(AppDbContext context, IUserService userService):base(context,userService){}

        /// <summary>
        ///     Permite al administrador ver todos los usuarios del sistema.
        /// </summary>
        /// <response code="200"> Todos los datos de los usuarios. </response>
        /// <response code="401"> No es administrador, por lo tanto prohibe el acceso.</response>
        [HttpGet("users")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetAll());
        }

        /// <summary>
        ///     Permite al administrador ver a usuarios de forma individual en el sistema.
        /// </summary>
        /// <param name="id" example="103">La cédula del usuario a visualizar</param>
        /// <response code="200"> Datos individuales de un usuario. </response>
        /// <response code="401"> No es administrador, por lo tanto prohibe el acceso.</response>
        /// <response code="404"> El usuario al que se quiere visualizar no fue encontrado.</response>
        [HttpGet("users/{id}")]
        public override IActionResult GetById(string id){
            return base.GetById(id);
        }

        /// <summary>
        ///     Permite al administrador crear un nuevo usuario.
        /// </summary>
        /// <response code="200"> Retorna el usuario recién creado. </response>
        /// <response code="400"> No se pudo crear un usuario debido posiblemente a que la cédula es incorrecta.</response>
        /// <response code="401"> No es administrador, por lo tanto prohibe el acceso.</response>
        
        [HttpPost("users")]
        [SwaggerRequestExample(typeof(UsuarioValidator),typeof(CrearUsuarioExample))]
        public IActionResult CrearUsuario([FromBody]UsuarioValidator atributosUsuario)
        {
            Usuario nuevoUsuario = _userService.CreateUser(atributosUsuario);
            if (nuevoUsuario == null)
            {
                return BadRequest(new { message = "No se pudo crear el usuario." });
            }
            return Ok(nuevoUsuario);
        }

        /// <summary>
        ///     Permite al administrador modificar la información de un usuario.
        ///     Requiere que se le envíen todos los datos del usuario.
        /// </summary>
        /// <param name="id" example="102">La cédula del usuario a modificar</param>
        /// <response code="200"> Retorna la información del usuario modificado. </response>
        /// <response code="400"> No se pudo modificar el usuario, posiblemente porque su cédula es incorrecta.</response>
        /// <response code="401"> No es administrador, por lo tanto prohibe el acceso.</response>
        /// <response code="404"> El usuario al que se quiere modificar no fue encontrado.</response>
        [HttpPatch("users/{id}/editar")]
        [SwaggerRequestExample(typeof(UsuarioValidator),typeof(EditarUsuarioExample))]
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

        /// <summary>
        ///     Permite al administrador eliminar un usuario.
        /// </summary>
        /// <param name="id" example="400">Cédula del usuario a eliminar</param>
        /// <response code="200"> El usuario fue eliminado correctamente. </response>
        /// <response code="400"> No se pudo eliminar el usuario, posiblemente porque su cédula es incorrecta.</response>
        /// <response code="401"> No es administrador, por lo tanto prohibe el acceso.</response>
        /// <response code="404"> El usuario al que se quiere eliminar no fue encontrado.</response>
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

        /// <summary>
        ///     Permite al administrador ver todas las cuentas del sistema.
        /// </summary>
        /// <response code="200"> Retorna todas las cuentas registradas en el sistema. </response>
        /// <response code="401"> No es administrador, por lo tanto prohibe el acceso.</response>
        [HttpGet("cuentas")]
        public override IActionResult GetMisCuentas()
        {
            ICuentaService cuentaService = new CuentaService(this.context);
            return Ok(cuentaService.GetAll());
        }

        /// <summary>
        ///     Permite al administrador modificar el balance de una cuenta.
        ///     Se debe de enviar el número del nuevo balance. Por ejemplo, si se le quieren restar
        ///     $200 a un balance de $1000, el número del balance que se debe de enviar es el resultado,
        ///     es decir, $800 en este caso.
        /// </summary>
        /// <response code="200"> Retorna la información de la cuenta modificada. </response>
        /// <response code="400"> El formato del número de cuenta o del balance es incorrecto.</response>
        /// <response code="401"> No es administrador, por lo tanto prohibe el acceso.</response>
        /// <response code="404"> La cuenta a editar el balance no fue encontrada.</response>
        [HttpPatch("cuentas/editar")]
        [SwaggerRequestExample(typeof(CuentaValidator), typeof(EditarBalanceExample))]
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