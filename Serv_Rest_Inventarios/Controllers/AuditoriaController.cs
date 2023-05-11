using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Negocio.Mantenimiento;

namespace Serv_Rest_Inventarios.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
        [HttpGet]
        [Route("obtener")]
        //public List<TblInventarioHistorico> Get(string objeto, string id)
        public object Get(string objeto, string id)
        {
            return  Auditoria.Get(objeto, id);
        }

    }
}
