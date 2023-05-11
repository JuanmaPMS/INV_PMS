using Data.Models;
using Entidades_complejas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Negocio
{
    public class usuario_inventario_contenedor_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion? Respuesta { get; set; }
        public tbl_usuario_inventario_contenedor_complex? asignacion { get; set; }

        public usuario_inventario_contenedor_negocio(tbl_usuario_inventario_contenedor_complex input, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {

                    TblUsuarioInventarioContenedor contenedor = new TblUsuarioInventarioContenedor();
                    contenedor.Id = 0;
                    contenedor.RelUsuarioInventarioId = input.RelUsuarioInventarioId;
                    contenedor.Contenedor = input.Contenedor;
                    contenedor.Inclusion = DateTime.Now;

                    ctx.TblUsuarioInventarioContenedors.Add(contenedor);
                    ctx.SaveChanges();

                    foreach(tbl_usuario_inventario_contenedor_imagenes_complex imagen in input.TblUsuarioInventarioContenedorImagenes)
                    {
                        TblUsuarioInventarioContenedorImagene imag = new TblUsuarioInventarioContenedorImagene();
                        imag.Id = 0;
                        imag.Imagen = imagen.Imagen;
                        imag.Descripcion = imagen.Descripcion;
                        imag.TblUsuarioInventarioContenedorId = contenedor.Id;
                        imag.Inclusion = DateTime.Now;
                        ctx.TblUsuarioInventarioContenedorImagenes.Add(imag);
                        ctx.SaveChanges();
                    }


                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa", contenedor.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }

        public usuario_inventario_contenedor_negocio(tbl_usuario_inventario_contenedor_complex input, ActionUpdate add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {



                    TblUsuarioInventarioContenedor contenedor = ctx.TblUsuarioInventarioContenedors.Where(x=>x.Id == input.Id).FirstOrDefault();

                    if(contenedor == null )
                    {
                         throw new Exception("No existe el contenedor."); 
                    }

                    contenedor.Contenedor = input.Contenedor;

                    ctx.TblUsuarioInventarioContenedors.Update(contenedor);
                    ctx.SaveChanges();

                    foreach (tbl_usuario_inventario_contenedor_imagenes_complex imagen in input.TblUsuarioInventarioContenedorImagenes)
                    {
                        TblUsuarioInventarioContenedorImagene imag = new TblUsuarioInventarioContenedorImagene();

                        //SOLO SE ACTUALIZA LA DESCRIPCION
                        if (imagen.Id > 0)
                        {
                            imag = ctx.TblUsuarioInventarioContenedorImagenes.Where(x => x.Id == imagen.Id).FirstOrDefault();
                            imag.Descripcion = imagen.Descripcion;
                            ctx.TblUsuarioInventarioContenedorImagenes.Update(imag);
                        } else
                        {
                            imag.Id = 0;
                            imag.Imagen = imagen.Imagen;
                            imag.Descripcion = imagen.Descripcion;
                            imag.TblUsuarioInventarioContenedorId = contenedor.Id;
                            imag.Inclusion = DateTime.Now;
                            ctx.TblUsuarioInventarioContenedorImagenes.Add(imag);
                        }
                        ctx.SaveChanges();

                        
                    }

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Actualización Exitosa", contenedor.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }
       
        public usuario_inventario_contenedor_negocio(int idImagenContenedor, ActionDisable disable)
        {
            try
            {
                //Valida que exista el registro
                TblUsuarioInventarioContenedorImagene imagenContenedor = ctx.TblUsuarioInventarioContenedorImagenes.Where(x => x.Id == idImagenContenedor).FirstOrDefault();

                if (imagenContenedor == null)
                { throw new Exception("No existe el registro en TblUsuarioInventarioContenedorImagenes, favor de validar."); }
                else
                {
                    ctx.TblUsuarioInventarioContenedorImagenes.Remove(imagenContenedor);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se inhabilito registro exitosamente.", idImagenContenedor);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public usuario_inventario_contenedor_negocio()
        { }

        public TipoAccion Get(int? id)
        {
            try
            {
                List<TblUsuarioInventarioContenedor> contenedores = ctx.TblUsuarioInventarioContenedors.Where(x=>x.RelUsuarioInventarioId== id).Include(q => q.TblUsuarioInventarioContenedorImagenes).ToList();


                if (contenedores.Count == 0)
                { throw new Exception("No existen registros en TblUsuarioInventarioContenedors"); }
                else
                {
                    List<TblUsuarioInventarioContenedor> full = JsonConvert.DeserializeObject<List<TblUsuarioInventarioContenedor>>(JsonConvert.SerializeObject(contenedores, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }))!;

                    return TipoAccion.Positiva(full);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion GetById(int? id)
        {
            try
            {
                TblUsuarioInventarioContenedor contenedores = ctx.TblUsuarioInventarioContenedors.Where(x=>x.Id== id).Include(q => q.TblUsuarioInventarioContenedorImagenes).FirstOrDefault();


                if (contenedores == null)
                { throw new Exception("No existen registros en TblUsuarioInventarioContenedors"); }
                else
                {
                    TblUsuarioInventarioContenedor full = JsonConvert.DeserializeObject<TblUsuarioInventarioContenedor>(JsonConvert.SerializeObject(contenedores, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }))!;

                    return TipoAccion.Positiva(full);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }


    }
}
