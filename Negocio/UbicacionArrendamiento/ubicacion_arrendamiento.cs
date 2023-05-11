using Data.Models;
using Entidades_complejas;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.UbicacionArrendamiento
{
    public class ubicacion_arrendamiento_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion? Respuesta { get; set; }
        public tbl_ubicacion_arrendamiento_complex? ubicacion { get; set; }

        public ubicacion_arrendamiento_negocio(tbl_ubicacion_arrendamiento_complex input, ActionAdd add)
        {
            try
            {
                this.ubicacion = input;
                TblClienteUbicacionArrendamiento addUbicacion = new TblClienteUbicacionArrendamiento();
                addUbicacion.CatClienteId = this.ubicacion.CatClienteId;
                addUbicacion.Direccion = this.ubicacion.Direccion;
                addUbicacion.Edificio = this.ubicacion.Edificio;
                addUbicacion.Piso = this.ubicacion.Piso;
                addUbicacion.Plano = this.ubicacion.Plano;
                addUbicacion.Estatus = true;
                addUbicacion.Inclusion = DateTime.Now;

                ctx.TblClienteUbicacionArrendamientos.Add(addUbicacion);
                ctx.SaveChanges();

                this.Respuesta = TipoAccion.Positiva("Alta Exitosa", addUbicacion.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }

        }
        public ubicacion_arrendamiento_negocio(tbl_ubicacion_arrendamiento_complex input, ActionUpdate update)
        {
            try
            {
                this.ubicacion = input;
                //Valida que exista el registro
                TblClienteUbicacionArrendamiento tblUbicacion = ctx.TblClienteUbicacionArrendamientos.Where(x => x.Id == this.ubicacion.Id).FirstOrDefault();

                if (tblUbicacion == null)
                { throw new Exception("No existe el registro en TblClienteUbicacions, favor de validar."); }
                else
                {
                    tblUbicacion.Direccion = this.ubicacion.Direccion;
                    tblUbicacion.Edificio = this.ubicacion.Edificio;
                    tblUbicacion.Piso = this.ubicacion.Piso;
                    tblUbicacion.Plano = this.ubicacion.Plano;

                    ctx.TblClienteUbicacionArrendamientos.Update(tblUbicacion);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se actualizo registro exitosamente.", tblUbicacion.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public ubicacion_arrendamiento_negocio(int id, ActionDisable disable)
        {
            try
            {
                //Valida que exista el registro
                TblClienteUbicacionArrendamiento tblUbicacion = ctx.TblClienteUbicacionArrendamientos.Where(x => x.Id == id).FirstOrDefault();

                if (tblUbicacion == null)
                { throw new Exception("No existe el registro en TblClienteUbicacionArrendamientos, favor de validar."); }
                else
                {
                    //Actualiza registros relacionados
                    List<RelClienteUbicacionOficinaArrendamiento> relUbicacionOficina = ctx.RelClienteUbicacionOficinaArrendamientos.Where(x => x.TblClienteUbicacionArrendamientoId == id).ToList();

                    relUbicacionOficina.ForEach(x => x.Estatus = false);
                    ctx.SaveChanges();

                    foreach (RelClienteUbicacionOficinaArrendamiento Oficina in relUbicacionOficina)
                    {
                        List<RelInventarioUbicacionArrendamiento> relInventarioUbicacion = ctx.RelInventarioUbicacionArrendamientos.Where(x => x.RelClienteUbicacionOficinaArrendamientoId == Oficina.Id).ToList();

                        if (relInventarioUbicacion.Count > 0)
                        {
                            relInventarioUbicacion.ForEach(x => x.Estatus = false);
                            ctx.SaveChanges();

                        }
                    }


                    //Actualiza registro
                    tblUbicacion.Estatus = false;

                    ctx.TblClienteUbicacionArrendamientos.Update(tblUbicacion);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se inhabilito registro exitosamente.", tblUbicacion.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public ubicacion_arrendamiento_negocio()
        { }

        public bool ValidaAsignados(int id)
        {
            bool asignados = false;

            TblClienteUbicacionArrendamiento tblUbicacion = ctx.TblClienteUbicacionArrendamientos.Where(x => x.Id == id).FirstOrDefault();

            List<RelClienteUbicacionOficinaArrendamiento> relUbicacionOficina = ctx.RelClienteUbicacionOficinaArrendamientos.Where(x => x.TblClienteUbicacionArrendamientoId == id && x.Estatus == true).ToList();

            foreach (RelClienteUbicacionOficinaArrendamiento Oficina in relUbicacionOficina)
            {
                List<RelInventarioUbicacionArrendamiento> relInventarioUbicacion = ctx.RelInventarioUbicacionArrendamientos.Where(x => x.RelClienteUbicacionOficinaArrendamientoId == Oficina.Id && x.Estatus == true).ToList();

                if (relInventarioUbicacion.Count > 0)
                { asignados = true; }
            }

            return asignados;
        }

        public TipoAccion Get(int? id)
        {
            try
            {
                List<TblClienteUbicacionArrendamiento> ubicaciones = id == null ? ctx.TblClienteUbicacionArrendamientos.Where(x => x.Estatus == true).ToList()
                                                : ctx.TblClienteUbicacionArrendamientos
                                                .Include(o => o.RelClienteUbicacionOficinaArrendamientos.Where(x => x.Estatus == true)).Where(x => x.Id == id).ToList();
                if (ubicaciones.Count == 0)
                { throw new Exception("No existen registros en TblClienteUbicacions"); }
                else
                {
                    List<tbl_ubicacion_arrendamiento_complex> full = JsonConvert.DeserializeObject<List<tbl_ubicacion_arrendamiento_complex>>(JsonConvert.SerializeObject(ubicaciones, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }))!;

                    full.ForEach(x => x.Cliente = ctx.CatClientes.Where(c => c.Id == x.CatClienteId).FirstOrDefault().Nombre);

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
