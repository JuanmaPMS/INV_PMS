using LDAP;
using LDAP.Models;
using Microsoft.AspNetCore.Mvc;

namespace LDAP_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        [HttpGet]
        [Route("[action]")]
        public Resultado ListadoUsuarios()
        {
            Listar listadoDeUsuarios = new();

            return listadoDeUsuarios.ListarUsuarios();
        }
    }
}
