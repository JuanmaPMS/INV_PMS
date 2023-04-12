using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaProductoController : ControllerBase
    {
        private readonly ILogger<CategoriaProductoController> _logger;
        private categoria_producto_negocio negocio = new categoria_producto_negocio();

        public CategoriaProductoController(ILogger<CategoriaProductoController> logger)
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
        public TipoAccion Add([FromBody] cat_categoria_producto_complex model)
        {
            return negocio.Add(model);
        }

        [HttpPut]
        [Route("[action]")]
        public TipoAccion Update([FromBody] cat_categoria_producto_complex model)
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
