using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class categoria_producto_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();

        public TipoAccion Get(int? id)
        {
            try
            {
                List<CatCategoriaProducto> categorias = id == null ? ctx.CatCategoriaProductos.Where(x => x.Estatus == true).OrderBy(x => x.Nombre).ToList()
                                                : ctx.CatCategoriaProductos.Where(x => x.Id == id).ToList();
                if (categorias.Count == 0)
                {
                    throw new Exception("No existen registros en Cat_Categoria_Producto");
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

        public TipoAccion Add(cat_categoria_producto_complex entidad)
        {
            try
            {
                CatCategoriaProducto categoria = ctx.CatCategoriaProductos.Where(x => x.Nombre.ToUpper() == entidad.Nombre.ToUpper() && x.Estatico == entidad.Estatico && x.Estatus == true).FirstOrDefault();

                if (categoria != null)
                { throw new Exception("Ya existe la Categoria en Cat_Categoria_Producto, favor de validar."); }
                else
                {
                    CatCategoriaProducto catCategoria = new CatCategoriaProducto();
                    catCategoria.Nombre = entidad.Nombre.ToUpper();
                    catCategoria.Estatus = true;
                    catCategoria.Estatico = entidad.Estatico;
                    catCategoria.Inclusion = DateTime.Now;

                    ctx.CatCategoriaProductos.Add(catCategoria);
                    ctx.SaveChanges();

                    return TipoAccion.Positiva("Se inserto registro exitosamente.", catCategoria.Id);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Update(cat_categoria_producto_complex entidad)
        {
            try
            {
                CatCategoriaProducto categoria = ctx.CatCategoriaProductos.Where(x => x.Id == entidad.Id).FirstOrDefault();

                if (categoria == null)
                { throw new Exception("No existe el registro en Cat_Categoria_Producto, favor de validar."); }
                else
                {
                    //Valida que no se duplique registro
                    CatCategoriaProducto valida = ctx.CatCategoriaProductos.Where(x => x.Nombre.ToUpper() == entidad.Nombre.ToUpper() && x.Estatico == entidad.Estatico && x.Id != entidad.Id && x.Estatus == true).FirstOrDefault();

                    if (valida != null)
                    { throw new Exception("No se puede duplicar el registro Cat_Categoria_Producto, favor de validar."); }
                    else
                    {
                        categoria.Nombre = entidad.Nombre.ToUpper();
                        categoria.Estatico = entidad.Estatico;

                        ctx.CatCategoriaProductos.Update(categoria);
                        ctx.SaveChanges();

                        return TipoAccion.Positiva("Se actualizo registro exitosamente.", categoria.Id);
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
                CatCategoriaProducto categoria = ctx.CatCategoriaProductos.Where(x => x.Id == id).FirstOrDefault();

                if (categoria == null)
                { throw new Exception("No existe el registro en Cat_Categoria_Producto, favor de validar."); }
                else
                {
                    categoria.Estatus = false;

                    ctx.CatCategoriaProductos.Update(categoria);
                    ctx.SaveChanges();

                    return TipoAccion.Positiva("Se inhabilito registro exitosamente.", categoria.Id);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }
    }
}
