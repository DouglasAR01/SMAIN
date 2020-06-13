using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroServicio.Contexts;
using MicroServicio.Entities;
using MicroServicio.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MicroServicio.Services
{
    public interface ICuentaService
    {
        IEnumerable<Cuenta> GetAll();
        IEnumerable<Cuenta> GetByUser(string cedula);
        Cuenta GetById(int id);
    }

    public class CuentaService : ICuentaService
    {
        private readonly AppDbContext _context;

        public CuentaService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Cuenta> GetAll()
        {
            return _context.Cuenta.ToList();
        }

        public IEnumerable<Cuenta> GetByUser(string cedula)
        {
            return _context.Cuenta.Where(x=>x.cedula == cedula).ToList();
        }

        public Cuenta GetById(int id)
        {
            var cuenta = _context.Cuenta.Find(id);
            return cuenta;
        }
    }
}