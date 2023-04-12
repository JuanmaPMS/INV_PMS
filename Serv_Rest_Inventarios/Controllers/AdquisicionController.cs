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

        #region estatitco
        private readonly ILogger<ProductoController> _logger;

        public AdquisicionController(ILogger<ProductoController> logger)
        {
            _logger = logger;
        }
        #endregion


        [HttpPost]
        [Route("agregar")]
        public TipoAccion agregar(tbl_adquisicion_complex input)
        {
            Adquisicion_negocio neg = new Adquisicion_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }


        [HttpPost]
        [Route("agregar/detalle")]
        public TipoAccion agregar(List<RelAdquisicionDetalle> input)
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

    }
}

