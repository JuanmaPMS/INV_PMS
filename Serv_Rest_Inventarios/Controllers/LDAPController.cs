using Entidades_complejas;
using LDAP;
using Microsoft.AspNetCore.Mvc;

namespace Serv_Rest_Inventarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LDAPController : ControllerBase
    {
        private readonly LDAP_Connection _connection = new();

        [HttpGet]
        [Route("[action]")]
        public TipoAccion ListadoUsuarioPorCliente(int id, string name)
        {
            return _connection.GetById(id, name);
        }

        [HttpGet]
        [Route("[action]")]
        public TipoAccion ListadoUsuariosPM(string name)
        {
            return _connection.GetUsuariosPM(name);
        }
    }
}
