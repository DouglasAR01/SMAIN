using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MicroServicio.Entities;
using MicroServicio.Helpers;
using MicroServicio.Contexts;

namespace MicroServicio.Services
{
    public interface IUserService { 
        Usuario Authenticate(string cedula, string password);
        IEnumerable<Usuario> GetAll();
        Usuario GetById(string id);
        Usuario UpdateData(Usuario usuario, Usuario atributosUsuario);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly AppDbContext _context;

        public UserService(IOptions<AppSettings> appSettings, AppDbContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public Usuario Authenticate(string cedula, string password)
        {
            var user = _context.Usuario.Where( x=> x.cedula == cedula  && x.password == password).FirstOrDefault();

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.cedula.ToString()),
                    new Claim(ClaimTypes.Role, user.role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.password = null;

            return user;
        }

        public IEnumerable<Usuario> GetAll()
        {
            // return users without passwords
            return _context.Usuario.ToList();
        }

        public Usuario GetById(string id)
        {
            var user = _context.Usuario.FirstOrDefault(x => x.cedula == id);

            // return user without password
            if (user != null)
                user.password = null;

            return user;
        }

        public Usuario UpdateData(Usuario original, Usuario updated)
        {
            original.nombre_1 = updated.nombre_1;
            original.nombre_2 = updated.nombre_2;
            original.apellido_1 = updated.apellido_1;
            original.apellido_2 = updated.apellido_2;
            original.password = updated.password;
            original.role = updated.role;
            original.cedula = updated.cedula;
            try
            {
                _context.Update(original);
                _context.SaveChanges();
            } catch
            {
                return null;
            }
            return original;
        }
    }
}