using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio.Catalogo;

namespace Serv_Rest_Inventarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<CategoriaProductoController> _logger;
        private readonly cliente_negocio _negocio = new();

        public ClienteController(ILogger<CategoriaProductoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public TipoAccion Get([FromQuery] int? id)
        {
            return _negocio.Get(id);
        }

        [HttpPost]
        [Route("[action]")]
        public TipoAccion Add([FromBody] cat_cliente_complex model)
        {
            return _negocio.Add(model);
        }

        [HttpPut]
        [Route("[action]")]
        public TipoAccion Update([FromBody] cat_cliente_complex model)
        {
            return _negocio.Update(model);
        }

        [HttpDelete]
        [Route("[action]")]
        public TipoAccion Disable([FromQuery] int id)
        {
            return _negocio.Disable(id);
        }
    }
}
