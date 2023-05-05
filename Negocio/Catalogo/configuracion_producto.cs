using Data.Models;
using Entidades_complejas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class configuracion_producto_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();

        public TipoAccion Get(int? id)
        {
            try
            {
                List<VwCatConfiguracionProducto> categorias = id == null ? ctx.VwCatConfiguracionProductos.Where(x => x.Estatus == true).OrderBy(x => x.CatCategoriaProductoId).ThenBy(x => x.Descripcion).ToList()
                                                : ctx.VwCatConfiguracionProductos.Where(x => x.Id == id).ToList();
                if (categorias.Count == 0)
                {
                    throw new Exception("No existen registros en CatConfiguracionProducto");
                }
                else
                {
                    return TipoAccion.Positiva(categorias);
                }
            }
            catch (Exception ex)
            {

                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion GetByCategoria(int id)
        {
            try
            {
                List<VwCatConfiguracionProducto> categorias = ctx.VwCatConfiguracionProductos.Where(x => x.CatCategoriaProductoId == id && x.Estatus == true).OrderBy(x => x.Descripcion).ToList();
                                                
                if (categorias.Count == 0)
                {
                    throw new Exception("No existen registros en CatConfiguracionProducto para la Categoría consultada.");
                }
                else
                {
                    return TipoAccion.Positiva(categorias);
                }
            }
            catch (Exception ex)
            {

                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Add(cat_configuracion_producto_complex entidad)
        {
            try
            {
                CatConfiguracionProducto configuracion = ctx.CatConfiguracionProductos.Where(x => x.Descripcion.ToUpper() == entidad.Descripcion.ToUpper() && x.CatCategoriaProductoId == entidad.CatCategoriaProductoId && x.Estatus == true).FirstOrDefault();

                if (configuracion != null)
                { throw new Exception("Ya existe la Categoria en CatConfiguracionProducto, favor de validar."); }
                else
                {
                    CatConfiguracionProducto catConfiguracion = new CatConfiguracionProducto();
                    catConfiguracion.CatCategoriaProductoId = entidad.CatCategoriaProductoId;
                    catConfiguracion.Descripcion = entidad.Descripcion.ToUpper(); 
                    catConfiguracion.Estatus = true;
                    catConfiguracion.Inclusion = DateTime.Now;

                    ctx.CatConfiguracionProductos.Add(catConfiguracion);
                    ctx.SaveChanges();

                    return TipoAccion.Positiva("Se inserto registro exitosamente.", catConfiguracion.Id);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Update(cat_configuracion_producto_complex entidad)
        {
            try
            {
                CatConfiguracionProducto configuracion = ctx.CatConfiguracionProductos.Where(x => x.Id == entidad.Id).FirstOrDefault();

                if (configuracion == null)
                { throw new Exception("No existe el registro en CatConfiguracionProducto, favor de validar."); }
                else
                {
                    //Valida que no se duplique registro
                    CatConfiguracionProducto valida = ctx.CatConfiguracionProductos.Where(x => x.Descripcion.ToUpper() == entidad.Descripcion.ToUpper() && x.CatCategoriaProductoId == entidad.CatCategoriaProductoId && x.Estatus == true).FirstOrDefault();

                    if (valida != null)
                    { throw new Exception("No se puede duplicar el registro CatConfiguracionProducto, favor de validar."); }
                    else
                    {
                        configuracion.Descripcion = entidad.Descripcion.ToUpper();

                        ctx.CatConfiguracionProductos.Update(configuracion);
                        ctx.SaveChanges();

                        return TipoAccion.Positiva("Se actualizo registro exitosamente.", configuracion.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Disable(int id)
        {
            try
            {
                CatConfiguracionProducto configuracion = ctx.CatConfiguracionProductos.Where(x => x.Id == id).FirstOrDefault();

                if (configuracion == null)
                { throw new Exception("No existe el registro en CatConfiguracionProducto, favor de validar."); }
                else
                {
                    configuracion.Estatus = false;

                    ctx.CatConfiguracionProductos.Update(configuracion);
                    ctx.SaveChanges();

                    return TipoAccion.Positiva("Se inhabilito registro exitosamente.", configuracion.Id);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

    }
}
