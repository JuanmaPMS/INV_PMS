using Data.Models;
using Entidades_complejas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.InventarioArrendamiento
{
    public class inventario_arrendamiento_ubicacion_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public rel_inventario_ubicacion_arrendamiento_complex ubicacion_inventario { get; set; }
        public TipoAccion Respuesta { get; set; }
        public inventario_arrendamiento_ubicacion_negocio() { }

        public List<equipo_arrendamiento_complex> GetFiltro(int id)
        {
            List<VwInventarioUbicacionArrendamiento> lst = ctx.VwInventarioUbicacionArrendamientos.Where(x => x.ClienteId == id).ToList();

            List<equipo_arrendamiento_complex> full = JsonConvert.DeserializeObject<List<equipo_arrendamiento_complex>>(JsonConvert.SerializeObject(lst, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }))!;

            return full;
        }

        public VwInventarioUbicacionArrendamiento GetDetail(int id)
        {
            return ctx.VwInventarioUbicacionArrendamientos.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<inventario_ubicacion_arrendamiento_complex> GetByUbicacion(int id)
        {
            List<RelInventarioUbicacionArrendamiento> relInventarios = ctx.RelInventarioUbicacionArrendamientos.Where(x => x.RelClienteUbicacionOficinaArrendamientoId == id && x.Estatus == true).ToList();
            List<inventario_ubicacion_arrendamiento_complex> result = new List<inventario_ubicacion_arrendamiento_complex>();

            foreach (RelInventarioUbicacionArrendamiento rel in relInventarios)
            {
                VwInventarioUbicacionArrendamiento data = ctx.VwInventarioUbicacionArrendamientos.Where(x => x.Id == rel.TblInventarioId).FirstOrDefault();

                if (data != null)
                {
                    inventario_ubicacion_arrendamiento_complex inventario = new inventario_ubicacion_arrendamiento_complex();
                    inventario.Id = rel.Id;
                    inventario.Usuario = data.UsuarioNombre;
                    inventario.Equipo = data.Categoria + "/" + data.Fabricante + "/" + data.Modelo;
                    inventario.Serie = data.Numerodeserie;
                    inventario.Clave = data.Inventarioclv;

                    result.Add(inventario);
                }
            }

            return result;
        }

        public inventario_arrendamiento_ubicacion_negocio(rel_inventario_ubicacion_arrendamiento_complex input, ActionAdd add)
        {
            try
            {
                this.ubicacion_inventario = input;

                if (!this.ubicacion_inventario.Confirm)
                {
                    //Valida si fue previamente validado
                    RelInventarioUbicacionArrendamiento relInventarioUbicacion = ctx.RelInventarioUbicacionArrendamientos.Where(x => x.TblInventarioId == input.TblInventarioId && x.Estatus == true).FirstOrDefault();

                    if (relInventarioUbicacion != null)
                    { throw new Exception("El Inventario se encuentra asignado a otra ubicación, ¿esta seguro qué desea actualizar la ubicación?."); }
                }

                if (this.ubicacion_inventario.Confirm)
                {
                    RelInventarioUbicacionArrendamiento relInventarioUbicacion = ctx.RelInventarioUbicacionArrendamientos.Where(x => x.TblInventarioId == input.TblInventarioId && x.Estatus == true).FirstOrDefault();
                    if (relInventarioUbicacion != null)
                    {
                        relInventarioUbicacion.Estatus = false;

                        ctx.RelInventarioUbicacionArrendamientos.Update(relInventarioUbicacion);
                        ctx.SaveChanges();
                    }
                }

                RelInventarioUbicacionArrendamiento addUbicacion = new RelInventarioUbicacionArrendamiento();
                addUbicacion.TblInventarioId = this.ubicacion_inventario.TblInventarioId;
                addUbicacion.RelClienteUbicacionOficinaArrendamientoId = this.ubicacion_inventario.RelClienteUbicacionOficinaArrendamientoId;
                addUbicacion.Estatus = true;
                addUbicacion.Inclusion = DateTime.Now;

                ctx.RelInventarioUbicacionArrendamientos.Add(addUbicacion);
                ctx.SaveChanges();

                this.Respuesta = TipoAccion.Positiva("Alta Exitosa", addUbicacion.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public inventario_arrendamiento_ubicacion_negocio(int id, ActionDisable disable)
        {
            try
            {
                //Valida que exista el registro
                RelInventarioUbicacionArrendamiento relInventarioUbicacion = ctx.RelInventarioUbicacionArrendamientos.Where(x => x.Id == id).FirstOrDefault();

                if (relInventarioUbicacion == null)
                { throw new Exception("No existe el registro en RelInventarioUbicacionArrendamientos, favor de validar."); }
                else
                {
                    //Actualiza registro
                    relInventarioUbicacion.Estatus = false;

                    ctx.RelInventarioUbicacionArrendamientos.Update(relInventarioUbicacion);
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
