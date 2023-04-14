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
    public class inventario_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public tbl_inventario_complex inventario { get; set; }
        public TipoAccion Respuesta { get; set; }
        public Boolean estatus { get; set; }

        public inventario_negocio(List<tbl_inventario_complex> input, ActionAdd add)
        {
            try
            {
                foreach (tbl_inventario_complex inventario_complex in input)
                {
                    TblInventario tblInventario = new TblInventario();

                    tblInventario.TblAdquisicionId = inventario_complex.TblAdquisicionId;
                    tblInventario.CatProductoId = inventario_complex.CatProductoId;
                    tblInventario.CatEstatusinventarioId = (int)inventario_complex.CatEstatusinventarioId!;
                    tblInventario.Numerodeserie = inventario_complex.Numerodeserie;
                    tblInventario.Inventarioclv = inventario_complex.Inventarioclv;
                    tblInventario.Notas = inventario_complex.Notas;
                    tblInventario.Estatus = true;
                    tblInventario.Inclusion = DateTime.Now;

                    ctx.TblInventarios.Add(tblInventario);
                    ctx.SaveChanges();
                }

                estatus = true;
            }
            catch (Exception ex)
            {
                estatus = false;
            }
        }

        public inventario_negocio(tbl_inventario_complex input, ActionAdd add)
        {
            try
            {
                this.inventario = input;

                TblInventario tblInventario = new TblInventario();

                tblInventario.TblAdquisicionId = this.inventario.TblAdquisicionId;
                tblInventario.CatProductoId = this.inventario.CatProductoId;
                tblInventario.CatEstatusinventarioId = (int)this.inventario.CatEstatusinventarioId!;
                tblInventario.Numerodeserie = this.inventario.Numerodeserie;
                tblInventario.Inventarioclv = this.inventario.Inventarioclv;
                tblInventario.Notas = this.inventario.Notas;
                tblInventario.Estatus = true;
                tblInventario.Inclusion = DateTime.Now;

                ctx.TblInventarios.Add(tblInventario);
                ctx.SaveChanges();

                this.Respuesta = TipoAccion.Positiva("Alta Exitosa", tblInventario.Id, tblInventario);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message, ex);
            }
        }

        public inventario_negocio(tbl_inventario_complex input, ActionUpdate update)
        {
            try
            {
                this.inventario = input;
                TblInventario tblInventario = ctx.TblInventarios.Where(x => x.Id == (int)this.inventario.Id!).FirstOrDefault()!;

                if (tblInventario == null)
                { throw new Exception("No existe el registro en Tbl_Inventario, favor de validar."); }
                else
                {
                    tblInventario.CatEstatusinventarioId = (int)this.inventario.CatEstatusinventarioId!;
                    tblInventario.Numerodeserie = this.inventario.Numerodeserie;
                    tblInventario.Inventarioclv = this.inventario.Inventarioclv;
                    tblInventario.Notas = this.inventario.Notas;

                    ctx.TblInventarios.Update(tblInventario);
                    ctx.SaveChanges();
                }

                this.Respuesta = TipoAccion.Positiva("Actualizacion Exitosa", tblInventario.Id, tblInventario);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message, ex);
            }
        }

        public inventario_negocio(int id, ActionDisable disable)
        {
            try
            {
                TblInventario tblInventario = ctx.TblInventarios.Where(x => x.Id == id).FirstOrDefault()!;

                if (tblInventario == null)
                { throw new Exception("No existe el registro en Tbl_Inventario, favor de validar."); }
                else
                {
                    tblInventario.Estatus = false;

                    ctx.TblInventarios.Update(tblInventario);
                    ctx.SaveChanges();
                }

                this.Respuesta = TipoAccion.Positiva("Baja Exitosa", tblInventario.Id, tblInventario);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message, ex);
            }
        }

        //Obtencion de datos
        public inventario_negocio()
        { }

        public List<VwInventario> todos()
        {
            return ctx.VwInventarios.ToList();
        }
        public List<VwInventario> identificador(int id)
        {
            return ctx.VwInventarios.Where(X => X.Idinventario == id).ToList();
        }

    }
}
