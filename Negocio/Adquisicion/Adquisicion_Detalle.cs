using Data.Models;
using Entidades_complejas;
using Negocio.Inventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Adquisicion_Detalle_negocio
    { 
        public RelAdquisicionDetalle addetalle { get; set; }
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public Boolean estatus { get; set; }
        public Adquisicion_Detalle_negocio(List<rel_adquisicion_detalle_complex> detalle, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    foreach (rel_adquisicion_detalle_complex detalle_complex in detalle)
                    {
                        RelAdquisicionDetalle relDetalle = new RelAdquisicionDetalle();

                        relDetalle.Cantidad = detalle_complex.Cantidad;
                        relDetalle.TblAdquisicionId = detalle_complex.TblAdquisicionId;
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
                            tbl_inventario_complex inventario = new tbl_inventario_complex();
                            inventario.TblAdquisicionId = detalle_complex.TblAdquisicionId;
                            inventario.CatProductoId = detalle_complex.CatProductoId;
                            inventario.CatEstatusinventarioId = 1;//LIBRE
                            inventario.Numerodeserie = string.Empty;
                            inventario.Inventarioclv = string.Empty;
                            
                            lstInv.Add(inventario);
                        }
                        inventario_negocio _Inventario_Negocio =
                                new inventario_negocio(lstInv, new ActionAdd());
                    }

                    estatus = true;
                }
                catch (Exception ex)
                {
                    estatus = false;
                }
            }                
        }

        public Adquisicion_Detalle_negocio(rel_adquisicion_detalle_complex detalle, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    RelAdquisicionDetalle relDetalle = new RelAdquisicionDetalle();

                    relDetalle.Cantidad = detalle.Cantidad;
                    relDetalle.TblAdquisicionId = detalle.TblAdquisicionId;
                    relDetalle.CatProductoId = detalle.CatProductoId;
                    relDetalle.Costosiunitario = detalle.Costosiunitario;
                    relDetalle.Estatus = true;
                    relDetalle.Inclusion = DateTime.Now;

                    ctx.RelAdquisicionDetalles.Add(relDetalle);
                    ctx.SaveChanges();

                    List<tbl_inventario_complex> lstInv = new List<tbl_inventario_complex>();
                    for (int i = 0; i < detalle.Cantidad; i++)
                    {
                        //Inserta inventario
                        TblInventario tblInventario = new TblInventario();

                        tblInventario.TblAdquisicionId = detalle.TblAdquisicionId;
                        tblInventario.CatProductoId = detalle.CatProductoId;
                        tblInventario.CatEstatusinventarioId = 1;//LIBRE
                        tblInventario.Numerodeserie = string.Empty;
                        tblInventario.Inventarioclv = string.Empty;
                        tblInventario.Estatus = true;
                        tblInventario.Inclusion = DateTime.Now;

                        ctx.TblInventarios.Add(tblInventario);
                        ctx.SaveChanges();
                    }

                    tran.Commit();
                    estatus = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    estatus = false;
                }
            }
        }
        public Adquisicion_Detalle_negocio(List<rel_adquisicion_detalle_complex> detalle, ActionUpdate update)
        {
            try
            {
                foreach (rel_adquisicion_detalle_complex detalle_complex in detalle)
                {
                    RelAdquisicionDetalle relDetalle = ctx.RelAdquisicionDetalles.Where(x => x.Id == detalle_complex.Id && x.TblAdquisicionId == detalle_complex.TblAdquisicionId).FirstOrDefault()!;

                    if (relDetalle == null)
                    {
                        //Se considera como un nuevo registro
                        RelAdquisicionDetalle relDetalleNew = new RelAdquisicionDetalle();

                        relDetalleNew.Cantidad = detalle_complex.Cantidad;
                        relDetalleNew.TblAdquisicionId = detalle_complex.TblAdquisicionId;
                        relDetalleNew.CatProductoId = detalle_complex.CatProductoId;
                        relDetalleNew.Costosiunitario = detalle_complex.Costosiunitario;
                        relDetalleNew.Estatus = true;
                        relDetalleNew.Inclusion = DateTime.Now;

                        ctx.RelAdquisicionDetalles.Add(relDetalleNew);
                        ctx.SaveChanges();

                        for (int i = 0; i < detalle_complex.Cantidad; i++)
                        {
                            //Inserta inventario
                            tbl_inventario_complex inventario = new tbl_inventario_complex();
                            inventario.TblAdquisicionId = detalle_complex.TblAdquisicionId;
                            inventario.CatProductoId = detalle_complex.CatProductoId;
                            inventario.CatEstatusinventarioId = 1;//LIBRE
                            inventario.Numerodeserie = string.Empty;
                            inventario.Inventarioclv = string.Empty;

                            inventario_negocio _Inventario_Negocio =
                                    new inventario_negocio(inventario, new ActionAdd());
                        }
                    }
                    else
                    {
                        //Actualiza registro
                        relDetalle.Cantidad = detalle_complex.Cantidad;
                        //relDetalle.CatProductoId = detalle_complex.CatProductoId;
                        relDetalle.Costosiunitario = detalle_complex.Costosiunitario;

                        ctx.RelAdquisicionDetalles.Update(relDetalle);
                        ctx.SaveChanges();
                    }
                }

                estatus = true;
            }
            catch
            {
                estatus = false;
            }
        }

        public Adquisicion_Detalle_negocio(rel_adquisicion_detalle_complex detalle, ActionUpdate update)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    RelAdquisicionDetalle relDetalle = ctx.RelAdquisicionDetalles.Where(x => x.Id == detalle.Id && x.TblAdquisicionId == detalle.TblAdquisicionId).FirstOrDefault()!;

                    if (relDetalle == null)
                    { throw new Exception("No existe el registro en Rel_Adquisicion_Detalle, favor de validar."); }
                    else
                    {
                        bool upInventario = false;
                        //Valida si se actualizara la cantidad del inventario
                        if (relDetalle.Cantidad != detalle.Cantidad)
                        { upInventario = true; }

                        //Actualiza registro
                        relDetalle.Cantidad = detalle.Cantidad;
                        //relDetalle.CatProductoId = detalle.CatProductoId;
                        relDetalle.Costosiunitario = detalle.Costosiunitario;

                        ctx.RelAdquisicionDetalles.Update(relDetalle);
                        ctx.SaveChanges();

                        if (upInventario)
                        {
                            //Valida que el numero de inventarios coincida
                            int inventarios = ctx.TblInventarios.Where(x => x.TblAdquisicionId == detalle.TblAdquisicionId && x.CatProductoId == relDetalle.CatProductoId && relDetalle.Estatus == true).Count();

                            if (inventarios < relDetalle.Cantidad)
                            {
                                int faltantes = relDetalle.Cantidad - inventarios;

                                for (int i = 0; i < faltantes; i++)
                                {
                                    //Inserta inventario
                                    TblInventario tblInventario = new TblInventario();

                                    tblInventario.TblAdquisicionId = detalle.TblAdquisicionId;
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
                            else if (inventarios > relDetalle.Cantidad)
                            {
                                int sobrantes = inventarios - relDetalle.Cantidad;

                                List<TblInventario> tblInventario = ctx.TblInventarios.Where(x => x.TblAdquisicionId == detalle.TblAdquisicionId && x.CatProductoId == relDetalle.CatProductoId && relDetalle.Estatus == true).OrderByDescending(x => x.Id).Take(sobrantes).ToList();

                                tblInventario.ForEach(x => x.Estatus = false);

                                ctx.TblInventarios.UpdateRange(tblInventario);
                                ctx.SaveChanges();
                            }
                        }


                    }
                    tran.Commit();
                    estatus = true;
                }
                catch
                {
                    tran.Rollback();
                    estatus = false;
                }
            }
        }

        public Adquisicion_Detalle_negocio(int id, ActionDisable disable)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    RelAdquisicionDetalle relDetalle = ctx.RelAdquisicionDetalles.Where(x => x.Id == id).FirstOrDefault()!;

                    if (relDetalle == null)
                    { throw new Exception("No existe el registro en Rel_Adquisicion_Detalle, favor de validar."); }
                    else
                    {
                        //Actualiza registro
                        relDetalle.Estatus = false;

                        ctx.RelAdquisicionDetalles.Update(relDetalle);
                        ctx.SaveChanges();

                        //Baja de inventario
                        List<TblInventario> tblInventarios = ctx.TblInventarios.Where(x => x.TblAdquisicionId == relDetalle.TblAdquisicionId && x.CatProductoId == relDetalle.CatProductoId).ToList();
                        tblInventarios.ForEach(x => x.Estatus = false);

                        ctx.TblInventarios.UpdateRange(tblInventarios);
                        ctx.SaveChanges();
                    }

                    tran.Commit();
                    estatus = true;
                }
                catch
                {
                    tran.Rollback();
                    estatus = false;
                }
            }
                
        }


    }
}
