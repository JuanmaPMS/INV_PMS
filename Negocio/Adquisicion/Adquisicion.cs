using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    ad.Id = (int)this.adquisicion.Id!;
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
                    if (input.detalle!.Count() > 0)
                    {
                        input.detalle!.ForEach(x => x.TblAdquisicionId = ad.Id);
                        Adquisicion_Detalle_negocio _Detalle_Negocio =
                            new Adquisicion_Detalle_negocio(input.detalle, new ActionAdd());
                    }
                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa", ad.Id, ad);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message, ex);

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

                        //if (input.detalle!.Count() > 0)
                        //{
                        //    input.detalle!.ForEach(x => x.TblAdquisicionId = ad.Id);
                        //    Adquisicion_Detalle_negocio _Detalle_Negocio =
                        //        new Adquisicion_Detalle_negocio(input.detalle, new ActionAdd());
                        //}
                    }
                    
                    this.Respuesta = TipoAccion.Positiva("Actualizacion Exitosa", ad.Id, ad);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message, ex);

                }
            }
        }

        public Adquisicion_negocio(tbl_adquisicion_complex input, ActionDisable disable)
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
                        ad.Estatus = false; 

                        ctx.TblAdquisicions.Update(ad);
                        ctx.SaveChanges();
                    }

                    this.Respuesta = TipoAccion.Positiva("Baja Exitosa", ad.Id, ad);
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message, ex);

                }
            }
        }

        //Obtencion de datos
        public Adquisicion_negocio()
        { }
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
