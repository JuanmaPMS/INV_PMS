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
                    if (input.detalle.Count() > 0)
                    {
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
    }
}
