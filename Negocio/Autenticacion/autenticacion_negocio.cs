using Azure;
using Data.Models;
using Email;
using Entidades_complejas;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class autenticacion_negocio
    {
        private PmsInventarioContext ctx = new PmsInventarioContext();
        private Security SS = new Security();
        private Notificacion email = new Notificacion();
        private IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();

        public TipoAccion Get(usuario_login_complex login)
        {
            try
            {
                string usuarioEn = SS.Encriptar(login.Usuario);
                usuarioEn = SS.Base64Encode(usuarioEn);
                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Usuario == login.Usuario && x.Password == login.Password && x.Activo == true).FirstOrDefault();

                if (usuario == null)
                { throw new Exception("No se encontraron los acccesos, favor de validar."); }
                else
                { 
                    usuario_app_complex usuario_ = new usuario_app_complex();
                    usuario_.Nombres = usuario.Nombres;
                    usuario_.Apellidos = usuario.Apellidos;
                    usuario_.Usuario = usuario.Usuario;
                    
                    return TipoAccion.Positiva(usuario_); 
                
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion GetResset(string id)
        {
            try
            {
                int idU = Convert.ToInt32(SS.DesencriptarId(SS.Base64Decode(id)));
                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idU && x.Activo == true).FirstOrDefault();

                if (usuario == null)
                { throw new Exception("Entidad no encontrada, favor de validar."); }
                else
                {
                    usuario_app_complex usuario_ = new usuario_app_complex();
                    usuario_.Id = usuario.Id;
                    usuario_.Nombres = usuario.Nombres;
                    usuario_.Apellidos = usuario.Apellidos;
                    usuario_.Usuario = usuario.Usuario;

                    return TipoAccion.Positiva(usuario_);

                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Reset(usuario_login_complex login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.Usuario))
                    throw new Exception("Favor de indicar el correo.");

                string correo = SS.Desencriptar(login.Usuario);

                UsuariosApp _usuario = ctx.UsuariosApps.Where(x => x.Usuario.ToLower() == correo.ToLower() && x.Activo == true).FirstOrDefault();
                if (_usuario == null)
                    throw new Exception("No existe el usuario solicitado, favor de validar.");

                string url = Configuration.GetSection("Redireccion").GetValue<string>("ResetPassword");
                string id = SS.EncriptarId(_usuario.Id.ToString());

                usuario_app_complex usuario_ = new usuario_app_complex();
                usuario_.Nombres = _usuario.Nombres;
                usuario_.Apellidos = _usuario.Apellidos;
                usuario_.Usuario = _usuario.Usuario;
                usuario_.Password = url + SS.Base64Encode(id);

                bool notifica = email.Reset(usuario_);

                if (notifica)
                { return TipoAccion.Positiva("Notificación enviada con éxito.", _usuario.Id); }
                else
                { throw new Exception("Ocurrio un error al enviar la notificación, intente más tarde."); }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }


        public TipoAccion UpdatePassword(usuario_app_complex entidad)
        {
            try
            {
                if(string.IsNullOrEmpty(entidad.Password))
                    throw new Exception("La contraseña no puede venir vacía, favor de validar.");

                UsuariosApp _usuario = ctx.UsuariosApps.Where(x => x.Id == entidad.Id && x.Activo == true).FirstOrDefault();
                if (_usuario == null)
                    throw new Exception("No existe el usuario solicitado, favor de validar.");

                _usuario.Password = SS.Encriptar(entidad.Password);

                ctx.Entry(_usuario).State = EntityState.Modified;
                ctx.SaveChanges();


                return TipoAccion.Positiva("Registro actualizado con éxito.", _usuario.Id);
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }            
        }


    }
}
