using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.UbicacionArrendamiento
{
    public class ubicacion_arrendamiento_oficina_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion? Respuesta { get; set; }
        public rel_ubicacion_oficina_arrendamiento_complex? oficina { get; set; }

        public ubicacion_arrendamiento_oficina_negocio(rel_ubicacion_oficina_arrendamiento_complex input, ActionAdd add)
        {
            try
            {
                this.oficina = input;
                RelClienteUbicacionOficinaArrendamiento addOficina = new RelClienteUbicacionOficinaArrendamiento();
                addOficina.TblClienteUbicacionArrendamientoId = this.oficina.TblClienteUbicacionArrendamientoId;
                addOficina.Nombre = this.oficina.Nombre;
                addOficina.EjeX = this.oficina.EjeX;
                addOficina.EjeY = this.oficina.EjeY;
                addOficina.Alto = this.oficina.Alto;
                addOficina.Ancho = this.oficina.Ancho;
                addOficina.Estatus = true;
                addOficina.Inclusion = DateTime.Now;

                ctx.RelClienteUbicacionOficinaArrendamientos.Add(addOficina);
                ctx.SaveChanges();

                this.Respuesta = TipoAccion.Positiva("Alta Exitosa", addOficina.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }

        }
        public ubicacion_arrendamiento_oficina_negocio(rel_ubicacion_oficina_arrendamiento_complex input, ActionUpdate update)
        {
            try
            {
                this.oficina = input;
                //Valida que exista el registro
                RelClienteUbicacionOficinaArrendamiento relUbicacionOficina = ctx.RelClienteUbicacionOficinaArrendamientos.Where(x => x.Id == this.oficina.Id).FirstOrDefault();

                if (relUbicacionOficina == null)
                { throw new Exception("No existe el registro en RelClienteUbicacionOficinaArrendamientos, favor de validar."); }
                else
                {
                    relUbicacionOficina.Nombre = this.oficina.Nombre;
                    relUbicacionOficina.EjeX = this.oficina.EjeX;
                    relUbicacionOficina.EjeY = this.oficina.EjeY;
                    relUbicacionOficina.Alto = this.oficina.Alto;
                    relUbicacionOficina.Ancho = this.oficina.Ancho;

                    ctx.RelClienteUbicacionOficinaArrendamientos.Update(relUbicacionOficina);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se actualizo registro exitosamente.", relUbicacionOficina.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public ubicacion_arrendamiento_oficina_negocio(int id, ActionDisable disable)
        {
            try
            {
                //Valida que exista el registro
                RelClienteUbicacionOficinaArrendamiento relUbicacionOficina = ctx.RelClienteUbicacionOficinaArrendamientos.Where(x => x.Id == id).FirstOrDefault();

                if (relUbicacionOficina == null)
                { throw new Exception("No existe el registro en RelClienteUbicacionOficinaArrendamientos, favor de validar."); }
                else
                {
                    //Elimina inventario asignado
                    List<RelInventarioUbicacionArrendamiento> relInventarioUbicacion = ctx.RelInventarioUbicacionArrendamientos.Where(x => x.RelClienteUbicacionOficinaArrendamientoId == id).ToList();

                    if (relInventarioUbicacion.Count > 0)
                    {
                        relInventarioUbicacion.ForEach(x => x.Estatus = false);
                        ctx.SaveChanges();
                    }

                    //Actualiza registro
                    relUbicacionOficina.Estatus = false;

                    ctx.RelClienteUbicacionOficinaArrendamientos.Update(relUbicacionOficina);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se inhabilito registro exitosamente.", relUbicacionOficina.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public ubicacion_arrendamiento_oficina_negocio()
        { }

        public bool ValidaAsignados(int id)
        {
            bool asignados = false;

            List<RelInventarioUbicacionArrendamiento> relInventarioUbicacion = ctx.RelInventarioUbicacionArrendamientos.Where(x => x.RelClienteUbicacionOficinaArrendamientoId == id && x.Estatus == true).ToList();

            if (relInventarioUbicacion.Count > 0)
            { asignados = true; }

            return asignados;
        }
    }
}
