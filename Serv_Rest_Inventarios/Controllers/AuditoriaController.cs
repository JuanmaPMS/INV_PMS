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
        [Route("inventario")]
        public object Get(string NumSerie)
        {
            //return  Auditoria.Get(objeto, NumSerie);
            return  Auditoria.GetInventario(NumSerie);
        }

    }
}
