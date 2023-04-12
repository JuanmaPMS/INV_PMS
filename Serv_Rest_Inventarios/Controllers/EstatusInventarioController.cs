using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio.Catalogo;

namespace Serv_Rest_Inventarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstatusInventarioController : ControllerBase
    {
        private readonly ILogger<EstatusInventarioController> _logger;
        private readonly estatus_inventario _negocio = new();

        public EstatusInventarioController(ILogger<EstatusInventarioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public TipoAccion Get([FromQuery] int? id)
        {
            return _negocio.Get(id);
        }
    }
}
