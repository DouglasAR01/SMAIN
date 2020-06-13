using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MicroServicio.Contexts;
using MicroServicio.Entities;
using MicroServicio.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MicroServicio.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private IUserService _userService;

        private readonly AppDbContext context;
        public UsuarioController(AppDbContext context, IUserService userService)
        {
            this.context = context;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]Usuario userParam)
        {
            Debug.WriteLine("Entro a authenticate------------------------------------");
            var user = _userService.Authenticate(userParam.cedula, userParam.password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Authorize(Roles = "a")]
        [HttpGet]
        public IActionResult GetAll()
        {
            Debug.WriteLine("Entro a getAll------------------------------------");
            var users = _userService.GetAll();
            return Ok(users);
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
            if (id != currentUserId && !User.IsInRole("1"))
            {
                return Forbid();
            }

            return Ok(user);
        }

    }
}
