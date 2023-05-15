using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio.CargaMasiva;
using ClosedXML.Excel;
using Data.Models;
using Negocio;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CargaMasivaController : Controller
    {
        private readonly ILogger<InventarioUbicacionController> _logger;
        private readonly CargaMasivaNegocio _negocio = new();
        public CargaMasivaController(ILogger<InventarioUbicacionController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("[action]")]
        public FileStreamResult ObtenerPlantilla(int num)
        {
            byte[] bytes = _negocio.obtenerPlantilla(num);
            return File(new MemoryStream(bytes), "application/vnd.ms-excel");
            
        }

        [HttpPost]
        [Route("[action]")]
        public TipoAccion CargarPlantilla(IFormFile file)
        {
            TipoAccion result = new TipoAccion();
            try
            {
                var workbook = new XLWorkbook(file.OpenReadStream());
                var worksheet_p = workbook.Worksheet(1);
                var rows_p = worksheet_p.RangeUsed().RowsUsed().Skip(1); //Skip header row

                if (rows_p.Count() <= 0)
                { throw new Exception("No se encontró información en el Excel para generar adquisición."); }

                adquisicion_masiva_complex adquisicion = new adquisicion_masiva_complex();
                foreach (var row in rows_p)
                {
                    adquisicion.Proveedor = row.Cell(1).Value.ToString();
                    adquisicion.Propietario = row.Cell(2).Value.ToString();
                    adquisicion.Articulos = row.Cell(3).Value.ToString();
                    adquisicion.Monto = row.Cell(4).Value.ToString();
                    adquisicion.Impuesto = row.Cell(5).Value.ToString();
                    adquisicion.Fechadecompra = Convert.ToDateTime(row.Cell(6).Value);
                }

                var worksheet_d = workbook.Worksheet(2);
                var rows_d = worksheet_d.RangeUsed().RowsUsed().Skip(1); //Skip header row

                if (rows_d.Count() > 0)
                {
                    adquisicion.RelAdquisicionDetalles = new List<adquisicion_masiva_detalle_complex>();
                    foreach (var row in rows_d)
                    {
                        adquisicion_masiva_detalle_complex detalle_Complex = new adquisicion_masiva_detalle_complex();
                        detalle_Complex.Producto = row.Cell(1).Value.ToString();
                        detalle_Complex.Costosiunitario = row.Cell(2).Value.ToString();
                        detalle_Complex.Numerodeserie = row.Cell(3).Value.ToString();
                        detalle_Complex.Inventarioclv = row.Cell(4).Value.ToString();
                        adquisicion.RelAdquisicionDetalles.Add(detalle_Complex);
                    }
                }


                //result.Exito = true;
                //result.Mensaje = "Carga correcta";
                result = _negocio.carga_masiva(adquisicion);
            }
            catch(Exception ex)
            {
                result.Exito = false;
                result.Mensaje = ex.Message;
            }
            return result;
        }
    }
}
