using Data.Models;
using Entidades_complejas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Catalogo
{
    public class familia_articulo_falla_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion? Respuesta { get; set; }
        public cat_familia_articulo_falla_complex FamiliaArticuloFalla { get; set; }

        public familia_articulo_falla_negocio() { }

        public List<CatFamiliaArticuloFalla> obtener(int id)
        {
            var listaFallas = ctx.CatFamiliaArticuloFallas.Where(x => x.CatFamiliaArticuloId == id)
                .ToList();

            return listaFallas;
        }


        public familia_articulo_falla_negocio(cat_familia_articulo_falla_complex input, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    this.FamiliaArticuloFalla = input;
                    CatFamiliaArticuloFalla catFamiliaArtFalla = new CatFamiliaArticuloFalla();
                    catFamiliaArtFalla.CatFamiliaArticuloId = (int)this.FamiliaArticuloFalla.CatFamiliaArticuloId!;
                    catFamiliaArtFalla.Falla = this.FamiliaArticuloFalla.Falla!.ToUpper();
                    catFamiliaArtFalla.Descripcion = this.FamiliaArticuloFalla.Descripcion!.ToUpper();
                    catFamiliaArtFalla.Estatus = true;
                    catFamiliaArtFalla.Inclusion = DateTime.Now;

                    ctx.CatFamiliaArticuloFallas.Add(catFamiliaArtFalla);
                    ctx.SaveChanges();

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa", catFamiliaArtFalla.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }

            }
        }

        public familia_articulo_falla_negocio(cat_familia_articulo_falla_complex input, ActionUpdate add)
        {
            try
            {

                this.FamiliaArticuloFalla = input;
                CatFamiliaArticuloFalla catFamiliaArtFalla = ctx.CatFamiliaArticuloFallas.Where(x => x.Id == input.Id).FirstOrDefault();
                if (catFamiliaArtFalla == null)
                { throw new Exception("No existe el registro, favor de validar."); }
                else
                {
                    catFamiliaArtFalla.CatFamiliaArticuloId = (int)this.FamiliaArticuloFalla.CatFamiliaArticuloId!;
                    catFamiliaArtFalla.Falla = this.FamiliaArticuloFalla.Falla!.ToUpper();
                    catFamiliaArtFalla.Descripcion = this.FamiliaArticuloFalla.Descripcion!.ToUpper();
                    catFamiliaArtFalla.Inclusion = DateTime.Now;


                    ctx.CatFamiliaArticuloFallas.Update(catFamiliaArtFalla);
                    ctx.SaveChanges();
                }

                this.Respuesta = TipoAccion.Positiva("Actualización Exitosa", catFamiliaArtFalla.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public familia_articulo_falla_negocio(int id, ActionDisable disable)
        {
            try
            {
                CatFamiliaArticuloFalla catFamiliaArtFalla = ctx.CatFamiliaArticuloFallas.Where(x => x.Id == id).FirstOrDefault();
                if (catFamiliaArtFalla == null)
                { throw new Exception("No existe el registro, favor de validar."); }
                else
                {
                    catFamiliaArtFalla.Estatus = false;
                    catFamiliaArtFalla.Inclusion = DateTime.Now;

                    ctx.CatFamiliaArticuloFallas.Update(catFamiliaArtFalla);
                    ctx.SaveChanges();
                }

                this.Respuesta = TipoAccion.Positiva("Eliminación Exitosa", catFamiliaArtFalla.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }


    }
}
