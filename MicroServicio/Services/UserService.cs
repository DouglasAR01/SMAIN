﻿using System;
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
using MicroServicio.Validators;

namespace MicroServicio.Services
{
    public interface IUserService { 
        Usuario Authenticate(string cedula, string password);
        IEnumerable<Usuario> GetAll();
        Usuario GetById(string id);
        Usuario UpdateData(Usuario usuario, UsuarioValidator atributosUsuario);
        bool DeteleUser(Usuario usuario);
        Usuario CreateUser(UsuarioValidator nuevoUsuario);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IOptions<HashingOptions> _hashingOptions;
        private readonly AppDbContext _context;

        public UserService(IOptions<AppSettings> appSettings, AppDbContext context, IOptions<HashingOptions> hashingOptions)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _hashingOptions = hashingOptions;
        }

        public Usuario Authenticate(string cedula, string password)
        {
            IPasswordHasher passwordHasher = new PasswordHasher(_hashingOptions);
            var user = _context.Usuario.Where( x=> x.cedula == cedula).FirstOrDefault();
            // return null if user not found
            if (user == null)
                return null;

            var (check, _) = passwordHasher.Check(user.password, password);
            if (!check)
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

        public Usuario UpdateData(Usuario original, UsuarioValidator updated)
        {
            IPasswordHasher passwordHasher = new PasswordHasher(_hashingOptions);
            string hashedPassword = passwordHasher.Hash(updated.password);
            original.nombre_1 = updated.nombre_1;
            original.nombre_2 = updated.nombre_2;
            original.apellido_1 = updated.apellido_1;
            original.apellido_2 = updated.apellido_2;
            original.password = hashedPassword;
            original.role = updated.role;
            original.cedula = updated.cedula;
            try
            {
                _context.Update(original);
                _context.SaveChanges();
            } catch (Exception)
            {
                return null;
            }
            return original;
        }

        public bool DeteleUser(Usuario usuario)
        {
            try
            {
                _context.Remove(usuario);
                _context.SaveChanges();
            } catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Usuario CreateUser(UsuarioValidator nuevoUsuario)
        {
            IPasswordHasher passwordHasher = new PasswordHasher(_hashingOptions);
            string hashedPassword = passwordHasher.Hash(nuevoUsuario.password);
            Usuario usuario = new Usuario()
            {
                cedula = nuevoUsuario.cedula,
                nombre_1 = nuevoUsuario.nombre_1,
                nombre_2 = nuevoUsuario.nombre_2,
                apellido_1 = nuevoUsuario.apellido_1,
                apellido_2 = nuevoUsuario.apellido_2,
                role = nuevoUsuario.role,
                password = hashedPassword
            };
            try
            {
                _context.Usuario.Add(usuario);
                _context.SaveChanges();
            } catch (Exception)
            {
                return null;
            }
            return usuario;
        }
    }
}