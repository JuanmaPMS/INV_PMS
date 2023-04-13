using Data.Models;
using Entidades_complejas;
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
                    foreach(rel_adquisicion_detalle_complex detalle_complex in detalle)
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
                    }
                    
                    tran.Commit();
                    estatus = true;
                }
                catch {
                    tran.Rollback();
                    estatus= false;
                }
            }
        }

        public Adquisicion_Detalle_negocio(List<rel_adquisicion_detalle_complex> detalle, ActionUpdate update)
        {
            using (var tran = ctx.Database.BeginTransaction())
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
                        }
                        else
                        {
                            //Actualiza registro
                            relDetalle.Cantidad = detalle_complex.Cantidad;
                            relDetalle.CatProductoId = detalle_complex.CatProductoId;
                            relDetalle.Costosiunitario = detalle_complex.Costosiunitario;

                            ctx.RelAdquisicionDetalles.Update(relDetalle);
                            ctx.SaveChanges();
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
                        //Actualiza registro
                        relDetalle.Cantidad = detalle.Cantidad;
                        relDetalle.CatProductoId = detalle.CatProductoId;
                        relDetalle.Costosiunitario = detalle.Costosiunitario;

                        ctx.RelAdquisicionDetalles.Update(relDetalle);
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
