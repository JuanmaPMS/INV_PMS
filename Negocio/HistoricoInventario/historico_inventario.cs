using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.HistoricoInventario
{
    public class historico_inventario_negocio
    {
        public static PmsInventarioContext ctx = new PmsInventarioContext();
        

        public static void CapturaNumeroDeSerie(int idUsuario, VwInventario inventario, string numeroSerie)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario == null)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }

                if (numeroSerie == null)
                {
                    throw new Exception("No se ha proporcionado el numero de serie");
                }



                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();
                //var inventario_ = ctx.VwInventarios.Where(x => x.Idinventario == inventario.Idinventario).FirstOrDefault();

                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha modificado el número de serie a: " + numeroSerie +
                " del producto: " + inventario.Modelo + ".";


                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Captura número de serie";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
               // this.Respuesta = TipoAccion.Positiva("Número de serie exitoso.", historico.Id);
            }
            catch (Exception ex)
            {

               // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }


        public static void CapturaClaveInventario(int idUsuario, VwInventario inventario, string claveInventario)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario.Idinventario == 0)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }

                if (claveInventario == null)
                {
                    throw new Exception("No se ha proporcionado la clave");
                }

                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();
                

                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha modificado la clave de inventario a: " + claveInventario +
                " del producto: " + inventario.Modelo + ".";


                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Captura clave inventario";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
               // this.Respuesta = TipoAccion.Positiva("Clave inventario exitoso.", historico.Id);
            }
            catch (Exception ex)
            {

               // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public static void CapturaAccesorioInventario(int idUsuario, VwInventario inventario, string accesorio)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario.Idinventario == 0)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }

                if (accesorio == null)
                {
                    throw new Exception("No se ha proporcionado la clave");
                }

                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();
               

                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha agregado el accesorio: " + accesorio +
                " al producto: " + inventario.Modelo + ".";


                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Captura accesorio";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
               // this.Respuesta = TipoAccion.Positiva("Accesorio exitoso.", historico.Id);
            }
            catch (Exception ex)
            {

               // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }


        public static void ModificaAccesorioInventario(int idUsuario, VwInventario inventario, string accesorioviejo, string accesorionuevo)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario.Idinventario == 0)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }

                if (accesorioviejo == null)
                {
                    throw new Exception("No se ha proporcionado el accesorio");
                }

                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();


                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha modificado el accesorio: " + accesorioviejo + " a: " + accesorionuevo +
                " del producto: " + inventario.Modelo + ".";


                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Modificación accesorio";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
                // this.Respuesta = TipoAccion.Positiva("Accesorio exitoso.", historico.Id);
            }
            catch (Exception ex)
            {

                // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public static void eliminaAccesorioInventario(int idUsuario, VwInventario inventario, string accesorio)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario.Idinventario == 0)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }

                if (accesorio == null)
                {
                    throw new Exception("No se ha proporcionado la clave");
                }

                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();
               

                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha eliminado el accesorio: " + accesorio +
                " al producto: " + inventario.Modelo + ".";


                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Eliminación accesorio";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
               // this.Respuesta = TipoAccion.Positiva("Eliminación exitosa.", historico.Id);
            }
            catch (Exception ex)
            {

               // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public static void EliminaInventario(int idUsuario, VwInventario inventario)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario.Idinventario == 0)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }




                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();
               

                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha eliminado el producto: " + inventario.Modelo + ".";


                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Eliminación inventario";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
               // this.Respuesta = TipoAccion.Positiva("Eliminación exitosa.", historico.Id);
            }
            catch (Exception ex)
            {

               // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }


        public static void AsignacionInventario(int idUsuario, VwInventario inventario, CatUsuario usuarioAsignado)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario.Idinventario == 0)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }

                if (usuarioAsignado == null)
                {
                    throw new Exception("No se ha proporcionado el usuario asignado");
                }


                //CatUsuario usuarioAsignado = ctx.CatUsuarios.Where(x => x.Id == idUsuarioAsignado).FirstOrDefault();
                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();
               

                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha asignado el producto: " + inventario.Modelo + " con número de serie: "
                    + inventario.Numerodeserie + " a " + usuarioAsignado.Nombre + ".";


                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Asignación inventario";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
               // this.Respuesta = TipoAccion.Positiva("Eliminación exitosa.", historico.Id);
            }
            catch (Exception ex)
            {

               // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public static void CartaResponsivaAsignacionInventario(int idUsuario, VwInventario inventario)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario.Idinventario == 0)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }



                //CatUsuario usuarioAsignado = ctx.CatUsuarios.Where(x => x.Id == idUsuarioAsignado).FirstOrDefault();
                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();


                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha cargado la carta responsiva el producto: " + inventario.Modelo + " con número de serie: "
                    + inventario.Numerodeserie;


                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Carga carta responsiva";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
                // this.Respuesta = TipoAccion.Positiva("Eliminación exitosa.", historico.Id);
            }
            catch (Exception ex)
            {

                // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public static void DesasignacionInventario(int idUsuario, VwInventario inventario)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario.Idinventario == 0)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }

                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();
               

                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha desasignado el producto: " + inventario.Modelo + " con número de serie: "
                    + inventario.Numerodeserie + ".";

                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Desasignación inventario";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
               // this.Respuesta = TipoAccion.Positiva("Eliminación exitosa.", historico.Id);
            }
            catch (Exception ex)
            {

               // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }



        public static void AgregarContenedorImagenInventario(int idUsuario, VwInventario inventario, string expedienteImagen)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario.Idinventario == 0)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }

                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();
               

                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha agregado el expediente fotográfico: " + expedienteImagen + " al producto: " + inventario.Modelo + " con número de serie: "
                    + inventario.Numerodeserie + ".";

                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Captura expediente";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
               // this.Respuesta = TipoAccion.Positiva("Eliminación exitosa.", historico.Id);
            }
            catch (Exception ex)
            {

               // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }



        public static void EditarContenedorImagenInventario(int idUsuario, VwInventario inventario, string expedienteViejo, string expedienteNuevo)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario.Idinventario == 0)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }

                if (expedienteViejo == null )
                {
                    throw new Exception("No se ha proporcionado el expediente");
                }

                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();
               

                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha actualizado el expediente fotográfico: " + expedienteViejo + " a: " + expedienteNuevo + "  del producto: " + inventario.Modelo + " con número de serie: "
                    + inventario.Numerodeserie + ".";

                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Modificación expediente";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
               // this.Respuesta = TipoAccion.Positiva("Eliminación exitosa.", historico.Id);
            }
            catch (Exception ex)
            {

               // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }


        public static void AgregarImagenContenedorInventario(int idUsuario, VwInventario inventario, string expediente)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario.Idinventario == 0)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }

                if (expediente == null)
                {
                    throw new Exception("No se ha proporcionado el expediente");
                }

                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();
               

                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha agregado imagene(s) al expediente fotográfico: " + expediente + " del producto: " + inventario.Modelo + " con número de serie: "
                    + inventario.Numerodeserie + ".";

                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Carga imagen en expediente";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
               // this.Respuesta = TipoAccion.Positiva("Eliminación exitosa.", historico.Id);
            }
            catch (Exception ex)
            {

               // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public static void EliminarImagenContenedorInventario(int idUsuario, VwInventario inventario, string expediente)
        {

            try
            {
                if (idUsuario == 0)
                {
                    throw new Exception("No se ha proporcionado el usuario");
                }

                if (inventario.Idinventario == 0)
                {
                    throw new Exception("No se ha proporcionado el inventario");
                }

                if (expediente == null)
                {
                    throw new Exception("No se ha proporcionado el expediente");
                }

                UsuariosApp usuario = ctx.UsuariosApps.Where(x => x.Id == idUsuario).FirstOrDefault();
               

                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha eliminado imagene(s) del expediente fotográfico: " + expediente + " del producto: " + inventario.Modelo + " con número de serie: "
                    + inventario.Numerodeserie + ".";

                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Tipo = "Eliminación imagen en expediente";
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
               // this.Respuesta = TipoAccion.Positiva("Eliminación exitosa.", historico.Id);
            }
            catch (Exception ex)
            {

               // this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }





        public TipoAccion seleccionar(string numeroSerie)
        {
            {

                return TipoAccion.Positiva(ctx.VwHistoricoInventarios.Where(x => x.Numerodeserie == numeroSerie).OrderByDescending(x => x.Inclusion).ToList());
            }
        }



    }
}
