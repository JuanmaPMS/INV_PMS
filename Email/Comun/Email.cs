﻿using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Email
{
    public class Email
    {
        private IConfiguration Configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();
        private string[] destinatarios;
        private string[] destinatariosCC;
        private string[] destinatariosBCC;
        private string[] adjuntos;
        private string asunto;

        public Email(string _asunto, string[] _destinatarios, string[] _destinatariosCC = null, string[] _destinatariosBCC = null, string[] _adjuntos = null)
        {
            destinatarios = _destinatarios;
            destinatariosCC = _destinatariosCC;
            destinatariosBCC = _destinatariosBCC;
            adjuntos = _adjuntos;
            asunto = _asunto;
        }

        //public MemoryStream Archivo_Adjunto
        //{
        //    get { return archivo_adjunto; }
        //    set { archivo_adjunto = value; }
        //}

        public void EnviarEmail(AlternateView _vistaEmail, out bool _status, out string _mensaje)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                Configuracion config = Configuration.GetSection("EmailConfiguration").Get<Configuracion>();

                message.From = new MailAddress(config.From, config.DisplayName, Encoding.UTF8);
                message.Subject = asunto;
                message.SubjectEncoding = Encoding.UTF8;
                message.AlternateViews.Add(_vistaEmail);
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;

                if (destinatarios != null)
                {
                    foreach (string correo in destinatarios)
                    {
                        message.To.Add(correo);
                    }
                }
                if (destinatariosCC != null)
                {

                    foreach (string correo in destinatariosCC)
                    {
                        message.CC.Add(correo);
                    }
                }
                if (destinatariosBCC != null)
                {
                    foreach (string correo in destinatariosBCC)
                    {
                        message.Bcc.Add(correo);
                    }
                }
                if (adjuntos != null)
                {
                    foreach (string path in adjuntos)
                    {
                        message.Attachments.Add(new Attachment(path));
                    }
                }

                smtp.Host = config.SmtpServer;
                smtp.Port = config.Port;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                //if (ConfigurationManager.AppSettings["CREDENTIAL_ACCOUNT"] != null && ConfigurationManager.AppSettings["CREDENTIAL_ACCOUNT"] != string.Empty &&
                //    ConfigurationManager.AppSettings["CREDENTIAL_PASSWORD"] != null && ConfigurationManager.AppSettings["CREDENTIAL_PASSWORD"] != string.Empty)
                //{
                //    smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["CREDENTIAL_ACCOUNT"], ConfigurationManager.AppSettings["CREDENTIAL_PASSWORD"]);
                //}
                //else
                //{
                //    smtp.Credentials = CredentialCache.DefaultNetworkCredentials;
                //}
                smtp.Credentials = new NetworkCredential(config.From, config.Password);
                try
                {
                    smtp.Send(message);
                    message.Dispose();
                    _status = true;
                    _mensaje = "CORREO ELECTRONICO ENVIADO CORRECTAMENTE";
                }
                catch (SmtpException ex)
                {
                    _status = false;
                    _mensaje = ex.Message;
                }
            }
            catch (Exception ex)
            {
                _status = false;
                _mensaje = ex.Message;
            }
        }
    }
}
