using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class producto_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public cat_producto_complex producto { get; set; }
        public TipoAccion Respuesta { get; set; }
        //Constructor de alta 
        public producto_negocio(cat_producto_complex input, ActionAdd add)
        {
            
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    this.producto = input;
                    CatProducto catProducto = new CatProducto();
                    catProducto.CatCategoriaProductoId = (int)this.producto.CatCategoriaProductoId!;
                    catProducto.CatFabricanteId = (int)this.producto.CatFabricanteId!;
                    catProducto.Modelo = this.producto.Modelo!;
                    catProducto.Anio = this.producto.Anio;
                    catProducto.Nuevo = (Boolean)this.producto.Nuevo!;
                    catProducto.Vidautil = (int)this.producto.Vidautil!;
                    ctx.CatProductos.Add(catProducto);
                    ctx.SaveChanges();
                    List<RelProductoCatacteristica> caracteristicas = new List<RelProductoCatacteristica>();
                    foreach (String item in input.Caracteristicas_!)
                    {
                        RelProductoCatacteristica relProductoCatacteristica = new RelProductoCatacteristica();
                        relProductoCatacteristica.CatProductoId = catProducto.Id;
                        relProductoCatacteristica.Nombre = item;
                    }
                    producto_caracteristicas_negocio producto_Caracteristicas_Negocio =
                        new producto_caracteristicas_negocio(caracteristicas);
                    producto_Caracteristicas_Negocio.addRange();
                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa", catProducto.Id, catProducto);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message, ex);
                }

            }
        }
        //Metodos de Seleccion
        public producto_negocio()
        { }
        public List<VwCatProducto> todos()
        { 
            return ctx.VwCatProductos.ToList();
        }
        public List<VwCatProducto> identificador(int id)
        {
            return ctx.VwCatProductos.Where(X=> X.Idproducto == id).ToList();
        }
        public List<String> autocomplete()
        {
            List<String> output = new List<String>();
            List<VwCatProducto> list = ctx.VwCatProductos.ToList();
            foreach (VwCatProducto item in list)
            {
                String value = item.Idproducto+ "|" + 
                    item.Fabricante + "|" + 
                    item.Modelo + "|" + 
                    item.Categoria; 
                output.Add(value);
            }
            return output;
        }
    }
}
