using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdquisicionController : ControllerBase
    {
        #region estatico
        private readonly ILogger<ProductoController> _logger;

        public AdquisicionController(ILogger<ProductoController> logger)
        {
            _logger = logger;
        }
        #endregion

        [HttpGet]
        [Route("seleccionar")]
        public TipoAccion select(int? id)
        {
            Adquisicion_negocio neg = new Adquisicion_negocio();
            return neg.select(id);
        }

        [HttpGet]
        [Route("seleccionar/todos")]
        public List<VwAdquisicion> seleccionar()
        {
            Adquisicion_negocio neg = new Adquisicion_negocio();
            return neg.todos();
        }

        [HttpPost]
        [Route("agregar")]
        public TipoAccion agregar(tbl_adquisicion_complex input)
        {
            Adquisicion_negocio neg = new Adquisicion_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPost]
        [Route("agregar/detalle")]
        public TipoAccion agregar(rel_adquisicion_detalle_complex input)
        {
            Adquisicion_Detalle_negocio neg = new Adquisicion_Detalle_negocio(input, new ActionAdd());
            if (neg.estatus)
            {
                return TipoAccion.Positiva("Alta Exitosa");
            }
            else
            {
                return TipoAccion.Negativa("No fue posible agregar elementos");
            }

        }

        [HttpPut]
        [Route("editar")]
        public TipoAccion editar(tbl_adquisicion_complex input)
        {
            Adquisicion_negocio neg = new Adquisicion_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editar/detalle")]
        public TipoAccion editardetalle(rel_adquisicion_detalle_complex input)
        {
            Adquisicion_Detalle_negocio neg = new Adquisicion_Detalle_negocio(input, new ActionUpdate());
            if (neg.estatus)
            {
                return TipoAccion.Positiva("Actualización Exitosa");
            }
            else
            {
                return TipoAccion.Negativa("No fue posible actualizar el elemento");
            }
        }

        [HttpDelete]
        [Route("eliminar")]
        public TipoAccion eliminar(int id)
        {
            Adquisicion_negocio neg = new Adquisicion_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar/detalle")]
        public TipoAccion eliminardetalle(int id)
        {
            Adquisicion_Detalle_negocio neg = new Adquisicion_Detalle_negocio(id, new ActionDisable());
            if (neg.estatus)
            {
                return TipoAccion.Positiva("Baja Exitosa");
            }
            else
            {
                return TipoAccion.Negativa("No fue posible inhabilitar el elemento");
            }
        }

    }
}

