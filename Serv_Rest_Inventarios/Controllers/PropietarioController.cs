using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropietarioController : ControllerBase
    {
        private readonly ILogger<PropietarioController> _logger;
        private propietario_negocio negocio = new propietario_negocio();

        public PropietarioController(ILogger<PropietarioController> logger)
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
        public TipoAccion Add([FromBody] cat_propietario_complex model)
        {
            return negocio.Add(model);
        }

        [HttpPut]
        [Route("[action]")]
        public TipoAccion Update([FromBody] cat_propietario_complex model)
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
