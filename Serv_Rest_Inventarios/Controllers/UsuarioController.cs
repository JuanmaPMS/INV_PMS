using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Negocio.Mantenimiento;
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


        [HttpGet]
        [Route("[action]")]
        public List<RelUsuarioCorreoAdicional> obtenercorreo()
        {
            usuario_correo_adicional_negocio neg = new usuario_correo_adicional_negocio();
            return neg.obtener();
        }


        [HttpPost]
        [Route("agregarcorreo")]
        public TipoAccion Post([FromBody] RelUsuarioCorreoAdicional relUsuarioCorreoAdicional)
        {
            usuario_correo_adicional_negocio neg = new usuario_correo_adicional_negocio(relUsuarioCorreoAdicional, new ActionAdd());
            return neg.Respuesta;
        }


        [HttpPut]
        [Route("actualizarcorreo")]
        public TipoAccion update([FromBody] RelUsuarioCorreoAdicional relUsuarioCorreoAdicional)
        {
            usuario_correo_adicional_negocio neg = new usuario_correo_adicional_negocio(relUsuarioCorreoAdicional, new ActionUpdate());
            return neg.Respuesta;
        }


        [HttpDelete]
        [Route("eliminarcorreo")]
        public TipoAccion delete([FromBody] int id)
        {
            usuario_correo_adicional_negocio neg = new usuario_correo_adicional_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }





    }
}
