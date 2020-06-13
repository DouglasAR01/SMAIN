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
    [Route("")]
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
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody]Usuario userParam)
        {
            Debug.WriteLine("Entro a authenticate------------------------------------");
            var user = _userService.Authenticate(userParam.cedula, userParam.password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Authorize("1")]
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

        // GET api/<controller>/5
        /*[HttpGet("{id}")]
        public string Get(string id)
        {
            var resultado = context.Usuario.Find(id);
            return resultado.ToString();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
