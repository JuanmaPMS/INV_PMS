using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class usuario_inventario_configuracion_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion? Respuesta { get; set; }
        public rel_usuario_inventario_configuracion_complex? configuracion { get; set; }

        public usuario_inventario_configuracion_negocio(rel_usuario_inventario_configuracion_complex input, ActionAdd add)
        {
            try
            {
                this.configuracion = input;
                RelUsuarioInventarioConfiguracion addConfiguracion = new RelUsuarioInventarioConfiguracion();
                addConfiguracion.RelUsuarioInventarioId = this.configuracion.RelUsuarioInventarioId;
                addConfiguracion.CatConfiguracionProductoId = this.configuracion.CatConfiguracionProductoId;
                addConfiguracion.Valor = this.configuracion.Valor;
                addConfiguracion.Estatus = true;
                addConfiguracion.Inclusion = DateTime.Now;

                ctx.RelUsuarioInventarioConfiguracions.Add(addConfiguracion);
                ctx.SaveChanges();

                this.Respuesta = TipoAccion.Positiva("Alta Exitosa", addConfiguracion.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }
        public usuario_inventario_configuracion_negocio(rel_usuario_inventario_configuracion_complex input, ActionUpdate update)
        {
            try
            {
                this.configuracion = input;
                //Valida que exista el registro
                RelUsuarioInventarioConfiguracion relConfiguracion = ctx.RelUsuarioInventarioConfiguracions.Where(x => x.Id == this.configuracion.Id).FirstOrDefault();

                if (relConfiguracion == null)
                { throw new Exception("No existe el registro en RelUsuarioInventarioConfiguracion, favor de validar."); }
                else
                {
                    relConfiguracion.CatConfiguracionProductoId = this.configuracion.CatConfiguracionProductoId;
                    relConfiguracion.Valor = this.configuracion.Valor;

                    ctx.RelUsuarioInventarioConfiguracions.Update(relConfiguracion);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se actualizo registro exitosamente.", relConfiguracion.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public usuario_inventario_configuracion_negocio(int id, ActionDisable disable)
        {
            try
            {
                //Valida que exista el registro
                RelUsuarioInventarioConfiguracion relConfiguracion = ctx.RelUsuarioInventarioConfiguracions.Where(x => x.Id == id).FirstOrDefault();

                if (relConfiguracion == null)
                { throw new Exception("No existe el registro en RelUsuarioInventarioConfiguracion, favor de validar."); }
                else
                {
                    //Actualiza registro
                    relConfiguracion.Estatus = false;

                    ctx.RelUsuarioInventarioConfiguracions.Update(relConfiguracion);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se inhabilito registro exitosamente.", relConfiguracion.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public usuario_inventario_configuracion_negocio()
        { }



        public List<VwInventarioProductosDisponible> seleccionarInventarioProductosDisponibles()
        {
            {
                return ctx.VwInventarioProductosDisponibles.Where(x => x.CatEstatusinventarioId == 1).ToList();
            }
        }



        public List<VwUsuarioInventario> seleccionarAsignacionTodos()
        {
            {
                return ctx.VwUsuarioInventarios.Where(x => x.Estatus == true).ToList();
            }
        }

        public usuario_inventario_complex seleccionarAsignacion(int idrelusuarioinventario)
        {
            {

                var asignacion = ctx.VwUsuarioInventarios.Where(x => x.Idrelusuarioinventario == idrelusuarioinventario).FirstOrDefault();
                usuario_inventario_complex objeto = new usuario_inventario_complex();

                objeto.Idrelusuarioinventario = asignacion.Idrelusuarioinventario;
                objeto.Responsiva = asignacion.Responsiva;
                
                objeto.Nombreusuario = asignacion.Nombreusuario;
                objeto.Estatus = asignacion.Estatus;
                objeto.Idinventario = asignacion.Idinventario;
                objeto.Idadquisicion = asignacion.Idadquisicion;
                objeto.Idproducto = asignacion.Idproducto;
                objeto.Idfabricante = asignacion.Idfabricante;
                objeto.Fabricante = asignacion.Fabricante;
                objeto.Modelo = asignacion.Modelo;
                objeto.Idcategoria = asignacion.Idcategoria;
                objeto.Categoria = asignacion.Categoria;
                objeto.Esestatico = asignacion.Esestatico;
                objeto.Anio = asignacion.Anio;
                objeto.Nuevo = asignacion.Nuevo;
                objeto.Vidautil = asignacion.Vidautil;
                objeto.Caracteristicas = asignacion.Caracteristicas;
                objeto.Numerodeserie = asignacion.Numerodeserie;
                objeto.Inventarioclv = asignacion.Inventarioclv;
                objeto.Notainventario = asignacion.Notainventario;
                objeto.CatEstatusinventarioId = asignacion.CatEstatusinventarioId;
                objeto.CatEstatusinventario = asignacion.CatEstatusinventario;             
                objeto.Accesorios = asignacion.Accesorios;



                if (asignacion != null)
                {
                    objeto.Archivos = ctx.RelArchivosUsuarioInventarios.Where(x => x.RelUsuarioInventarioId == idrelusuarioinventario && x.Estatus == true).ToList();
                    objeto.Configuracion = ctx.RelUsuarioInventarioConfiguracions.Where(x => x.RelUsuarioInventarioId == idrelusuarioinventario && x.Estatus == true).ToList();

                }
                else
                {
                    objeto = new usuario_inventario_complex();
                }

                return objeto;
                
            }
        }
    }
}
