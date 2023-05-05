using Generador_Word;
using Microsoft.AspNetCore.Mvc;

namespace Serv_Rest_Inventarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartaResponsivaWordController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public FileStreamResult GenerarCartaResponsiva(CartaResponsiva cartaResponsiva)
        {
            GenerarCartaResponsiva neg = new();
            byte[] mybytearray = neg.CartaResponsivaEquipoComputo(cartaResponsiva)!;

            return File(
                new MemoryStream(mybytearray),
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            );
        }
    }
}
