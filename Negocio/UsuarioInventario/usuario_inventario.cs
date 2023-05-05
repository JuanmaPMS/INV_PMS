using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class usuario_inventario_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion? Respuesta { get; set; }
        public rel_usuario_inventario_complex? asignacion { get; set; }

        public usuario_inventario_negocio(rel_usuario_inventario_complex input, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    this.asignacion = input;
                    RelUsuarioInventario addAsignacion = new RelUsuarioInventario();
                    addAsignacion.CatUsuarioId = this.asignacion.CatUsuarioId;
                    addAsignacion.TblInventarioId = this.asignacion.TblInventarioId;
                    addAsignacion.Responsiva = this.asignacion.Responsiva;
                    addAsignacion.Estatus = true;
                    addAsignacion.Inclusion = DateTime.Now;

                    ctx.RelUsuarioInventarios.Add(addAsignacion);
                    ctx.SaveChanges();

                    if(this.asignacion.Configuracion != null && this.asignacion.Configuracion.Count > 0)
                    {
                        foreach(rel_usuario_inventario_configuracion_complex config in this.asignacion.Configuracion)
                        {
                            RelUsuarioInventarioConfiguracion relConfiguracion = new RelUsuarioInventarioConfiguracion();

                            relConfiguracion.RelUsuarioInventarioId = addAsignacion.Id;
                            relConfiguracion.CatConfiguracionProductoId = config.CatConfiguracionProductoId;
                            relConfiguracion.Valor = config.Valor;
                            relConfiguracion.Estatus = true;
                            relConfiguracion.Inclusion = DateTime.Now;

                            ctx.RelUsuarioInventarioConfiguracions.Add(relConfiguracion);
                            ctx.SaveChanges();
                        }
                    }

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa", addAsignacion.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }
        public usuario_inventario_negocio(rel_usuario_inventario_complex input, ActionUpdate update)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    this.asignacion = input;
                    //Valida que exista el registro
                    RelUsuarioInventario relAsignacion = ctx.RelUsuarioInventarios.Where(x => x.Id == this.asignacion.Id).FirstOrDefault();

                    if (relAsignacion == null)
                    { throw new Exception("No existe el registro en RelUsuarioInventarios, favor de validar."); }
                    else
                    {
                        relAsignacion.CatUsuarioId = this.asignacion.CatUsuarioId;
                        relAsignacion.TblInventarioId = this.asignacion.TblInventarioId;
                        relAsignacion.Responsiva = this.asignacion.Responsiva;

                        ctx.RelUsuarioInventarios.Update(relAsignacion);
                        ctx.SaveChanges();

                        if (this.asignacion.Configuracion != null && this.asignacion.Configuracion.Count > 0)
                        {
                            foreach (rel_usuario_inventario_configuracion_complex config in this.asignacion.Configuracion)
                            {
                                RelUsuarioInventarioConfiguracion relConfiguracion = ctx.RelUsuarioInventarioConfiguracions.Where(x => x.Id == config.Id).FirstOrDefault();

                                if (relConfiguracion == null)
                                {
                                    RelUsuarioInventarioConfiguracion relInventarioConfiguracion = new RelUsuarioInventarioConfiguracion();
                                    relInventarioConfiguracion.RelUsuarioInventarioId = relAsignacion.Id;
                                    relInventarioConfiguracion.CatConfiguracionProductoId = config.CatConfiguracionProductoId;
                                    relInventarioConfiguracion.Valor = config.Valor;
                                    relInventarioConfiguracion.Estatus = true;
                                    relInventarioConfiguracion.Inclusion = DateTime.Now;

                                    ctx.RelUsuarioInventarioConfiguracions.Add(relInventarioConfiguracion);
                                    ctx.SaveChanges();
                                }
                                else
                                {
                                    relConfiguracion.CatConfiguracionProductoId = config.CatConfiguracionProductoId;
                                    relConfiguracion.Valor = config.Valor;

                                    ctx.RelUsuarioInventarioConfiguracions.Update(relConfiguracion);
                                    ctx.SaveChanges();
                                }

                            }
                        }

                        tran.Commit();
                        this.Respuesta = TipoAccion.Positiva("Se actualizo registro exitosamente.", relAsignacion.Id);
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }

        public usuario_inventario_negocio(int id, ActionDisable disable)
        {
            try
            {
                //Valida que exista el registro
                RelUsuarioInventario relAsignacion = ctx.RelUsuarioInventarios.Where(x => x.Id == id).FirstOrDefault();

                if (relAsignacion == null)
                { throw new Exception("No existe el registro en RelUsuarioInventarios, favor de validar."); }
                else
                {
                    //Actualiza registro
                    relAsignacion.Estatus = false;

                    ctx.RelUsuarioInventarios.Update(relAsignacion);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se inhabilito registro exitosamente.", relAsignacion.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public usuario_inventario_negocio()
        { }


    }
}
