using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Negocio.Usuario;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly usuario_negocio _negocio = new();
        public UsuarioController(ILogger<UsuarioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]")]
        public TipoAccion seleccionar([FromQuery] int? id)
        {
            return _negocio.Get(id);
        }

        [HttpPost]
        [Route("agregar")]
        public TipoAccion agregar(cat_usuario_complex input)
        {
            usuario_negocio neg = new usuario_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editar")]
        public TipoAccion editar(cat_usuario_complex input)
        {
            usuario_negocio neg = new usuario_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar")]
        public TipoAccion eliminar(int id)
        {
            usuario_negocio neg = new usuario_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }



        #region Correos Adicionales
        [HttpGet]
        [Route("[action]")]
        public List<RelUsuarioCorreoAdicional> obtenercorreo()
        {
            usuario_correo_adicional_negocio neg = new usuario_correo_adicional_negocio();
            return neg.obtener();
        }


        [HttpPost]
        [Route("agregarcorreo")]
        public TipoAccion PostCorreo([FromBody] rel_usuario_correo_adicional_complex relUsuarioCorreoAdicional)
        {
            usuario_correo_adicional_negocio neg = new usuario_correo_adicional_negocio(relUsuarioCorreoAdicional, new ActionAdd());
            return neg.Respuesta;
        }


        [HttpPut]
        [Route("actualizarcorreo")]
        public TipoAccion updateCorreo([FromBody] rel_usuario_correo_adicional_complex relUsuarioCorreoAdicional)
        {
            usuario_correo_adicional_negocio neg = new usuario_correo_adicional_negocio(relUsuarioCorreoAdicional, new ActionUpdate());
            return neg.Respuesta;
        }


        [HttpDelete]
        [Route("eliminarcorreo")]
        public TipoAccion deleteCorreo([FromBody] int id)
        {
            usuario_correo_adicional_negocio neg = new usuario_correo_adicional_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }

        #endregion


    }
}
