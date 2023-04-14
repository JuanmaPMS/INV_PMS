using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class producto_caracteristicas_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public List<RelProductoCatacteristica> productoCaracter { get; set; }
        public TipoAccion Respuesta { get; set; }
        public producto_caracteristicas_negocio(List<RelProductoCatacteristica> input)
        { 
            this.productoCaracter = input;
        }
        public Boolean addRange()
        {
            using (var dbContextTransaction = ctx.Database.BeginTransaction())
            {
                try
                {
                    ctx.RelProductoCatacteristicas.AddRange(this.productoCaracter);
                    ctx.SaveChanges();
                    dbContextTransaction.Commit();
                    return true;
                }
                catch(Exception ex) {
                    dbContextTransaction.Rollback();
                    return false;
                }
            }
        }

        public producto_caracteristicas_negocio(rel_producto_caracteristicas_complex input, ActionAdd add)
        {
            
        }

    }
}
