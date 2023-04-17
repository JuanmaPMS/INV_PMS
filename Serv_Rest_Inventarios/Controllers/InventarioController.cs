using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Negocio.Inventario;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly ILogger<InventarioController> _logger;
        public InventarioController(ILogger<InventarioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("seleccionar/todos")]
        public List<VwInventario> seleccionar()
        {
            inventario_negocio neg = new inventario_negocio();
            return neg.todos();
        }

        [HttpGet]
        [Route("seleccionar/{id}")]
        public List<VwInventario> identificador(int id)
        {
            inventario_negocio neg = new inventario_negocio();
            return neg.identificador(id);
        }

        [HttpPost]
        [Route("agregar")]
        public TipoAccion agregar(tbl_inventario_complex input)
        {
            inventario_negocio neg = new inventario_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPost]
        [Route("agregar/accesorio")]
        public TipoAccion agregaraccesorio(List<tbl_inventario_accesorio_complex> input)
        {
            inventario_accesorio_negocio neg = new inventario_accesorio_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editar")]
        public TipoAccion editar(List<tbl_inventario_complex> input)
        {
            inventario_negocio neg = new inventario_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editar/accesorio")]
        public TipoAccion editaraccesorio(List<tbl_inventario_accesorio_complex> input)
        {
            inventario_accesorio_negocio neg = new inventario_accesorio_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar")]
        public TipoAccion eliminar(int id)
        {
            inventario_negocio neg = new inventario_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar/accesorio")]
        public TipoAccion eliminaraccesorio(int id)
        {
            inventario_accesorio_negocio neg = new inventario_accesorio_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }

    }
}
