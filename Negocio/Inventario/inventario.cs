﻿using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Inventario
{
    public class inventario_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public tbl_inventario_complex inventario { get; set; }
        public TipoAccion Respuesta { get; set; }
        public Boolean estatus { get; set; }

        public inventario_negocio(List<tbl_inventario_complex> input, ActionAdd add)
        {
            try
            {
                foreach (tbl_inventario_complex inventario_complex in input)
                {
                    TblInventario tblInventario = new TblInventario();

                    tblInventario.TblAdquisicionId = inventario_complex.TblAdquisicionId;
                    tblInventario.CatProductoId = inventario_complex.CatProductoId;
                    tblInventario.CatEstatusinventarioId = (int)inventario_complex.CatEstatusinventarioId!;
                    tblInventario.Numerodeserie = inventario_complex.Numerodeserie;
                    tblInventario.Inventarioclv = inventario_complex.Inventarioclv;
                    tblInventario.Notas = inventario_complex.Notas;
                    tblInventario.Estatus = true;
                    tblInventario.Inclusion = DateTime.Now;

                    ctx.TblInventarios.Add(tblInventario);
                    ctx.SaveChanges();
                }

                estatus = true;
            }
            catch (Exception ex)
            {
                estatus = false;
            }
        }

        public inventario_negocio(tbl_inventario_complex input, ActionAdd add)
        {
            try
            {
                this.inventario = input;

                TblInventario tblInventario = new TblInventario();

                tblInventario.TblAdquisicionId = this.inventario.TblAdquisicionId;
                tblInventario.CatProductoId = this.inventario.CatProductoId;
                tblInventario.CatEstatusinventarioId = (int)this.inventario.CatEstatusinventarioId!;
                tblInventario.Numerodeserie = this.inventario.Numerodeserie;
                tblInventario.Inventarioclv = this.inventario.Inventarioclv;
                tblInventario.Notas = this.inventario.Notas;
                tblInventario.Estatus = true;
                tblInventario.Inclusion = DateTime.Now;

                ctx.TblInventarios.Add(tblInventario);
                ctx.SaveChanges();

                this.Respuesta = TipoAccion.Positiva("Alta Exitosa", tblInventario.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public inventario_negocio(List<tbl_inventario_complex> input, ActionUpdate update)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    foreach (tbl_inventario_complex inventario_complex in input)
                    {
                        TblInventario tblInventario = ctx.TblInventarios.Where(x => x.Id == inventario_complex.Id).FirstOrDefault()!;

                        if (tblInventario == null)
                        { throw new Exception("No existe el registro en Tbl_Inventario, favor de validar."); }
                        else
                        {
                            if (inventario_complex.CatEstatusinventarioId != null)
                            { tblInventario.CatEstatusinventarioId = (int)inventario_complex.CatEstatusinventarioId; }
                            tblInventario.Numerodeserie = inventario_complex.Numerodeserie;
                            tblInventario.Inventarioclv = inventario_complex.Inventarioclv;
                            tblInventario.Notas = inventario_complex.Notas;

                            ctx.TblInventarios.Update(tblInventario);
                            ctx.SaveChanges();

                            if(inventario_complex.Accesorios != null && inventario_complex.Accesorios.Count > 0)
                            {
                                foreach(tbl_inventario_accesorio_complex accesorio_Complex in inventario_complex.Accesorios)
                                {
                                    TblInventarioAccesoriosincluido tblAccesorio = ctx.TblInventarioAccesoriosincluidos.Where(x => x.Id == accesorio_Complex.Id && x.TblInventarioId == tblInventario.Id).FirstOrDefault()!;

                                    if(tblAccesorio == null)
                                    {
                                        //Guarda el accesorio
                                        TblInventarioAccesoriosincluido accesorio = new TblInventarioAccesoriosincluido();
                                        accesorio.TblInventarioId = tblInventario.Id;
                                        accesorio.Nombre = accesorio_Complex.Nombre;
                                        accesorio.Detalle = accesorio_Complex.Detalle;

                                        ctx.TblInventarioAccesoriosincluidos.Add(accesorio);
                                        ctx.SaveChanges();
                                    }
                                    else
                                    {
                                        //Actualiza el accesorio
                                        tblAccesorio.Nombre = accesorio_Complex.Nombre;
                                        tblAccesorio.Detalle = accesorio_Complex.Detalle;

                                        ctx.TblInventarioAccesoriosincluidos.Update(tblAccesorio);
                                        ctx.SaveChanges();
                                    }
                                }
                            }
                        }
                    }

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Actualizacion Exitosa");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }

        public inventario_negocio(int id, ActionDisable disable)
        {
            try
            {
                TblInventario tblInventario = ctx.TblInventarios.Where(x => x.Id == id).FirstOrDefault()!;

                if (tblInventario == null)
                { throw new Exception("No existe el registro en Tbl_Inventario, favor de validar."); }
                else
                {
                    tblInventario.Estatus = false;

                    ctx.TblInventarios.Update(tblInventario);
                    ctx.SaveChanges();
                }

                this.Respuesta = TipoAccion.Positiva("Baja Exitosa", tblInventario.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        //Obtencion de datos
        public inventario_negocio()
        { }

        public List<VwInventario> todos()
        {
            return ctx.VwInventarios.ToList();
        }
        public List<VwInventario> identificador(int id)
        {
            return ctx.VwInventarios.Where(X => X.Idinventario == id).ToList();
        }

    }
}