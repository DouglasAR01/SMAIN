using MicroServicio.Contexts;
using MicroServicio.Entities;
using MicroServicio.Services;
using MicroServicio.SwaggerExample.Request;
using MicroServicio.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace MicroServicio.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private IUserService _userService;

        private readonly AppDbContext context;
        public LoginController(AppDbContext context, IUserService userService)
        {
            this.context = context;
            _userService = userService;
        }

        /// <summary>
        ///    Permite a un usuario hacer el login.
        /// </summary>
        /// <param name="userParam"> La cedula y la contraseña del usuario. </param>
        /// <response code="200"> Otorga los datos del usuario y el token. </response>
        /// <response code="400"> Cedula o contraseña incorrecta. </response>
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        [SwaggerRequestExample(typeof(Usuario), typeof(AuthenticateExample))]
        public IActionResult Authenticate([FromBody]LoginValidator userParam)
        {
            var user = _userService.Authenticate(userParam.cedula, userParam.password);

            if (user == null)
                return BadRequest(new { message = "Cedula o contraseña incorrecta" });

            return Ok(user);
        }

    }
}