using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Inventario
{
    public class inventario_accesorio_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion Respuesta { get; set; }

        public inventario_accesorio_negocio(List<tbl_inventario_accesorio_complex> input, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    foreach (tbl_inventario_accesorio_complex inventario_complex in input)
                    {
                        TblInventarioAccesoriosincluido tblAccesorio = new TblInventarioAccesoriosincluido();

                        tblAccesorio.TblInventarioId = inventario_complex.TblInventarioId;
                        tblAccesorio.Nombre = inventario_complex.Nombre.ToUpper().Trim();
                        tblAccesorio.Detalle = inventario_complex.Detalle == null ? null : inventario_complex.Detalle.ToUpper().Trim();

                        ctx.TblInventarioAccesoriosincluidos.Add(tblAccesorio);
                        ctx.SaveChanges();
                    }

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                }
            }              
        }
    }
}
