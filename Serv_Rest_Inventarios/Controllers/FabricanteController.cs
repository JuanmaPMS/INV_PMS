using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FabricanteController : ControllerBase
    {
        private readonly ILogger<FabricanteController> _logger;
        private fabricante_negocio negocio = new fabricante_negocio();

        public FabricanteController(ILogger<FabricanteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public TipoAccion Get([FromQuery] int? id)
        {
            return negocio.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public TipoAccion Add([FromBody] cat_fabricante_complex model)
        {
            return negocio.Add(model);
        }

        [HttpPut]
        [Route("[action]")]
        public TipoAccion Update([FromBody] cat_fabricante_complex model)
        {
            return negocio.Update(model);
        }

        [HttpPut]
        [Route("[action]")]
        public TipoAccion Disable([FromQuery] int id)
        {
            return negocio.Disable(id);
        }
    }
}
