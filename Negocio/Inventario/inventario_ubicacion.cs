using Data.Models;
using Entidades_complejas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Negocio.Inventario
{
    public class inventario_ubicacion_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public rel_inventario_ubicacion_complex ubicacion_inventario { get; set; }
        public TipoAccion Respuesta { get; set; }
        public inventario_ubicacion_negocio() { }

        public List<equipo_complex> GetFiltro()
        {
            List<VwInventarioUbicacion> lst =  ctx.VwInventarioUbicacions.ToList();

            List<equipo_complex> full = JsonConvert.DeserializeObject<List<equipo_complex>>(JsonConvert.SerializeObject(lst, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }))!;

            return full;
        }

        public VwInventarioUbicacion GetDetail(int id)
        {
            return ctx.VwInventarioUbicacions.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<inventario_ubicacion_complex> GetByUbicacion(int id)
        {
            List<RelInventarioUbicacion> relInventarios = ctx.RelInventarioUbicacions.Where(x => x.RelClienteUbicacionOficinaId == id && x.Estatus == true).ToList();
            List<inventario_ubicacion_complex> result = new List<inventario_ubicacion_complex>();

            foreach (RelInventarioUbicacion rel in relInventarios)
            {
                VwInventarioUbicacion data = ctx.VwInventarioUbicacions.Where(x => x.Id == rel.TblInventarioId).FirstOrDefault();
                inventario_ubicacion_complex inventario = new inventario_ubicacion_complex();
                inventario.Id = rel.Id;
                inventario.Usuario = data.UsuarioNombre;
                inventario.Equipo = data.Categoria + "/" + data.Fabricante + "/" + data.Modelo;
                inventario.Serie = data.Numerodeserie;
                inventario.Clave = data.Inventarioclv;

                result.Add(inventario); 
            }    

            return result;
        }

        public inventario_ubicacion_negocio(rel_inventario_ubicacion_complex input, ActionAdd add)
        {
            try
            {
                this.ubicacion_inventario = input;

                if(!this.ubicacion_inventario.Confirm)
                {
                    //Valida si fue previamente validado
                    RelInventarioUbicacion relInventarioUbicacion = ctx.RelInventarioUbicacions.Where(x => x.TblInventarioId == input.TblInventarioId && x.Estatus == true).FirstOrDefault();

                    if(relInventarioUbicacion != null)
                    { throw new Exception("El Inventario se encuentra asignado a otra ubicación, ¿esta seguro qué desea actualizar la ubicación?.");  }
                }

                if(this.ubicacion_inventario.Confirm)
                {
                    RelInventarioUbicacion relInventarioUbicacion = ctx.RelInventarioUbicacions.Where(x => x.TblInventarioId == input.TblInventarioId && x.Estatus == true).FirstOrDefault();
                    if (relInventarioUbicacion != null)
                    {
                        relInventarioUbicacion.Estatus = false;

                        ctx.RelInventarioUbicacions.Update(relInventarioUbicacion);
                        ctx.SaveChanges();
                    }
                }

                RelInventarioUbicacion addUbicacion = new RelInventarioUbicacion();
                addUbicacion.TblInventarioId = this.ubicacion_inventario.TblInventarioId;
                addUbicacion.RelClienteUbicacionOficinaId = this.ubicacion_inventario.RelClienteUbicacionOficinaId;
                addUbicacion.Estatus = true;
                addUbicacion.Inclusion = DateTime.Now;

                ctx.RelInventarioUbicacions.Add(addUbicacion);
                ctx.SaveChanges();

                this.Respuesta = TipoAccion.Positiva("Alta Exitosa", addUbicacion.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public inventario_ubicacion_negocio(int id, ActionDisable disable)
        {
            try
            {
                //Valida que exista el registro
                RelInventarioUbicacion relInventarioUbicacion = ctx.RelInventarioUbicacions.Where(x => x.Id == id).FirstOrDefault();

                if (relInventarioUbicacion == null)
                { throw new Exception("No existe el registro en RelInventarioUbicacions, favor de validar."); }
                else
                {
                    //Actualiza registro
                    relInventarioUbicacion.Estatus = false;

                    ctx.RelInventarioUbicacions.Update(relInventarioUbicacion);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se inhabilito registro exitosamente.", relInventarioUbicacion.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }



    }
}
