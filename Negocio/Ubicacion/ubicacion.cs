using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Ubicacion
{
    public class ubicacion_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion? Respuesta { get; set; }
        public tbl_ubicacion_complex? ubicacion { get; set; }

        public ubicacion_negocio(tbl_ubicacion_complex input, ActionAdd add)
        {
            try
            { 
                this.ubicacion = input;
                TblClienteUbicacion addUbicacion = new TblClienteUbicacion();
                addUbicacion.CatClienteId = this.ubicacion.CatClienteId;
                addUbicacion.Direccion = this.ubicacion.Direccion;
                addUbicacion.Edificio = this.ubicacion.Edificio;
                addUbicacion.Piso = this.ubicacion.Piso;
                addUbicacion.Plano = this.ubicacion.Plano;
                addUbicacion.Estatus = true;
                addUbicacion.Inclusion =DateTime.Now;

                ctx.TblClienteUbicacions.Add(addUbicacion);
                ctx.SaveChanges();

                this.Respuesta = TipoAccion.Positiva("Alta Exitosa", addUbicacion.Id);
            }
            catch(Exception ex) 
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
            
        }
        public ubicacion_negocio(tbl_ubicacion_complex input, ActionUpdate update)
        {
            try
            {
                this.ubicacion = input;
                //Valida que exista el registro
                TblClienteUbicacion tblUbicacion = ctx.TblClienteUbicacions.Where(x => x.Id == this.ubicacion.Id).FirstOrDefault();

                if(tblUbicacion == null)
                { throw new Exception("No existe el registro en TblClienteUbicacions, favor de validar."); }
                else
                {
                    tblUbicacion.Direccion = this.ubicacion.Direccion;
                    tblUbicacion.Edificio = this.ubicacion.Edificio;
                    tblUbicacion.Piso = this.ubicacion.Piso;
                    tblUbicacion.Plano = this.ubicacion.Plano;

                    ctx.TblClienteUbicacions.Update(tblUbicacion);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se actualizo registro exitosamente.", tblUbicacion.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public ubicacion_negocio(int id, ActionDisable disable)
        {
            try
            {
                //Valida que exista el registro
                TblClienteUbicacion tblUbicacion = ctx.TblClienteUbicacions.Where(x => x.Id == id).FirstOrDefault();

                if (tblUbicacion == null)
                { throw new Exception("No existe el registro en TblClienteUbicacions, favor de validar."); }
                else
                {
                    //Actualiza registro
                    tblUbicacion.Estatus = false;

                    ctx.TblClienteUbicacions.Update(tblUbicacion);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se inhabilito registro exitosamente.", tblUbicacion.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

    }
}
