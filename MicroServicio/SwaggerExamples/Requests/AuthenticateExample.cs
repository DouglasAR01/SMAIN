using MicroServicio.Validators;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicio.SwaggerExample.Request
{
    public class AuthenticateExample:IExamplesProvider<LoginValidator>
    {
        public LoginValidator GetExamples()
        {
            return new LoginValidator
            {
                cedula = "101",
                password = "prueba"
            };
        }
    }
}
