using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {

        #region estatitco
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(ILogger<ProductoController> logger)
        {
            _logger = logger;
        }
        #endregion

        [HttpGet]
        [Route("seleccionar/todos")]
        public List<VwCatProducto> seleccionar()
        {
            producto_negocio neg = new producto_negocio();
            return neg.todos();
        }
        [HttpGet]
        [Route("seleccionar/{id}")]
        public List<VwCatProducto> identificador(int id)
        {
            producto_negocio neg = new producto_negocio();
            return neg.identificador(id);
        }

        [HttpGet]
        [Route("seleccionarCaracteristicas/{id}")]
        public List<RelProductoCatacteristica> seleccionarCaracteristicas(int id)
        {
            producto_negocio neg = new producto_negocio();
            return neg.caracteristicas(id);
        }

        [HttpGet]
        [Route("seleccionar/autocomplete")]
        public List<String> autocomplete()
        {
            producto_negocio neg = new producto_negocio();
            return neg.autocomplete();
        }




        [HttpPost]
        [Route("agregar")]
        public TipoAccion agregar(cat_producto_complex input)
        {
            producto_negocio neg = new producto_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPost]
        [Route("agregar/caracteristica")]
        public TipoAccion agregarcaracteristica(rel_producto_caracteristicas_complex input)
        {
            producto_caracteristicas_negocio neg = new producto_caracteristicas_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editar")]
        public TipoAccion editar(cat_producto_complex input)
        {
            producto_negocio neg = new producto_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editar/caracteristica")]
        public TipoAccion editarcaracteristica(rel_producto_caracteristicas_complex input)
        {
            producto_caracteristicas_negocio neg = new producto_caracteristicas_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar")]
        public TipoAccion eliminar(int id)
        {
            producto_negocio neg = new producto_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar/caracteristica")]
        public TipoAccion eliminarcaracteristica(int id)
        {
            producto_caracteristicas_negocio neg = new producto_caracteristicas_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }
    }
}

