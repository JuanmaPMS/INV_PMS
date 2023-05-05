using Data.Models;
using Entidades_complejas;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Usuario
{
    public class usuario_correo_adicional_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion? Respuesta { get; set; }
        public RelUsuarioCorreoAdicional relUsuarioCorreoAdicional { get; set; }

        public usuario_correo_adicional_negocio()
        { }

        public List<RelUsuarioCorreoAdicional> obtener()
        {
            var listaCorreos = ctx.RelUsuarioCorreoAdicionals.Include(x => x.CatUsuario.Estatus==true).ToList();
            return listaCorreos;
        }


        public usuario_correo_adicional_negocio(RelUsuarioCorreoAdicional input, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    this.relUsuarioCorreoAdicional = input;
                    RelUsuarioCorreoAdicional relUsuarioCorreoAd = new RelUsuarioCorreoAdicional();
                    relUsuarioCorreoAd.CatUsuarioId = (int)this.relUsuarioCorreoAdicional.CatUsuarioId!;
                    relUsuarioCorreoAd.Correo = this.relUsuarioCorreoAdicional.Correo!;
                    relUsuarioCorreoAd.Estatus = this.relUsuarioCorreoAdicional.Estatus!;
                    relUsuarioCorreoAd.Inclusion = DateTime.Now;

                    ctx.RelUsuarioCorreoAdicionals.Add(relUsuarioCorreoAd);
                    ctx.SaveChanges();

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa", relUsuarioCorreoAd.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }

            }
        }


        public usuario_correo_adicional_negocio(RelUsuarioCorreoAdicional input, ActionUpdate update)
        {
            try
            {
                RelUsuarioCorreoAdicional relUsuarioCorreoAd = ctx.RelUsuarioCorreoAdicionals.Where(x => x.Id == input.Id).FirstOrDefault();
                if (relUsuarioCorreoAd == null)
                { throw new Exception("No existe el registro, favor de validar."); }
                else
                {
                    relUsuarioCorreoAd.CatUsuarioId = (int)input.CatUsuarioId!;
                    relUsuarioCorreoAd.Correo = input.Correo!;
                    relUsuarioCorreoAd.Estatus = input.Estatus!;
                    relUsuarioCorreoAd.Inclusion = DateTime.Now;


                    ctx.RelUsuarioCorreoAdicionals.Update(relUsuarioCorreoAd);
                    ctx.SaveChanges();
                }

                this.Respuesta = TipoAccion.Positiva("Actualización Exitosa", relUsuarioCorreoAd.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }



        public usuario_correo_adicional_negocio(int id, ActionDisable update)
        {
            try
            {
                RelUsuarioCorreoAdicional relUsuarioCorreoAd = ctx.RelUsuarioCorreoAdicionals.Where(x => x.Id == id).FirstOrDefault();
                if (relUsuarioCorreoAd == null)
                { throw new Exception("No existe el registro, favor de validar."); }
                else
                {
                    relUsuarioCorreoAd.Estatus = false;
                    relUsuarioCorreoAd.Inclusion = DateTime.Now;

                    ctx.RelUsuarioCorreoAdicionals.Update(relUsuarioCorreoAd);
                    ctx.SaveChanges();
                }

                this.Respuesta = TipoAccion.Positiva("Eliminación Exitosa", relUsuarioCorreoAd.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }



    }
}
