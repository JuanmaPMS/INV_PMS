using Microsoft.AspNetCore.Mvc;
using Data.Models;
using Negocio.Mantenimiento;
using Entidades_complejas;

namespace Serv_Rest_Inventarios.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MantenimientoController : ControllerBase
    {
        [HttpGet]
        [Route("obtener")]
        public List<VwMantenimientoInventario> Get()
        {
            mantenimiento_negocio neg = new mantenimiento_negocio();
            return neg.todos();
        }

        [HttpPost]
        [Route("agregar")]
        public TipoAccion Post([FromBody] tbl_mantenimiento_complex tblMantenimiento)
        {
            mantenimiento_negocio neg = new mantenimiento_negocio(tblMantenimiento, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("actualizar")]
        public TipoAccion update([FromBody] tbl_mantenimiento_complex tblMantenimiento)
        {
            mantenimiento_negocio neg = new mantenimiento_negocio(tblMantenimiento, new ActionUpdate());
            return neg.Respuesta;
        }

    }
}
