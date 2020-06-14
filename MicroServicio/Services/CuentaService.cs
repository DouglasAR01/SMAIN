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
        bool IsUserOwner(ulong? cuenta, string cedula);
        bool IsNameUserOwner(ulong? cuenta, string name);
        bool IsEnoughMoney(ulong? cuenta, decimal? monto);
        void Transaccion(ulong? cuentaOrigen, ulong? cuentaDestino, decimal? monto);
    }

    public class CuentaService : ICuentaService
    {
        private readonly AppDbContext _context;

        public CuentaService(AppDbContext context){
            _context = context;
        }

        public IEnumerable<Cuenta> GetAll(){
            return _context.Cuenta.ToList();
        }

        public IEnumerable<Cuenta> GetByUser(string cedula){
            return _context.Cuenta.Where(x=>x.Usuario.cedula == cedula).ToList();
        }

        public bool IsUserOwner(ulong? cuenta, string cedula){
            Cuenta cuentaObj = _context.Cuenta.FirstOrDefault(x => x.id.Equals(cuenta) && x.Usuario.cedula == cedula);
            return cuentaObj != null;
        }

        public bool IsNameUserOwner(ulong? cuenta, string name){
            Cuenta cuentaObj = _context.Cuenta.Where( x => x.id.Equals(cuenta) && $"{x.Usuario.nombre_1} {x.Usuario.nombre_2} {x.Usuario.apellido_1} {x.Usuario.apellido_2}".ToLower().Contains(name.ToLower()) ).FirstOrDefault();
            return cuentaObj != null;
        }

        public bool IsEnoughMoney(ulong? cuenta, decimal? monto){
            Cuenta cuentaObj = _context.Cuenta.Where(x => x.id.Equals(cuenta) && x.balance >= monto ).FirstOrDefault();
            return cuentaObj != null;
        }

        public void Transaccion(ulong? cuentaOrigen, ulong? cuentaDestino, decimal? monto){
            Cuenta cuentaOrigenObj = _context.Cuenta.Find(cuentaOrigen);
            Cuenta cuentaDestinoObj = _context.Cuenta.Find(cuentaDestino);

            cuentaOrigenObj.balance -= monto;
            cuentaDestinoObj.balance += monto;

            _context.SaveChanges();
        }

        public Cuenta GetById(int id){
            var cuenta = _context.Cuenta.Find(id);
            return cuenta;
        }
    }
}