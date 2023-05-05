using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfiguracionProductoController : ControllerBase
    {
        private readonly ILogger<ConfiguracionProductoController> _logger;
        private configuracion_producto_negocio negocio = new configuracion_producto_negocio();

        public ConfiguracionProductoController(ILogger<ConfiguracionProductoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public TipoAccion Get([FromQuery] int? id)
        {
            return negocio.Get(id);
        }


        [HttpGet]
        [Route("[action]")]
        public TipoAccion GetByCategory([FromQuery] int id)
        {
            return negocio.GetByCategoria(id);
        }

        [HttpPost]
        [Route("[action]")]
        public TipoAccion Add([FromBody] cat_configuracion_producto_complex model)
        {
            return negocio.Add(model);
        }

        [HttpPut]
        [Route("[action]")]
        public TipoAccion Update([FromBody] cat_configuracion_producto_complex model)
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
