﻿using Microsoft.AspNetCore.Mvc;
using Negocio.CargaMasiva;

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
        public IActionResult ObtenerPlantilla(int num)
        {
            byte[] bytes = _negocio.obtenerPlantilla(num);
            return new FileStreamResult(new MemoryStream(bytes), "application/vnd.ms-excel") 
            {
                FileDownloadName = "PlantillaCargaMasiva"
            };
            
        }
    }
}
