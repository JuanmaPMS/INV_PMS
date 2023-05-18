using Data.Models;
using Entidades_complejas;
using Negocio.HistoricoInventario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Inventario
{
    public class inventario_accesorio_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion Respuesta { get; set; }

        public inventario_accesorio_negocio(List<tbl_inventario_accesorio_complex> input, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    TblInventarioAccesoriosincluido tblAccesorio = new TblInventarioAccesoriosincluido();
                    foreach (tbl_inventario_accesorio_complex inventario_complex in input)
                    {
                       
                        tblAccesorio.TblInventarioId = inventario_complex.TblInventarioId;
                        tblAccesorio.Nombre = inventario_complex.Nombre.ToUpper().Trim();
                        tblAccesorio.Detalle = inventario_complex.Detalle == null ? null : inventario_complex.Detalle.ToUpper().Trim();

                        ctx.TblInventarioAccesoriosincluidos.Add(tblAccesorio);
                        ctx.SaveChanges();
                        //Auditoria.Log(tblAccesorio, inventario_complex.usuarioAppid);
                        var inventario = ctx.VwInventarios.Where(x => x.Idinventario == inventario_complex.TblInventarioId).FirstOrDefault();
                        historico_inventario_negocio.CapturaAccesorioInventario((int)inventario_complex.usuarioAppid, inventario, inventario_complex.Nombre.ToUpper().Trim());
                    }

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa", tblAccesorio.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }              
        }

        public inventario_accesorio_negocio(List<tbl_inventario_accesorio_complex> input, ActionUpdate update)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    foreach (tbl_inventario_accesorio_complex inventario_complex in input)
                    {
                        TblInventarioAccesoriosincluido tblAccesorio = ctx.TblInventarioAccesoriosincluidos.Where(x => x.Id == inventario_complex.Id && x.TblInventarioId == inventario_complex.TblInventarioId).FirstOrDefault()!;

                        if(tblAccesorio == null)
                        { throw new Exception("No existe el registro en Tbl_Inventario_Accesoriosincluido, favor de validar."); }
                        else
                        {
                            string accesorioViejo = tblAccesorio.Nombre;
                            tblAccesorio.Nombre = inventario_complex.Nombre.ToUpper().Trim();
                            tblAccesorio.Detalle = inventario_complex.Detalle == null ? null : inventario_complex.Detalle.ToUpper().Trim();

                            ctx.TblInventarioAccesoriosincluidos.Update(tblAccesorio);
                            ctx.SaveChanges();
                            //Auditoria.Log(tblAccesorio, inventario_complex.usuarioAppid);
                            var inventario = ctx.VwInventarios.Where(x => x.Idinventario == inventario_complex.TblInventarioId).FirstOrDefault();
                            historico_inventario_negocio.ModificaAccesorioInventario((int)inventario_complex.usuarioAppid, inventario, accesorioViejo, inventario_complex.Nombre.ToUpper().Trim());
                        }                 
                    }

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Actualización Exitosa");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }

        public inventario_accesorio_negocio(int id, int idUsuario, ActionDisable disable)
        {
            try
            {
                TblInventarioAccesoriosincluido tblAccesorio = ctx.TblInventarioAccesoriosincluidos.Where(x => x.Id == id).FirstOrDefault()!;

                if (tblAccesorio == null)
                { throw new Exception("No existe el registro en Tbl_Inventario_Accesoriosincluido, favor de validar."); }
                else
                {
                    ctx.TblInventarioAccesoriosincluidos.Remove(tblAccesorio);
                    ctx.SaveChanges();
                    //Auditoria.Log(tblAccesorio, idUsuario);
                    var inventario = ctx.VwInventarios.Where(x => x.Idinventario == tblAccesorio.TblInventarioId).FirstOrDefault();
                    historico_inventario_negocio.eliminaAccesorioInventario(idUsuario, inventario, tblAccesorio.Nombre);

                    this.Respuesta = TipoAccion.Positiva("Eliminación Exitosa", tblAccesorio.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

    }
}
