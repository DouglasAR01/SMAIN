using MicroServicio.Validators;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicio.SwaggerExamples.Requests
{
    public class EditarBalanceExample : IExamplesProvider<CuentaValidator>
    {
        public CuentaValidator GetExamples()
        {
            return new CuentaValidator
            {
                numCuenta = "3",
                nuevoBalance = "200.65"
            };
        }
    }
}
