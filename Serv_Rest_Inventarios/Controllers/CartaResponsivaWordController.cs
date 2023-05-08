using Entidades_complejas;
using Generador_Word;
using Microsoft.AspNetCore.Mvc;
using Negocio.Catalogo;
using Negocio.Responsiva;

namespace Serv_Rest_Inventarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaResponsivaWordController : ControllerBase
    {
        private responsiva_usuario_negocio negocio_usuario = new();
        private responsiva_arrendamiento_negocio negocio_arrendamiento = new();

        [HttpPost]
        [Route("[action]")]
        public FileStreamResult GenerarCartaUsuario(int id)
        {
            responsiva_complex cartaResponsiva = negocio_usuario.Get(id);
            
            GenerarCartaResponsiva neg = new();
            byte[] mybytearray = neg.CartaResponsivaEquipoComputo(cartaResponsiva)!;

            return File(
                new MemoryStream(mybytearray),
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            );
        }

        [HttpPost]
        [Route("[action]")]
        public FileStreamResult GenerarCartaArrendamiento(int id)
        {
            responsiva_complex cartaResponsiva = negocio_arrendamiento.Get(id);

            GenerarCartaResponsiva neg = new();
            byte[] mybytearray = neg.CartaResponsivaEquipoComputo(cartaResponsiva)!;

            return File(
                new MemoryStream(mybytearray),
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            );
        }
    }
}
