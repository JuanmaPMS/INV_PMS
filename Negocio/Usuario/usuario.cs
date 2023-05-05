using Data.Models;
using Entidades_complejas;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class usuario_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion? Respuesta { get; set; }
        public cat_usuario_complex? usuario { get; set; }

        public usuario_negocio()
        { }

        public TipoAccion Get(int? id)
        {
            try
            {
                List<CatUsuario> usuarios = id == null ? ctx.CatUsuarios.Where(x => x.Estatus == true).ToList()
                                                : ctx.CatUsuarios.Where(x => x.Id == id).ToList();
                if (usuarios.Count == 0)
                { throw new Exception("No existen registros en Cat_Usuario"); }
                else
                {
                    List<cat_usuario_complex> full = JsonConvert.DeserializeObject<List<cat_usuario_complex>>(JsonConvert.SerializeObject(usuarios, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }))!;

                    return TipoAccion.Positiva(full);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public usuario_negocio(cat_usuario_complex input, ActionAdd add)
        {
            try
            {
                this.usuario = input;
                CatUsuario addUser = new CatUsuario();
                addUser.Nombre = this.usuario.Nombre;
                addUser.Cuenta = this.usuario.Cuenta;
                addUser.Correo = this.usuario.Correo;
                addUser.Ubicacion = this.usuario.Ubicacion;
                addUser.Estatus = true;
                addUser.Inclusion = DateTime.Now;

                ctx.CatUsuarios.Add(addUser);
                ctx.SaveChanges();

                this.Respuesta = TipoAccion.Positiva("Alta Exitosa", addUser.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }

        }
        public usuario_negocio(cat_usuario_complex input, ActionUpdate update)
        {
            try
            {
                this.usuario = input;
                //Valida que exista el registro
                CatUsuario catUsuario = ctx.CatUsuarios.Where(x => x.Id == this.usuario.Id).FirstOrDefault();

                if (catUsuario == null)
                { throw new Exception("No existe el registro en Cat_Usuario, favor de validar."); }
                else
                {
                    catUsuario.Nombre = this.usuario.Nombre;
                    catUsuario.Cuenta = this.usuario.Cuenta;
                    catUsuario.Correo = this.usuario.Correo;
                    catUsuario.Ubicacion = this.usuario.Ubicacion;

                    ctx.CatUsuarios.Update(catUsuario);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se actualizo registro exitosamente.", catUsuario.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public usuario_negocio(int id, ActionDisable disable)
        {
            try
            {
                //Valida que exista el registro
                CatUsuario catUsuario = ctx.CatUsuarios.Where(x => x.Id == id).FirstOrDefault();

                if (catUsuario == null)
                { throw new Exception("No existe el registro en Cat_Usuario, favor de validar."); }
                else
                {
                    //Actualiza registro
                    catUsuario.Estatus = false;

                    ctx.CatUsuarios.Update(catUsuario);
                    ctx.SaveChanges();

                    this.Respuesta = TipoAccion.Positiva("Se inhabilito registro exitosamente.", catUsuario.Id);
                }
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }


    }
}
