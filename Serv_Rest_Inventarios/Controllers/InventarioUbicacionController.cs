using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Negocio.Inventario;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventarioUbicacionController : ControllerBase
    {
        private readonly ILogger<InventarioUbicacionController> _logger;
        private readonly inventario_ubicacion_negocio _negocio = new();
        public InventarioUbicacionController(ILogger<InventarioUbicacionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public List<equipo_complex> filtro()
        {
            return _negocio.GetFiltro();
        }

        [HttpGet]
        [Route("[action]")]
        public VwInventarioUbicacion caracteristicas(int id)
        {
            return _negocio.GetDetail(id);
        }

        [HttpGet]
        [Route("[action]")]
        public List<inventario_ubicacion_complex> seleccionar(int id)
        {
            return _negocio.GetByUbicacion(id);
        }

        [HttpPost]
        [Route("agregar")]
        public TipoAccion agregar(rel_inventario_ubicacion_complex input)
        {
            inventario_ubicacion_negocio neg = new inventario_ubicacion_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar")]
        public TipoAccion eliminar(int id)
        {
            inventario_ubicacion_negocio neg = new inventario_ubicacion_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }

    }
}
