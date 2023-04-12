using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Adquisicion_Detalle_negocio
    { 
        public RelAdquisicionDetalle addetalle { get; set; }
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public Boolean estatus { get; set; }
        public Adquisicion_Detalle_negocio(List<RelAdquisicionDetalle>? detalle, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    ctx.RelAdquisicionDetalles.AddRange((List<RelAdquisicionDetalle>)detalle!);
                    ctx.SaveChanges();
                    tran.Commit();
                    estatus = true;
                }
                catch {
                    tran.Rollback();
                    estatus= false;
                }
            }

        }
    }
}
