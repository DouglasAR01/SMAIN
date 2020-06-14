using MicroServicio.Validators;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicio.SwaggerExamples.Requests
{
    public class TransferirExample : IExamplesProvider<DatosTransferir>
    {
        public DatosTransferir GetExamples()
        {
            return new DatosTransferir
            {
                numCuentaOrigen="1",
                numCuentaDestino="3",
                nameDestino="James",
                monto="23.2"
            };
        }
    }
}
