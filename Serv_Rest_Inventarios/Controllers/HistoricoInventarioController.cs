using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Negocio.HistoricoInventario;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoricoInventarioController : ControllerBase
    {
        private readonly ILogger<HistoricoInventarioController> _logger;
        private historico_inventario_negocio negocio = new historico_inventario_negocio();

        public HistoricoInventarioController(ILogger<HistoricoInventarioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public TipoAccion Get([FromQuery] string numeroSerie)
        {
            
            return negocio.seleccionar(numeroSerie);
        }

       
    }
}
