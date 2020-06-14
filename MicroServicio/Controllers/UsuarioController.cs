using System;
using System.Collections.Generic;
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

        [HttpPost("transferir")]
        public IActionResult Transferir([FromBody]DatosTransferir data )
        {

            ICuentaService cuentaService = new CuentaService(this.context);

            //Verifica el monto
            if( data.monto <= 0){
                return BadRequest(new { message = "El monto a transferir no puede ser negativo o igual a 0." });
            }

            //Veridica que el usuario logeado sea el propietario de la cuenta de origen
            if ( !cuentaService.IsUserOwner(data.numCuentaOrigen, User.Identity.Name) ){
                return Forbid();
            }

            //Verifica que el nombre otorgado del usuario de desstino corresponda con la cuenta el nombre del propietario de la cuenta de destino
            if ( !cuentaService.IsNameUserOwner(data.numCuentaDestino, data.nameDestino) ){
                return BadRequest(new { message = "El nombre no coincide con el propietario de la cuenta de destino." });
            }

            //Verifica que tiene fondos sufucientes
            if ( !cuentaService.IsEnoughMoney(data.numCuentaOrigen, data.monto) ){
                return BadRequest(new { message = "No tiene los fondos suficientes para realizar esta transaccion." });
            }

            try{
                cuentaService.Transaccion(data.numCuentaOrigen, data.numCuentaDestino, data.monto);
                return Ok(new { message = "Transaccion efectuada con exito." });
            }catch(Exception){
                return BadRequest(new { message = "Error en el servidor." });
            }
        }

    }
}
