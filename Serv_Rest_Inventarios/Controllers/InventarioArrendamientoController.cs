using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Negocio.Inventario;
using Negocio.InventarioArrendamiento;

namespace Serv_Rest_Inventarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventarioArrendamiento : ControllerBase
    {
        private readonly ILogger<InventarioArrendamiento> _logger;
        public InventarioArrendamiento(ILogger<InventarioArrendamiento> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //[Route("seleccionar/todosAgrupado")]
        //public List<VwInventarioArrendamientoAgrupado> seleccionarAgrupado()
        //{
        //    inventario_arrendamiento_negocio neg = new inventario_arrendamiento_negocio();
        //    return neg.todosAgrupado();
        //}

        //[HttpGet]
        //[Route("seleccionar/detalle")]
        //public List<VwInventarioArrendamiento> seleccionarDetalle(int idProducto, int idCliente)
        //{
        //    inventario_arrendamiento_negocio neg = new inventario_arrendamiento_negocio();
        //    return neg.detalle(idProducto, idCliente);
        //}

        [HttpGet]
        [Route("seleccionarInventarioDisponible")]
        public List<VwInventarioArrendamiento> seleccionarInventarioDisponible(int idCliente)
        {
            inventario_arrendamiento_negocio neg = new inventario_arrendamiento_negocio();
            return neg.seleccionarInventarioDisponible(idCliente);
        }


        [HttpPost]
        [Route("agregarInventarioCliente")]
        public TipoAccion agregar(tbl_inventario_arrendamiento_complex input)
        {
            inventario_arrendamiento_negocio neg = new inventario_arrendamiento_negocio();
            neg.agregar(input);
            return neg.Respuesta;
        }


        [HttpGet]
        [Route("seleccionarAsignacion")]
        public empleado_inventario_arrendamiento_complex seleccionarAsignacion(int idrelempleadoinventarioarrendamiento)
        {
            inventario_arrendamiento_negocio neg = new inventario_arrendamiento_negocio();
            return neg.seleccionarAsignacion(idrelempleadoinventarioarrendamiento);
        }

        [HttpPost]
        [Route("agregarAsignacion")]
        public TipoAccion agregarAsignacion(rel_empleado_inventario_arrendamiento_complex input)
        {
            inventario_arrendamiento_negocio neg = new inventario_arrendamiento_negocio();
            neg.agregarAsignacion(input);
            return neg.Respuesta;
        }

        [HttpPost]
        [Route("agregarArchivosAsignacion")]
        public TipoAccion agregarArchivosAsignacion(List<RelArchivosEmpleadoInventarioArrendamiento> input)
        {
            inventario_arrendamiento_negocio neg = new inventario_arrendamiento_negocio();
            neg.agregarArchivosAsignacion(input);
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editarAsignacion")]
        public TipoAccion editarAsignacion(rel_empleado_inventario_arrendamiento_complex input)
        {
            inventario_arrendamiento_negocio neg = new inventario_arrendamiento_negocio();
            neg.editarAsignacion(input);
            return neg.Respuesta;
        }


        [HttpDelete]
        [Route("eliminarAsignacion")]
        public TipoAccion eliminar(int id)
        {
            inventario_negocio neg = new inventario_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }

        //[HttpDelete]
        //[Route("eliminar/accesorio")]
        //public TipoAccion eliminaraccesorio(int id)
        //{
        //    inventario_accesorio_negocio neg = new inventario_accesorio_negocio(id, new ActionDisable());
        //    return neg.Respuesta;
        //}

    }
}
