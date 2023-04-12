using Data.Models;
using Entidades_complejas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Catalogo
{
    public class proveedor_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion Get(int? id)
        {
            try
            {
                List<CatProveedor> proveedors = id == null ? ctx.CatProveedors
                                                            .Include(p => p.RelProveedorContactosoportes)
                                                            .ThenInclude(c => c.CatContactosoporte)
                                                            .Where(x => x.Estatus == true).OrderBy(x => x.Razonsocial).ToList()
                                                : ctx.CatProveedors
                                                  .Include(p => p.RelProveedorContactosoportes)
                                                  .ThenInclude(c => c.CatContactosoporte)
                                                  .Where(x => x.Id == id).ToList();

                if (proveedors.Count == 0)
                { throw new Exception("No existen registros en Cat_Proveedor"); }
                else
                {
                    List<cat_proveedor_complex> proveedores = new List<cat_proveedor_complex>();

                    return TipoAccion.Positiva(proveedores); 
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }
    }
}
