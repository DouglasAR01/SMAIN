using MicroServicio.Validators;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicio.SwaggerExamples.Requests
{
    public class EditarUsuarioExample : IExamplesProvider<UsuarioValidator>
    {
        public UsuarioValidator GetExamples()
        {
            return new UsuarioValidator
            {
                cedula = "102",
                nombre_1 = "Bobinsky",
                nombre_2 = "Alejandro",
                apellido_1 = "Ramirez",
                apellido_2 = "Toloza",
                role = "u",
                password = "prueba"
            };
        }
    }
}
