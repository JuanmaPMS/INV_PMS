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
            try
            {
                RelProductoCatacteristica caracteristica = ctx.RelProductoCatacteristicas.Where(x => x.CatProductoId == input.CatProductoId && x.Nombre.ToUpper() == input.Nombre.ToUpper()).FirstOrDefault();

                if (caracteristica != null)
                { throw new Exception("Ya existe la Caracteristica en Rel_Producto_Caracteristica, favor de validar."); }
                else
                {
                    RelProductoCatacteristica relCaracteristica = new RelProductoCatacteristica();
                    relCaracteristica.CatProductoId = input.CatProductoId;
                    relCaracteristica.Nombre = input.Nombre.ToUpper().Trim();

                    ctx.RelProductoCatacteristicas.Add(relCaracteristica);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa", relCaracteristica.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public producto_caracteristicas_negocio(rel_producto_caracteristicas_complex input, ActionUpdate update)
        {
            try
            {
                RelProductoCatacteristica caracteristica = ctx.RelProductoCatacteristicas.Where(x => x.Id == input.Id).FirstOrDefault();

                if (caracteristica == null)
                { throw new Exception("No existe el registro en Rel_Producto_Caracteristica, favor de validar."); }
                else
                {
                    caracteristica.Nombre = input.Nombre.ToUpper().Trim();

                    ctx.RelProductoCatacteristicas.Update(caracteristica);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Actualizacion Exitosa", caracteristica.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public producto_caracteristicas_negocio(int id, ActionDisable disable)
        {
            try
            {
                RelProductoCatacteristica caracteristica = ctx.RelProductoCatacteristicas.Where(x => x.Id == id).FirstOrDefault()!;

                if (caracteristica == null)
                { throw new Exception("No existe el registro en Rel_Producto_Caracteristica, favor de validar."); }
                else
                {
                    ctx.RelProductoCatacteristicas.Remove(caracteristica);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Eliminación Exitosa", caracteristica.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

    }
}
