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


        [HttpPost]
        [Route("agregar")]
        public TipoAccion agregar(cat_producto_complex input)
        {
            producto_negocio neg = new producto_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

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
        [Route("seleccionar/autocomplete")]
        public List<String> autocomplete()
        {
            producto_negocio neg = new producto_negocio();
            return neg.autocomplete();
        }
    }
}

