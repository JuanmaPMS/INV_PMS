using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Mantenimiento
{
    public class mantenimiento_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public tbl_mantenimiento_complex mantenimientoInventario { get; set; }
        public TipoAccion Respuesta { get; set; }

        public mantenimiento_negocio() { }

        public mantenimiento_negocio(tbl_mantenimiento_complex input, ActionAdd add)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    this.mantenimientoInventario = input;
                    TblMantenimientoInventario tblMantenimientoInv = new TblMantenimientoInventario();
                    tblMantenimientoInv.RelUsuarioInventarioId = (int)this.mantenimientoInventario.RelUsuarioInventarioId!;
                    tblMantenimientoInv.Inclusion = this.mantenimientoInventario.Inclusion!;

                    ctx.TblMantenimientoInventarios.Add(tblMantenimientoInv);
                    ctx.SaveChanges();

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Alta Exitosa", tblMantenimientoInv.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }

            }
        }

        public mantenimiento_negocio(tbl_mantenimiento_complex input, ActionUpdate update)
        {
            try
            {
                TblMantenimientoInventario mantenimientoInventario = null;//ctx.TblMantenimientoInventarios.Where(x => x.Id == input.Id).FirstOrDefault();
                if (mantenimientoInventario == null)
                { throw new Exception("No existe el registro, favor de validar."); }
                else
                {
                    mantenimientoInventario.RelUsuarioInventarioId = (int)input.RelUsuarioInventarioId!;
                    mantenimientoInventario.Inclusion = input.Inclusion!;


                    ctx.TblMantenimientoInventarios.Update(mantenimientoInventario);
                    ctx.SaveChanges();
                }

                this.Respuesta = TipoAccion.Positiva("Actualización Exitosa", mantenimientoInventario.Id);
            }
            catch (Exception ex)
            {
                this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }


        public List<VwMantenimientoInventario> todos()
        {
            return ctx.VwMantenimientoInventarios
                .Where(x => x.Ultimomantenimiento < DateTime.Now && x.Ultimomantenimiento > DateTime.Now.AddMonths(-11))
            .ToList();
        }


        public mantenimiento_negocio(bool esPorCorreo)
        {
            var enviarporcorreo = ctx.VwMantenimientoInventarios
               .Where(x => x.Ultimomantenimiento < DateTime.Now && x.Ultimomantenimiento > DateTime.Now.AddMonths(-11))
               .ToList();
            if (enviarporcorreo.Count == 0)
            {
                this.Respuesta = TipoAccion.Positiva("No existe información por enviar", 0);
                return;
            }

            var listaNotificaciones = ctx.TblMantenimientoNotificacions.Where(x => x.Activo == true).Select(x => x.Correo).ToArray();
            if (listaNotificaciones.Count() == 0)
            {
                this.Respuesta = TipoAccion.Positiva("No existen destinatarios configurados", 0);
                return;
            }

            string plantilla = "/Plantillas/ListaMantenimiento.html";
            StreamReader format = new StreamReader(Environment.CurrentDirectory + plantilla);
            string mailBody = format.ReadToEnd();
            string shtml = string.Empty;

            StringBuilder sb = new StringBuilder();
            sb.Append("Listado de Mantenimiento");
            sb.Append("\r\n");
            sb.Append("\r\n");
            sb.Append("Nombre,");
            sb.Append("Correo,");
            sb.Append("Fabricante,");
            sb.Append("Modelo,");
            sb.Append("Caracteristicas,");
            sb.Append("No Serie,");
            sb.Append("\r\n");


            foreach (var item in enviarporcorreo)
            {
                shtml.Concat("<tr><td>");
                sb.Append(item.Nombreusuario);
                shtml.Concat(item.Nombreusuario + "</td><td>");
                sb.Append(",");
                sb.Append(item.Correousuario);
                shtml.Concat(item.Correousuario + "</td><td>");
                sb.Append(",");
                sb.Append(item.Fabricante.Replace(",", " "));
                shtml.Concat(item.Fabricante + "</td><td>");
                sb.Append(",");
                sb.Append(item.Modelo);
                shtml.Concat(item.Modelo + "</td><td>");
                sb.Append(",");
                sb.Append(item.Caracteristicas.Replace(",", " "));
                shtml.Concat(item.Caracteristicas + "</td><td>");
                sb.Append(",");
                sb.Append(item.Numerodeserie);
                shtml.Concat(item.Numerodeserie + "</td></tr>");
                sb.Append("\r\n");
            }

            mailBody.Replace(" @@FilasManto@@", shtml);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "ListaMantenimiento.csv", sb.ToString());

            Email.Email email = new Email.Email("Mantenimiento de inventario",
                listaNotificaciones, _adjuntos: new string[] { AppDomain.CurrentDomain.BaseDirectory + "ListaMantenimiento.csv" });

            bool status;
            string mensaje;
            email.EnviarEmail(System.Net.Mail.AlternateView.CreateAlternateViewFromString(mailBody, Encoding.UTF8, "text/html"), out status, out mensaje);

            if (status)
                this.Respuesta = TipoAccion.Positiva(mensaje, enviarporcorreo.Count());
            else
                this.Respuesta = TipoAccion.Negativa("No se envio la lista de mantenimiento: " + mensaje, 0);

        }
    }
}
