using Data.Models;
using Entidades_complejas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;

namespace Negocio
{
    public class Adquisicion_negocio
    { 
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion? Respuesta { get; set; }
        public tbl_adquisicion_complex? adquisicion { get; set; }
        public Adquisicion_negocio(tbl_adquisicion_complex input, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    this.adquisicion = input;
                    TblAdquisicion ad = new TblAdquisicion();
                    ad.CatProveedorId = (int)this.adquisicion.CatProveedorId!;
                    ad.CatPropietarioId = (int)this.adquisicion.CatPropietarioId!;
                    ad.Monto = (double)this.adquisicion.Monto!;
                    ad.Impuesto = (double)this.adquisicion.Impuesto!;
                    ad.Articulos = (int)this.adquisicion.Articulos!;
                    ad.FacPdf = this.adquisicion.FacPdf!;
                    ad.FacXml = this.adquisicion.FacXml!;
                    ad.Fechadecompra = (DateTime)this.adquisicion.Fechadecompra!;
                    ctx.TblAdquisicions.Add(ad);
                    ctx.SaveChanges();

                    if (input.detalle != null && input.detalle.Count() > 0)
                    {
                        foreach (rel_adquisicion_detalle_complex detalle_complex in input.detalle)
                        {
                            RelAdquisicionDetalle relDetalle = new RelAdquisicionDetalle();

                            relDetalle.Cantidad = detalle_complex.Cantidad;
                            relDetalle.TblAdquisicionId = ad.Id;
                            relDetalle.CatProductoId = detalle_complex.CatProductoId;
                            relDetalle.Costosiunitario = detalle_complex.Costosiunitario;
                            relDetalle.Estatus = true;
                            relDetalle.Inclusion = DateTime.Now;

                            ctx.RelAdquisicionDetalles.Add(relDetalle);
                            ctx.SaveChanges();

                            List<tbl_inventario_complex> lstInv = new List<tbl_inventario_complex>();
                            for (int i = 0; i < detalle_complex.Cantidad; i++)
                            {
                                //Inserta inventario
                                TblInventario tblInventario = new TblInventario();

                                tblInventario.TblAdquisicionId = ad.Id;
                                tblInventario.CatProductoId = detalle_complex.CatProductoId;
                                tblInventario.CatEstatusinventarioId = 1;//LIBRE
                                tblInventario.Numerodeserie = string.Empty;
                                tblInventario.Inventarioclv = string.Empty;
                                tblInventario.Estatus = true;
                                tblInventario.Inclusion = DateTime.Now;

                                ctx.TblInventarios.Add(tblInventario);
                                ctx.SaveChanges();
                            }
                        }
                    }
                                     
                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa", ad.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);

                }
            }
        }

        public Adquisicion_negocio(tbl_adquisicion_complex input, ActionUpdate update)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    this.adquisicion = input;
                    TblAdquisicion ad = ctx.TblAdquisicions.Where(x => x.Id == (int)this.adquisicion.Id!).FirstOrDefault()!;

                    if (ad == null)
                    { throw new Exception("No existe el registro en Tbl_Adquisicion, favor de validar."); }
                    else
                    {
                        ad.CatProveedorId = (int)this.adquisicion.CatProveedorId!;
                        ad.CatPropietarioId = (int)this.adquisicion.CatPropietarioId!;
                        ad.Monto = (double)this.adquisicion.Monto!;
                        ad.Impuesto = (double)this.adquisicion.Impuesto!;
                        ad.Articulos = (int)this.adquisicion.Articulos!;
                        ad.FacPdf = this.adquisicion.FacPdf!;
                        ad.FacXml = this.adquisicion.FacXml!;
                        ad.Fechadecompra = (DateTime)this.adquisicion.Fechadecompra!;

                        ctx.TblAdquisicions.Update(ad);
                        ctx.SaveChanges();

                        if (input.detalle != null && input.detalle!.Count() > 0)
                        {
                            foreach (rel_adquisicion_detalle_complex detalle_complex in input.detalle)
                            {
                                RelAdquisicionDetalle relDetalle = ctx.RelAdquisicionDetalles.Where(x => x.Id == detalle_complex.Id && x.TblAdquisicionId == ad.Id).FirstOrDefault()!;

                                if (relDetalle == null)
                                {
                                    //Se considera como un nuevo registro
                                    RelAdquisicionDetalle relDetalleNew = new RelAdquisicionDetalle();

                                    relDetalleNew.Cantidad = detalle_complex.Cantidad;
                                    relDetalleNew.TblAdquisicionId = ad.Id;
                                    relDetalleNew.CatProductoId = detalle_complex.CatProductoId;
                                    relDetalleNew.Costosiunitario = detalle_complex.Costosiunitario;
                                    relDetalleNew.Estatus = true;
                                    relDetalleNew.Inclusion = DateTime.Now;

                                    ctx.RelAdquisicionDetalles.Add(relDetalleNew);
                                    ctx.SaveChanges();

                                    for (int i = 0; i > detalle_complex.Cantidad; i++)
                                    {
                                        //Inserta inventario
                                        TblInventario tblInventario = new TblInventario();

                                        tblInventario.TblAdquisicionId = ad.Id;
                                        tblInventario.CatProductoId = detalle_complex.CatProductoId;
                                        tblInventario.CatEstatusinventarioId = 1;//LIBRE
                                        tblInventario.Numerodeserie = string.Empty;
                                        tblInventario.Inventarioclv = string.Empty;
                                        tblInventario.Estatus = true;
                                        tblInventario.Inclusion = DateTime.Now;

                                        ctx.TblInventarios.Add(tblInventario);
                                        ctx.SaveChanges();
                                    }
                                }
                                else
                                {
                                    //Actualiza registro
                                    bool upInventario = false;
                                    //Valida si se actualizara la cantidad del inventario
                                    if (relDetalle.Cantidad != detalle_complex.Cantidad)
                                    { upInventario = true;}
                                        
                                    relDetalle.Cantidad = detalle_complex.Cantidad;
                                    //relDetalle.CatProductoId = detalle_complex.CatProductoId;
                                    relDetalle.Costosiunitario = detalle_complex.Costosiunitario;

                                    ctx.RelAdquisicionDetalles.Update(relDetalle);
                                    ctx.SaveChanges();

                                    if(upInventario)
                                    {
                                        //Valida que el numero de inventarios coincida
                                        int inventarios = ctx.TblInventarios.Where(x => x.TblAdquisicionId == ad.Id && x.CatProductoId == relDetalle.CatProductoId && relDetalle.Estatus == true).Count();

                                        if(inventarios < relDetalle.Cantidad) 
                                        {
                                            int faltantes = relDetalle.Cantidad - inventarios;

                                            for (int i = 0; i < faltantes; i++)
                                            {
                                                //Inserta inventario
                                                TblInventario tblInventario = new TblInventario();

                                                tblInventario.TblAdquisicionId = ad.Id;
                                                tblInventario.CatProductoId = relDetalle.CatProductoId;
                                                tblInventario.CatEstatusinventarioId = 1;//LIBRE
                                                tblInventario.Numerodeserie = string.Empty;
                                                tblInventario.Inventarioclv = string.Empty;
                                                tblInventario.Estatus = true;
                                                tblInventario.Inclusion = DateTime.Now;

                                                ctx.TblInventarios.Add(tblInventario);
                                                ctx.SaveChanges();
                                            }
                                        }
                                        else if(inventarios > relDetalle.Cantidad)
                                        {
                                            int sobrantes = inventarios - relDetalle.Cantidad;

                                            List<TblInventario> tblInventario = ctx.TblInventarios.Where(x => x.TblAdquisicionId == ad.Id && x.CatProductoId == relDetalle.CatProductoId && relDetalle.Estatus == true).OrderByDescending(x => x.Id).Take(sobrantes).ToList();

                                            tblInventario.ForEach(x => x.Estatus = false);

                                            ctx.TblInventarios.UpdateRange(tblInventario);
                                            ctx.SaveChanges();
                                        }

                                    }
                                }
                            }
                        }
                    }
                    
                    this.Respuesta = TipoAccion.Positiva("Actualizacion Exitosa", ad.Id);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }

        public Adquisicion_negocio(int id, ActionDisable disable)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    TblAdquisicion ad = ctx.TblAdquisicions.Where(x => x.Id == id).FirstOrDefault()!;

                    if (ad == null)
                    { throw new Exception("No existe el registro en Tbl_Adquisicion, favor de validar."); }
                    else
                    {
                        ad.Estatus = false;

                        ctx.TblAdquisicions.Update(ad);
                        ctx.SaveChanges();

                        //Baja de detalle
                        List<RelAdquisicionDetalle> relDetalle = ctx.RelAdquisicionDetalles.Where(x => x.TblAdquisicionId == id).ToList();
                        relDetalle.ForEach(x => x.Estatus = false);

                        ctx.RelAdquisicionDetalles.UpdateRange(relDetalle);
                        ctx.SaveChanges();

                        //Baja de inventario
                        List<TblInventario> tblInventarios = ctx.TblInventarios.Where(x => x.TblAdquisicionId == id).ToList();
                        tblInventarios.ForEach(x => x.Estatus = false);

                        ctx.TblInventarios.UpdateRange(tblInventarios);
                        ctx.SaveChanges();

                    }

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Baja Exitosa", ad.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }

        //Obtencion de datos
        public Adquisicion_negocio()
        { }

        public TipoAccion select(int? id)
        {
            try
            {
                var all = id == null ? ctx.TblAdquisicions
                          .Include(x => x.RelAdquisicionDetalles.Where(d => d.Estatus == true))
                          .Include(x => x.TblInventarios.Where(i => i.Estatus == true))
                          .Where(x => x.Estatus == true).ToList()
                          : ctx.TblAdquisicions
                          .Include(x => x.RelAdquisicionDetalles.Where(d => d.Estatus == true))
                          .Include(x => x.TblInventarios.Where(i => i.Estatus == true))
                          .Where(x => x.Id == id).ToList();

                if(all == null)
                { throw new Exception("No se encontraron registros en Tbl_Adquisiciones."); }
                else
                {
                    List<adquisicion_complex> full = JsonConvert.DeserializeObject<List<adquisicion_complex>>(JsonConvert.SerializeObject(all, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }))!;

                    foreach(adquisicion_complex adquisicion in full)
                    {
                        adquisicion.Proveedor = ctx.CatProveedors.Where(p => p.Id == adquisicion.CatProveedorId).FirstOrDefault().Razonsocial;
                        adquisicion.Propietario = ctx.CatPropietarios.Where(p => p.Id == adquisicion.CatPropietarioId).FirstOrDefault().Razonsocial;

                        adquisicion.TblInventarios.ForEach(x => x.Estatusinventario = ctx.CatEstatusinventarios.Where(p => p.Id == x.CatEstatusinventarioId).FirstOrDefault().Estatus);
                    }


                    return TipoAccion.Positiva(full);
                }                
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }
        public List<VwAdquisicion> todos()
        {
            return ctx.VwAdquisicions.ToList();
        }
        public List<VwAdquisicion> identificador(int id)
        {
            return ctx.VwAdquisicions.Where(X => X.Idadquisicion == id).ToList();
        }
    }
}
