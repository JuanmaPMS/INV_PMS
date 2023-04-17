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
        public TipoAccion ListadoUsuarioPorCliente(int id)
        {
            return _connection.GetById(id);
        }
    }
}
