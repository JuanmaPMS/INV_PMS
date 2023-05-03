using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Mantenimiento
{
    public class mantenimiento_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public tbl_mantenimiento_complex mantenimientoInventario { get; set; }
        public TipoAccion Respuesta { get; set; }

        public mantenimiento_negocio() { }

        public mantenimiento_negocio(tbl_mantenimiento_complex input, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    this.mantenimientoInventario = input;
                    TblMantenimientoInventario tblMantenimientoInv = new TblMantenimientoInventario();
                    tblMantenimientoInv.RelUsuarioInventarioId = (int)this.mantenimientoInventario.RelUsuarioInventarioId!;
                    tblMantenimientoInv.Inclusion = this.mantenimientoInventario.Inclusion!;

                    ctx.TblMantenimientoInventarios.Add(tblMantenimientoInv);
                    ctx.SaveChanges();

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa", tblMantenimientoInv.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }

            }
        }

        public mantenimiento_negocio(tbl_mantenimiento_complex input, ActionUpdate update)
        {
            try
            {
                TblMantenimientoInventario mantenimientoInventario = ctx.TblMantenimientoInventarios.Where(x => x.Id == input.Id).FirstOrDefault();
                if (mantenimientoInventario == null)
                { throw new Exception("No existe el registro, favor de validar."); }
                else
                {
                    mantenimientoInventario.RelUsuarioInventarioId = (int)input.RelUsuarioInventarioId!;
                    mantenimientoInventario.Inclusion = input.Inclusion!;


                    ctx.TblMantenimientoInventarios.Update(mantenimientoInventario);
                    ctx.SaveChanges();
                }

                this.Respuesta = TipoAccion.Positiva("Actualización Exitosa", mantenimientoInventario.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }


        public List<VwMantenimientoInventario> todos()
        {
            return ctx.VwMantenimientoInventarios
                .Where(x => x.Ultimomantenimiento < DateTime.Now && x.Ultimomantenimiento > DateTime.Now.AddMonths(-11))
            .ToList();
        }



    }
}
