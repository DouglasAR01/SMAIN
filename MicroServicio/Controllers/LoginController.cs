using MicroServicio.Contexts;
using MicroServicio.Entities;
using MicroServicio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        ///    Permite a un paciente hacer el login.
        /// </summary>
        /// <param name="userParam"> La cedula y la contrasela del usuario. </param>
        /// <remarks>
        ///     Request **simple**:
        ///         POST /api/login/authenticate
        ///         {
        ///         	"cedula":"101",
        ///         	"password":"prueba"
        ///         }
        /// </remarks>
        /// <response code="200"> Otorga los datos del usuario y el token. </response>
        /// <response code="400"> Cedula o contraseña incorrecta. </response>
        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody]Usuario userParam)
        {
            var user = _userService.Authenticate(userParam.cedula, userParam.password);

            if (user == null)
                return BadRequest(new { message = "Cedula o contraseña incorrecta" });

            return Ok(user);
        }

    }
}