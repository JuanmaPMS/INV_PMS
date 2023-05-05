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

    }
}
