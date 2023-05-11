using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UbicacionController : ControllerBase
    {
        private readonly ILogger<UbicacionController> _logger;
        private readonly ubicacion_negocio _negocio = new();
        private readonly ubicacion_oficina_negocio _negocio_oficina = new();
        public UbicacionController(ILogger<UbicacionController> logger)
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
        public bool validaasignados([FromQuery] int id)
        {
            return _negocio.ValidaAsignados(id);
        }

        [HttpGet]
        [Route("[action]")]
        public bool validaasignadosoficina([FromQuery] int id)
        {
            return _negocio_oficina.ValidaAsignados(id);
        }

        [HttpPost]
        [Route("agregar")]
        public TipoAccion agregar(tbl_ubicacion_complex input)
        {
            ubicacion_negocio neg = new ubicacion_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPost]
        [Route("agregar/oficina")]
        public TipoAccion agregaroficina(rel_ubicacion_oficina_complex input)
        {
            ubicacion_oficina_negocio neg = new ubicacion_oficina_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editar")]
        public TipoAccion editar(tbl_ubicacion_complex input)
        {
            ubicacion_negocio neg = new ubicacion_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editar/oficina")]
        public TipoAccion editaroficina(rel_ubicacion_oficina_complex input)
        {
            ubicacion_oficina_negocio neg = new ubicacion_oficina_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar")]
        public TipoAccion eliminar(int id)
        {
            ubicacion_negocio neg = new ubicacion_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar/oficina")]
        public TipoAccion eliminaroficina(int id)
        {
            ubicacion_oficina_negocio neg = new ubicacion_oficina_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }
    }
}
