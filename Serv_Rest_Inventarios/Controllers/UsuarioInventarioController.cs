using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace Serv_Rest_Inventarios.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsuarioInventarioController : ControllerBase
    {
        private readonly ILogger<UsuarioInventarioController> _logger;
        private readonly usuario_inventario_negocio _negocio = new();
        public UsuarioInventarioController(ILogger<UsuarioInventarioController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //[Route("[action]")]
        //public TipoAccion seleccionar([FromQuery] int? id)
        //{
        //    return _negocio.Get(id);
        //}

        [HttpPost]
        [Route("agregar")]
        public TipoAccion agregar(rel_usuario_inventario_complex input)
        {
            usuario_inventario_negocio neg = new usuario_inventario_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPost]
        [Route("agregar/configuracion")]
        public TipoAccion agregarconfiguracion(rel_usuario_inventario_configuracion_complex input)
        {
            usuario_inventario_configuracion_negocio neg = new usuario_inventario_configuracion_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editar")]
        public TipoAccion editar(rel_usuario_inventario_complex input)
        {
            usuario_inventario_negocio neg = new usuario_inventario_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editar/configuracion")]
        public TipoAccion editaroconfiguracion(rel_usuario_inventario_configuracion_complex input)
        {
            usuario_inventario_configuracion_negocio neg = new usuario_inventario_configuracion_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar")]
        public TipoAccion eliminar(int id)
        {
            usuario_inventario_negocio neg = new usuario_inventario_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar/configuracion")]
        public TipoAccion eliminarconfiguracion(int id)
        {
            usuario_inventario_configuracion_negocio neg = new usuario_inventario_configuracion_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }
    }
}
