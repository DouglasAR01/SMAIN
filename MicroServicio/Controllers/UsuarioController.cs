using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using MicroServicio.Contexts;
using MicroServicio.Entities;
using MicroServicio.Services;
using MicroServicio.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroServicio.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        protected IUserService _userService;

        protected readonly AppDbContext context;
        public UsuarioController(AppDbContext context, IUserService userService)
        {
            this.context = context;
            _userService = userService;
        }
        

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var user = _userService.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            // only allow admins to access other user records
            var currentUserId = User.Identity.Name;
            if (id != currentUserId && !User.IsInRole("a"))
            {
                return Forbid();
            }

            return Ok(user);
        }

        [HttpGet("cuentas")]
        public IActionResult GetMisCuentas()
        {
            ICuentaService cuentaObj = new CuentaService(this.context);
            IEnumerable<Cuenta> cuentas = cuentaObj.GetByUser(User.Identity.Name);
            return Ok(cuentas);
        }

        /// <summary>
        ///     Realiza una transaccion de uns cuenta a otra
        /// </summary>
        /// <param name="data"> </param>
        /// <remarks>
        ///     Request **simple**:
        ///         POST /api/usuario/transferir
        ///         {
        ///         	"numCuentaOrigen":2,
        ///         	"numCuentaDestino":3,
        ///         	"nameDestino":"James",
        ///         	"monto":12
        ///         }
        /// </remarks>
        /// <response code="200"> Transaccion efectuada con exito. </response>
        /// <response code="401"> No logeado o intenta transferir de una cuenta de la que no es propietaria. </response>
        [HttpPost("transferir")]
        public IActionResult Transferir([FromBody]DatosTransferir data )
        {
            try{
                // Casting
                decimal? monto = Convert.ToDecimal(data.monto);
                ulong? numCuentaOrigen = Convert.ToUInt64(data.numCuentaOrigen);
                ulong? numCuentaDestino = Convert.ToUInt64(data.numCuentaDestino);

                ICuentaService cuentaService = new CuentaService(this.context);

                //Verifica el monto
                if( monto <= 0){
                    return BadRequest(new { message = "El monto a transferir no puede ser negativo o igual a 0." });
                }

                //Existen las cuentas?
                if (cuentaService.GetById(numCuentaOrigen) == null)
                {
                    return NotFound(new { message = "Cuenta de origen no encontrada." });
                }
                if (cuentaService.GetById(numCuentaDestino) == null)
                {
                    return NotFound(new { message = "Cuenta de destino no encontrada." });
                }

                //Veridica que el usuario logeado sea el propietario de la cuenta de origen
                if ( !cuentaService.IsUserOwner(numCuentaOrigen, User.Identity.Name) ){
                    return Forbid();
                }

                //Verifica que el nombre otorgado del usuario de desstino corresponda con la cuenta el nombre del propietario de la cuenta de destino
                if ( !cuentaService.IsNameUserOwner(numCuentaDestino, data.nameDestino) ){
                    return BadRequest(new { message = "El nombre no coincide con el propietario de la cuenta de destino." });
                }

                //Verifica que tiene fondos sufucientes
                if ( !cuentaService.IsEnoughMoney(numCuentaOrigen, monto) ){
                    return BadRequest(new { message = "No tiene los fondos suficientes para realizar esta transaccion." });
                }

                cuentaService.Transaccion(numCuentaOrigen, numCuentaDestino, monto);
                return Ok(new { message = "Transaccion efectuada con exito." });

            }catch(Exception){
                return BadRequest(new { message = "Error en el servidor." });
            }
        }

    }
}
