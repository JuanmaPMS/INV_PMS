using LDAP.Models;
using LDAP;
using Microsoft.AspNetCore.Mvc;

namespace LDAP_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public Resultado Login(Auth credenciales)
        {
            string domain = "Grupopm.mx";
            int port = 389;

            Resultado Response = new LdapManager(domain, port).Validate(credenciales.User!, credenciales.Password!);

            if (Response.Exito == false)
            {
                Response = new()
                {
                    Exito = false,
                    Mensaje = "No se encontro usuario con esas credenciales.",
                    Objeto = null
                };
            }

            return Response;
        }
    }
}
