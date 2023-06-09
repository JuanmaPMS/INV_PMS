﻿using Data.Models;
using Entidades_complejas;
using Negocio.HistoricoInventario;
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

                    if(input.CatUsuarioId <= 0)
                    {
                        //Valida si existe el usuario en el catalogo
                        input.Cuenta = input.Cuenta == null ? "" : input.Cuenta.ToLower();
                        CatUsuario catUsuario = ctx.CatUsuarios.Where(x => x.Cuenta == input.Cuenta).FirstOrDefault();
                        if (catUsuario == null)
                        {
                            CatUsuario usuario = new CatUsuario();
                            usuario.Nombre = input.Nombre;
                            usuario.Cuenta = input.Cuenta;
                            usuario.Correo = input.Correo;
                            usuario.Ubicacion = "";
                            usuario.Estatus = true;
                            usuario.Inclusion = DateTime.Now;

                            ctx.CatUsuarios.Add(usuario);
                            ctx.SaveChanges();

                            input.CatUsuarioId = usuario.Id;
                        }
                        else
                        {
                            input.CatUsuarioId = catUsuario.Id;
                        }
                    }         

                    //Actualiza el estatus del inventario
                    TblInventario tblInventario = ctx.TblInventarios.Where(x => x.Id == this.asignacion.TblInventarioId).FirstOrDefault()!;
                    if (tblInventario == null)
                    { throw new Exception("No existe el registro en Tbl_Inventario, favor de validar."); }

                    tblInventario.CatEstatusinventarioId = 3; //ASIGNADO A USUARIO

                    ctx.TblInventarios.Update(tblInventario);
                    ctx.SaveChanges();

                    RelUsuarioInventario addAsignacion = new RelUsuarioInventario();
                    addAsignacion.CatUsuarioId = this.asignacion.CatUsuarioId;
                    addAsignacion.TblInventarioId = this.asignacion.TblInventarioId;
                    addAsignacion.Responsiva = this.asignacion.Responsiva;
                    addAsignacion.Estatus = true;
                    addAsignacion.Inclusion = DateTime.Now;

                    ctx.RelUsuarioInventarios.Add(addAsignacion);
                    ctx.SaveChanges();

                    var inventario = ctx.VwInventarios.Where(x => x.Idinventario == tblInventario.Id).FirstOrDefault();
                    CatUsuario catUsuario_ = ctx.CatUsuarios.Where(x => x.Id == this.asignacion.CatUsuarioId).FirstOrDefault();
                    historico_inventario_negocio.AsignacionInventario(input.UsuarioAppid, inventario, catUsuario_);
                    


                    if (this.asignacion.Configuracion != null && this.asignacion.Configuracion.Count > 0)
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
                        //Valida si se modifica el Inventario, para liberar el inventario
                        if(this.asignacion.TblInventarioId != relAsignacion.TblInventarioId)
                        {
                            //Inventario nuevo
                            TblInventario tblInventario = ctx.TblInventarios.Where(x => x.Id == this.asignacion.TblInventarioId).FirstOrDefault()!;
                            if (tblInventario == null)
                            { throw new Exception("No existe el registro en Tbl_Inventario, favor de validar."); }

                            tblInventario.CatEstatusinventarioId = 3; //ASIGNADO A USUARIO

                            ctx.TblInventarios.Update(tblInventario);
                            ctx.SaveChanges();

                            //Inventario Anterior
                            TblInventario tblInventarioA = ctx.TblInventarios.Where(x => x.Id == relAsignacion.TblInventarioId).FirstOrDefault()!;
                            if (tblInventarioA == null)
                            { throw new Exception("No existe el registro en Tbl_Inventario, favor de validar."); }

                            tblInventarioA.CatEstatusinventarioId = 1; //LIBRE

                            ctx.TblInventarios.Update(tblInventarioA);
                            ctx.SaveChanges();
                        }

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

        public usuario_inventario_negocio(rel_usuario_inventario_complex input, ActionUpdateResponsiva update)
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
                   
                        //relAsignacion.CatUsuarioId = this.asignacion.CatUsuarioId;
                        //relAsignacion.TblInventarioId = this.asignacion.TblInventarioId;
                        relAsignacion.Responsiva = this.asignacion.Responsiva;

                        ctx.RelUsuarioInventarios.Update(relAsignacion);
                        ctx.SaveChanges();

                    var inventario = ctx.VwInventarios.Where(x => x.Idinventario == relAsignacion.TblInventarioId).FirstOrDefault();
                    historico_inventario_negocio.CartaResponsivaAsignacionInventario(input.UsuarioAppid, inventario);


                    tran.Commit();
                        this.Respuesta = TipoAccion.Positiva("Se actualizo registro exitosamente.", relAsignacion.Id);
                    
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }

        public usuario_inventario_negocio(int id, int idUsuario, ActionDisable disable)
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




                    //Actualiza el estatus del inventario
                    TblInventario tblInventario = ctx.TblInventarios.Where(x => x.Id == relAsignacion.TblInventarioId).FirstOrDefault()!;
                    if (tblInventario == null)
                    { throw new Exception("No existe el registro en Tbl_Inventario, favor de validar."); }

                    tblInventario.CatEstatusinventarioId = 1; //LIBRE

                    ctx.TblInventarios.Update(tblInventario);
                    ctx.SaveChanges();

                    var inventario = ctx.VwInventarios.Where(x => x.Idinventario == tblInventario.Id).FirstOrDefault();
                    historico_inventario_negocio.DesasignacionInventario(idUsuario, inventario);

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
