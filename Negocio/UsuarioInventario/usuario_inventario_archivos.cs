using Data.Models;
using Entidades_complejas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.UsuarioInventario
{
    public class usuario_inventario_archivos_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion? Respuesta { get; set; }
        public rel_usuario_inventario_archivo_complex? archivo { get; set; }

        public usuario_inventario_archivos_negocio(List<rel_usuario_inventario_archivo_complex> input, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {

                    List<RelArchivosUsuarioInventario> lstArchivos = new List<RelArchivosUsuarioInventario>();

                    foreach (rel_usuario_inventario_archivo_complex item in input)
                    {
                        RelArchivosUsuarioInventario archivo = new RelArchivosUsuarioInventario();
                        archivo.Id = 0;
                        archivo.RelUsuarioInventarioId = item.RelUsuarioInventarioId;
                        archivo.Archivo = item.Archivo;
                        archivo.Estatus = true;
                        archivo.Inclusion = DateTime.Now;
                        lstArchivos.Add(archivo);
                    }

                    ctx.RelArchivosUsuarioInventarios.AddRange(lstArchivos);
                    ctx.SaveChanges();

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Alta de Archivos Exitosa");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }

        public usuario_inventario_archivos_negocio(int id, ActionDisable disable)
        {
            try
            {
                //Valida que exista el registro
                RelArchivosUsuarioInventario relArchivo = ctx.RelArchivosUsuarioInventarios.Where(x => x.Id == id).FirstOrDefault();

                if (relArchivo == null)
                { throw new Exception("No existe el registro en RelArchivosUsuarioInventarios, favor de validar."); }
                else
                {
                    //Actualiza registro
                    relArchivo.Estatus = false;

                    ctx.RelArchivosUsuarioInventarios.Update(relArchivo);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se inhabilito registro exitosamente.", relArchivo.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public usuario_inventario_archivos_negocio()
        { }

        public TipoAccion Get(int id)
        {
            try
            {
                List<RelArchivosUsuarioInventario> archivos = ctx.RelArchivosUsuarioInventarios.Where(x => x.RelUsuarioInventarioId == id && x.Estatus == true).ToList();

                if (archivos.Count == 0)
                { throw new Exception("No existen registros en RelArchivosUsuarioInventarios"); }
                else
                {
                    List<rel_usuario_inventario_archivo_complex> full = JsonConvert.DeserializeObject<List<rel_usuario_inventario_archivo_complex>>(JsonConvert.SerializeObject(archivos, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }))!;

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
