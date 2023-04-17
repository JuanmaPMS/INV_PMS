using Entidades_complejas;
using LDAP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Serv_Rest_Inventarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LDAPController : ControllerBase
    {
        private readonly LDAP_Connection _connection = new();

        //[HttpPost]
        //[Route("[action]")]
        //public Resultado Login(Auth credenciales)
        //{
        //    string domain = "Grupopm.mx";
        //    int port = 389;

        //    Resultado Response = new LdapManager(domain, port).Validate(credenciales.User!, credenciales.Password!);

        //    if (Response.Exito == false)
        //    {
        //        Response = new()
        //        {
        //            Exito = false,
        //            Mensaje = "No se encontro usuario con esas credenciales.",
        //            Objeto = null
        //        };
        //    }

        //    return Response;
        //}

        //[HttpGet]
        //[Route("[action]")]
        //public Resultado ListadoUsuarios()
        //{
        //    Listar listadoDeUsuarios = new();

        //    return listadoDeUsuarios.ListarUsuarios();
        //}

        [HttpGet]
        [Route("[action]")]
        public TipoAccion ListadoUsuarios(int? id)
        {
            return _connection.Get(id);
        }
    }
}
