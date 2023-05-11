using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ubicacion_oficina_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion? Respuesta { get; set; }
        public rel_ubicacion_oficina_complex? oficina { get; set; }

        public ubicacion_oficina_negocio(rel_ubicacion_oficina_complex input, ActionAdd add)
        {
            try
            {
                this.oficina = input;
                RelClienteUbicacionOficina addOficina = new RelClienteUbicacionOficina();
                addOficina.TblClienteUbicacionId = this.oficina.TblClienteUbicacionId;
                addOficina.Nombre = this.oficina.Nombre;
                addOficina.EjeX = this.oficina.EjeX;
                addOficina.EjeY = this.oficina.EjeY;
                addOficina.Alto = this.oficina.Alto;
                addOficina.Ancho = this.oficina.Ancho;
                addOficina.Estatus = true;
                addOficina.Inclusion = DateTime.Now;

                ctx.RelClienteUbicacionOficinas.Add(addOficina);
                ctx.SaveChanges();

                this.Respuesta = TipoAccion.Positiva("Alta Exitosa", addOficina.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }

        }
        public ubicacion_oficina_negocio(rel_ubicacion_oficina_complex input, ActionUpdate update)
        {
            try
            {
                this.oficina = input;
                //Valida que exista el registro
                RelClienteUbicacionOficina relUbicacionOficina = ctx.RelClienteUbicacionOficinas.Where(x => x.Id == this.oficina.Id).FirstOrDefault();

                if (relUbicacionOficina == null)
                { throw new Exception("No existe el registro en RelClienteUbicacionOficinas, favor de validar."); }
                else
                {
                    relUbicacionOficina.Nombre = this.oficina.Nombre;
                    relUbicacionOficina.EjeX = this.oficina.EjeX;
                    relUbicacionOficina.EjeY = this.oficina.EjeY;
                    relUbicacionOficina.Alto = this.oficina.Alto;
                    relUbicacionOficina.Ancho = this.oficina.Ancho;

                    ctx.RelClienteUbicacionOficinas.Update(relUbicacionOficina);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se actualizo registro exitosamente.", relUbicacionOficina.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public ubicacion_oficina_negocio(int id, ActionDisable disable)
        {
            try
            {
                //Valida que exista el registro
                RelClienteUbicacionOficina relUbicacionOficina = ctx.RelClienteUbicacionOficinas.Where(x => x.Id == id).FirstOrDefault();

                if (relUbicacionOficina == null)
                { throw new Exception("No existe el registro en RelClienteUbicacionOficinas, favor de validar."); }
                else
                {
                    //Elimina inventario asignado
                    List<RelInventarioUbicacion> relInventarioUbicacion = ctx.RelInventarioUbicacions.Where(x => x.RelClienteUbicacionOficinaId == id).ToList();

                    if (relInventarioUbicacion.Count > 0)
                    {
                        relInventarioUbicacion.ForEach(x => x.Estatus = false);
                        ctx.SaveChanges();
                    }

                    //Actualiza registro
                    relUbicacionOficina.Estatus = false;

                    ctx.RelClienteUbicacionOficinas.Update(relUbicacionOficina);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se inhabilito registro exitosamente.", relUbicacionOficina.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public ubicacion_oficina_negocio()
        { }

        public bool ValidaAsignados(int id)
        {
            bool asignados = false;

            List<RelInventarioUbicacion> relInventarioUbicacion = ctx.RelInventarioUbicacions.Where(x => x.RelClienteUbicacionOficinaId == id && x.Estatus == true).ToList();

            if (relInventarioUbicacion.Count > 0)
            { asignados = true; }

            return asignados;
        }

    }
}
