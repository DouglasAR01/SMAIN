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