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
                        caracteristicas.Add(relProductoCatacteristica);
                    }

                    ctx.RelProductoCatacteristicas.AddRange(caracteristicas);
                    ctx.SaveChanges();

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa", catProducto.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }

            }
        }

        public producto_negocio(cat_producto_complex input, ActionUpdate update)
        {
            try
            {
                CatProducto catProducto = ctx.CatProductos.Where(x => x.Id == input.Id).FirstOrDefault();
                if (catProducto == null)
                { throw new Exception("No existe el registro en Cat_Productos, favor de validar."); }
                else
                {
                    catProducto.CatCategoriaProductoId = (int)input.CatCategoriaProductoId!;
                    catProducto.CatFabricanteId = (int)input.CatFabricanteId!;
                    catProducto.Modelo = input.Modelo!;
                    catProducto.Anio = input.Anio;
                    catProducto.Nuevo = (Boolean)input.Nuevo!;
                    catProducto.Vidautil = (int)input.Vidautil!;

                    ctx.CatProductos.Update(catProducto);
                    ctx.SaveChanges();
                }

                this.Respuesta = TipoAccion.Positiva("Actualización Exitosa", catProducto.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }
        public producto_negocio(int id, ActionDisable disable)
        {
            try
            {
                CatProducto catProducto = ctx.CatProductos.Where(x => x.Id == id).FirstOrDefault();
                if (catProducto == null)
                { throw new Exception("No existe el registro en Cat_Productos, favor de validar."); }
                else
                {
                    catProducto.Estatus = false;

                    ctx.CatProductos.Update(catProducto);
                    ctx.SaveChanges();
                }

                this.Respuesta = TipoAccion.Positiva("Baja Exitosa", catProducto.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
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

        public List<RelProductoCatacteristica> caracteristicas(int id)
        {
            return ctx.RelProductoCatacteristicas.Where(X => X.CatProductoId == id).ToList();
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
