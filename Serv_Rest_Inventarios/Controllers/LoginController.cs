using Azure;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        public autenticacion_negocio negocio = new();

        private readonly ILogger<LoginController> _logger;
        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("Login")]
        public TipoAccion Login([FromBody] usuario_login_complex login)
        {
            return negocio.Get(login);
        }

        [HttpGet]
        [Route("ResetPassword/{id}")]
        public TipoAccion Reset(string id)
        {
            return negocio.GetResset(id);
        }

        [HttpPost]
        [Route("ResetPassword")]
        public TipoAccion Reset([FromBody] usuario_login_complex login)
        {
            return negocio.Reset(login);
        }

        [HttpPut]
        [Route("UpdatePassword")]
        public TipoAccion Update([FromBody] usuario_app_complex usuario)
        {
            return negocio.UpdatePassword(usuario);
        }
    }
}
