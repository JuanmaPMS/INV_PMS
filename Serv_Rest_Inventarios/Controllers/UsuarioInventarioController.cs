﻿using Data.Models;
using Entidades_complejas;
using Microsoft.AspNetCore.Mvc;
using Negocio;
using Negocio.UsuarioInventario;

namespace Serv_Rest_Inventarios.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UsuarioInventarioController : ControllerBase
    {
        private readonly ILogger<UsuarioInventarioController> _logger;
        private readonly usuario_inventario_negocio _negocio = new();
        private readonly usuario_inventario_archivos_negocio _negocioArchivos = new();
        public UsuarioInventarioController(ILogger<UsuarioInventarioController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //[Route("[action]")]
        //public TipoAccion seleccionar([FromQuery] int? id)
        //{
        //    return _negocio.Get(id);
        //}

        [HttpGet]
        [Route("[action]")]
        public TipoAccion seleccionararchivos([FromQuery] int id)
        {
            return _negocioArchivos.Get(id);
        }

        [HttpPost]
        [Route("agregar")]
        public TipoAccion agregar(rel_usuario_inventario_complex input)
        {
            usuario_inventario_negocio neg = new usuario_inventario_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPost]
        [Route("agregar/configuracion")]
        public TipoAccion agregarconfiguracion(rel_usuario_inventario_configuracion_complex input)
        {
            usuario_inventario_configuracion_negocio neg = new usuario_inventario_configuracion_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPost]
        [Route("agregar/archivos")]
        public TipoAccion agregararchivos(List<rel_usuario_inventario_archivo_complex> input)
        {
            usuario_inventario_archivos_negocio neg = new usuario_inventario_archivos_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editar")]
        public TipoAccion editar(rel_usuario_inventario_complex input)
        {
            usuario_inventario_negocio neg = new usuario_inventario_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editarResponsivaAsignacion")]
        public TipoAccion editarResponsivaAsignacion(rel_usuario_inventario_complex input)
        {
            usuario_inventario_negocio neg = new usuario_inventario_negocio(input, new ActionUpdateResponsiva());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editar/configuracion")]
        public TipoAccion editaroconfiguracion(rel_usuario_inventario_configuracion_complex input)
        {
            usuario_inventario_configuracion_negocio neg = new usuario_inventario_configuracion_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar")]
        public TipoAccion eliminar(int id, int idUsuario)
        {
            usuario_inventario_negocio neg = new usuario_inventario_negocio(id, idUsuario, new ActionDisable());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar/configuracion")]
        public TipoAccion eliminarconfiguracion(int id)
        {
            usuario_inventario_configuracion_negocio neg = new usuario_inventario_configuracion_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }

        [HttpDelete]
        [Route("eliminar/archivo")]
        public TipoAccion eliminararchivo(int id)
        {
            usuario_inventario_archivos_negocio neg = new usuario_inventario_archivos_negocio(id, new ActionDisable());
            return neg.Respuesta;
        }


        [HttpGet]
        [Route("seleccionarInventarioProductosDisponibles")]
        public List<VwInventarioProductosDisponible> seleccionarInventarioProductosDisponibles()
        {
            usuario_inventario_configuracion_negocio neg = new usuario_inventario_configuracion_negocio();
            return neg.seleccionarInventarioProductosDisponibles();
        }



        [HttpGet]
        [Route("seleccionarAsignacionTodos")]
        public List<VwUsuarioInventario> seleccionarAsignacionTodos()
        {
            usuario_inventario_configuracion_negocio neg = new usuario_inventario_configuracion_negocio();
            return neg.seleccionarAsignacionTodos();
        }

        [HttpGet]
        [Route("seleccionarAsignacion")]
        public usuario_inventario_complex seleccionarAsignacion(int idrelusuarioinventario)
        {
            usuario_inventario_configuracion_negocio neg = new usuario_inventario_configuracion_negocio();
            return neg.seleccionarAsignacion(idrelusuarioinventario);
        }




        [HttpGet]
        [Route("seleccionarContenedores")]
        public TipoAccion seleccionarContenedores(int idrelusuarioinventario)
        {
            usuario_inventario_contenedor_negocio neg = new usuario_inventario_contenedor_negocio();
            return neg.Get(idrelusuarioinventario);
        }

        [HttpGet]
        [Route("seleccionarContenedor")]
        public TipoAccion seleccionarContenedor(int id)
        {
            usuario_inventario_contenedor_negocio neg = new usuario_inventario_contenedor_negocio();
            return neg.GetById(id);
        }


        [HttpPost]
        [Route("agregarContenedor")]
        public TipoAccion agregarContenedores(tbl_usuario_inventario_contenedor_complex input)
        {
            usuario_inventario_contenedor_negocio neg = new usuario_inventario_contenedor_negocio(input, new ActionAdd());
            return neg.Respuesta;
        }

        [HttpPut]
        [Route("editarContenedor")]
        public TipoAccion editarContenedores(tbl_usuario_inventario_contenedor_complex input)
        {
            usuario_inventario_contenedor_negocio neg = new usuario_inventario_contenedor_negocio(input, new ActionUpdate());
            return neg.Respuesta;
        }



        [HttpDelete]
        [Route("eliminarContenedor")]
        public TipoAccion eliminarContenedores(int id, int idUsuario)
        {
            usuario_inventario_contenedor_negocio neg = new usuario_inventario_contenedor_negocio(id, idUsuario, new ActionDisable());
            return neg.Respuesta;
        }


    }
}
