using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio.Inventario;
using Negocio.InventarioArrendamiento;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventarioArrendamientoUbicacionController : ControllerBase
    {
        private readonly ILogger<InventarioArrendamientoUbicacionController> _logger;
        private readonly inventario_arrendamiento_ubicacion_negocio _negocio = new();
        public InventarioArrendamientoUbicacionController(ILogger<InventarioArrendamientoUbicacionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public List<equipo_arrendamiento_complex> filtro(int id)
        {
            return _negocio.GetFiltro(id);
        }

        [HttpGet]
        [Route("[action]")]
        public VwInventarioUbicacionArrendamiento caracteristicas(int id)
        {
            return _negocio.GetDetail(id);
        }

        [HttpGet]
        [Route("[action]")]
        public List<inventario_ubicacion_arrendamiento_complex> seleccionar(int id)
        {
            return _negocio.GetByUbicacion(id);
        }

        [HttpPost]
        [Route("agregar")]
        public TipoAccion agregar(rel_inventario_ubicacion_arrendamiento_complex input)
        {
            inventario_arrendamiento_ubicacion_negocio neg = new inventario_arrendamiento_ubicacion_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar")]
        public TipoAccion eliminar(int id)
        {
            inventario_arrendamiento_ubicacion_negocio neg = new inventario_arrendamiento_ubicacion_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }
    }
}
