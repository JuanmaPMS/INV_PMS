using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Negocio.Catalogo;

namespace Serv_Rest_Inventarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly ILogger<ProveedorController> _logger;
        private proveedor_negocio negocio = new();

        public ProveedorController(ILogger<ProveedorController> logger)
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
        public TipoAccion Add([FromBody] cat_proveedor_complex model)
        {
            return negocio.Add(model);
        }

        [HttpPut]
        [Route("[action]")]
        public TipoAccion Update([FromBody] cat_proveedor_complex model)
        {
            return negocio.Update(model);
        }

        [HttpDelete]
        [Route("[action]")]
        public TipoAccion Disable([FromQuery] int id)
        {
            return negocio.Disable(id);
        }
    }
}
