using MicroServicio.Validators;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicio.SwaggerExamples.Requests
{
    public class CrearUsuarioExample:IExamplesProvider<UsuarioValidator>
    {
        public UsuarioValidator GetExamples()
        {
            return new UsuarioValidator
            {
                cedula = "400",
                nombre_1 = "Javier",
                apellido_1 = "Tarazona",
                role = "u",
                password = "prueba"
            };
        }
    }
}
