using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Entidades_complejas;

namespace Email
{
    public class Notificacion
    {
        public bool Reset(usuario_app_complex body)
        {
            bool status = false;
            try
            {
                string mensaje = string.Empty;
                string _asunto = "Reestablecer Contraseña";
                string[] Destinatarios = { body.Usuario };
                string[] DestinatariosCC = new string[0];
                string[] adjuntos = new string[0];


                Email oEmail = new Email(_asunto, Destinatarios, DestinatariosCC, _adjuntos: adjuntos);
                oEmail.EnviarEmail(GetViewReset(body), out status, out mensaje);
            }
            catch
            {
                status = false;
            }
            return status;
        }

        private AlternateView GetViewReset(usuario_app_complex model)
        {
            AlternateView vistaEmail;
            string plantilla = string.Empty;
            plantilla = "/Plantillas/ResetPassword.html";

            //Body
            string mailBody = string.Empty;
            StreamReader format = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + plantilla);
            mailBody = format.ReadToEnd();

            if (!string.IsNullOrEmpty(model.Nombres))
                mailBody = mailBody.Replace("@@Nombre@@", model.Nombres);
            if (!string.IsNullOrEmpty(model.Password))
                mailBody = mailBody.Replace("@@Password@@", model.Password);

            format.Close();
            //Fin Body

            //LinkedResource imageLogo = new LinkedResource(AppDomain.CurrentDomain.BaseDirectory + "/Plantillas/img/logoGR.PNG", "image/jpeg");
            //imageLogo.ContentId = "logo";

            vistaEmail = AlternateView.CreateAlternateViewFromString(mailBody, Encoding.UTF8, "text/html");
            //vistaEmail.LinkedResources.Add(imageLogo);

            return vistaEmail;
        }
    }
}
