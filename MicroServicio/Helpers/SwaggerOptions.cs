using Microsoft.AspNetCore.Mvc;

namespace MicroServicio.Helpers
{
    public class SwaggerOptions : ControllerBase
    {
        public string JsonRoute { get; set; }
        public string Description { get; set; }
        public string UiEndPoint { get; set; }
    }
}