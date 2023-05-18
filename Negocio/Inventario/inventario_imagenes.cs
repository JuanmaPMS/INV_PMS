using Data.Models;
using Entidades_complejas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Inventario
{
    public class inventario_imagenes_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion Respuesta { get; set; }

        public inventario_imagenes_negocio() { }

        public List<tbl_inventario_imagen_complex> Get(int id)
        {
            List<TblInventarioImagene> lst = ctx.TblInventarioImagenes.Where(x => x.TblInventarioId == id && x.Estatus == true).ToList();

            List<tbl_inventario_imagen_complex> full = JsonConvert.DeserializeObject<List<tbl_inventario_imagen_complex>>(JsonConvert.SerializeObject(lst, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }))!;

            return full;
        }

        public inventario_imagenes_negocio(List<tbl_inventario_imagen_complex> input, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                { 
                    foreach (tbl_inventario_imagen_complex inventario_complex in input)
                    {
                        TblInventarioImagene tblImagen = new TblInventarioImagene();
                        tblImagen.TblInventarioId = inventario_complex.TblInventarioId;
                        tblImagen.Imagen = inventario_complex.Imagen;
                        tblImagen.Estatus = true;
                        tblImagen.Inclusion = DateTime.Now;

                        ctx.TblInventarioImagenes.Add(tblImagen);
                        ctx.SaveChanges();
                        Auditoria.Log(tblImagen, inventario_complex.usuarioAppid);
                    }

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }

        public inventario_imagenes_negocio(List<tbl_inventario_imagen_complex> input, ActionUpdate update)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    foreach (tbl_inventario_imagen_complex inventario_complex in input)
                    {
                        TblInventarioImagene tblImagen = ctx.TblInventarioImagenes.Where(x => x.Id == inventario_complex.Id && x.TblInventarioId == inventario_complex.TblInventarioId).FirstOrDefault()!;

                        if (tblImagen == null)
                        {
                            TblInventarioImagene tblNewImagen = new TblInventarioImagene();
                            tblNewImagen.TblInventarioId = inventario_complex.TblInventarioId;
                            tblNewImagen.Imagen = inventario_complex.Imagen;
                            tblNewImagen.Estatus = true;
                            tblNewImagen.Inclusion = DateTime.Now;

                            ctx.TblInventarioImagenes.Add(tblNewImagen);
                            ctx.SaveChanges();
                            Auditoria.Log(tblNewImagen, inventario_complex.usuarioAppid);
                        }
                        else
                        {
                            tblImagen.Imagen = inventario_complex.Imagen; 
                            
                            ctx.TblInventarioImagenes.Update(tblImagen);
                            ctx.SaveChanges();
                            Auditoria.Log(tblImagen, inventario_complex.usuarioAppid);
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

        public inventario_imagenes_negocio(int id, int idUsuario, ActionDisable disable)
        {
            try
            {
                TblInventarioImagene tblImagen = ctx.TblInventarioImagenes.Where(x => x.Id == id).FirstOrDefault()!;

                if (tblImagen == null)
                { throw new Exception("No existe el registro en Tbl_Inventario_Accesoriosincluido, favor de validar."); }
                else
                {
                    tblImagen.Estatus = false;

                    ctx.TblInventarioImagenes.Update(tblImagen);
                    ctx.SaveChanges();
                    Auditoria.Log(tblImagen, idUsuario);
                    this.Respuesta = TipoAccion.Positiva("Eliminación Exitosa", tblImagen.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }



    }
}
