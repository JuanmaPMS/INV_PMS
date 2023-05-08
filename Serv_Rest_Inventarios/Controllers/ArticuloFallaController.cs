using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio.Catalogo;
using Negocio.Usuario;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticuloFallaController : ControllerBase
    {

        [HttpGet]
        [Route("[action]")]
        public List<CatFamiliaArticuloFalla> obtenerfalla([FromQuery] int id)
        {
            familia_articulo_falla_negocio neg = new familia_articulo_falla_negocio();
            return neg.obtener(id);
        }

        [HttpPost]
        [Route("agregar")]
        public TipoAccion add([FromBody] cat_familia_articulo_falla_complex catFamiliaArticuloFalla)
        {
            familia_articulo_falla_negocio neg = new familia_articulo_falla_negocio(catFamiliaArticuloFalla, new ActionAdd());
            return neg.Respuesta;
        }


        [HttpPut]
        [Route("actualizar")]
        public TipoAccion update([FromBody] cat_familia_articulo_falla_complex catFamiliaArticuloFalla)
        {
            familia_articulo_falla_negocio neg = new familia_articulo_falla_negocio(catFamiliaArticuloFalla, new ActionUpdate());
            return neg.Respuesta;
        }


        [HttpDelete]
        [Route("eliminar")]
        public TipoAccion delete([FromBody] int id)
        {
            familia_articulo_falla_negocio neg = new familia_articulo_falla_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }



    }
}
