using Data.Models;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Data;

namespace Entidades_complejas
{
    public static class Auditoria
    {
        private static PmsInventarioContext ctx = new PmsInventarioContext();

        public static void Log(object objeto, int? usuarioApp)
        {
            TblInventarioHistorico inventarioHistorico = new TblInventarioHistorico()
            {
                TipoObjeto = objeto.GetType().Name,
                Objeto = JsonSerializer.Serialize(objeto),
                UsuariosAppId = usuarioApp
            };

            ctx.TblInventarioHistoricos.Add(inventarioHistorico);
            ctx.SaveChanges();
        }

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
                var inventario_ = ctx.VwInventarios.Where(x => x.Idinventario == inventario.Idinventario).FirstOrDefault();

                string descripcion = "El usuario: " + usuario.Nombres + " " + usuario.Apellidos + " ha modificado el número de serie a: " + numeroSerie +
                " del producto: " + inventario.Modelo + ".";


                TblHistoricoInventario historico = new TblHistoricoInventario();
                historico.Id = 0;
                historico.Descripcion = descripcion;
                historico.UsuariosAppId = idUsuario;
                historico.TblInventarioId = inventario.Idinventario;
                historico.Inclusion = DateTime.Now;
                ctx.TblHistoricoInventarios.Add(historico);
                ctx.SaveChanges();
                //this.Respuesta = TipoAccion.Positiva("Número de serie exitoso.", historico.Id);
            }
            catch (Exception ex)
            {

                //this.Respuesta = TipoAccion.Negativa(ex.Message);
            }
        }

        public static object  Get(string tipoObject, string NumSerie)
        {
            try
            {
                List<TblInventarioHistorico> logs =  ctx.TblInventarioHistoricos.Where(x => x.TipoObjeto == tipoObject && x.Objeto.Contains("\"Numerodeserie\":\"" + NumSerie + "\"")).ToList();
                if (logs.Count > 0)
                {
                    Type type = Type.GetType("Data.Models." + tipoObject);
                    object objType = new object();
                    foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        type = asm.GetType("Data.Models." + tipoObject);
                        if (type != null)
                            objType = Activator.CreateInstance(type);
                    }

                    List<object> lst = new List<object>();
                    foreach (var obj in logs)
                    {
                        objType = JsonSerializer.Deserialize<dynamic>(obj.Objeto);
                        lst.Add(objType);
                    }
                    
                    return TipoAccion.Positiva(lst);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }



        public static object GetInventario(string NumSerie)
        {
            try
            {
                List<TblInventario> logs = ctx.TblInventarios.Where(x => x.Numerodeserie == NumSerie ).ToList();

                if (logs.Count > 0)
                {
                    List<tbl_inventario_historico_complex> lista1 = new List<tbl_inventario_historico_complex>();
                    List<tbl_inventario_historico_complex> lista2 = new List<tbl_inventario_historico_complex>();

                    var lst = ctx.TblInventarioHistoricos.Where(x => x.Objeto.Contains("\"TblInventarioId\":" + logs.First().Id ))
                        .OrderByDescending(x => x.InclusionHistorico).ToList();

                    lst.ForEach(x =>
                    {
                        tbl_inventario_historico_complex historico = new tbl_inventario_historico_complex();
                        historico.Id = x.Id;
                        historico.TipoObjeto = x.TipoObjeto;
                        historico.Objeto = x.Objeto;
                        historico.UsuariosAppId = x.UsuariosAppId;

                        //UsuariosApp usu = ctx.UsuariosApps.Where((x) => x.Id == historico.UsuariosAppId).FirstOrDefault();

                        //historico.NombreUsuario = usu == null ? string.Concat(usu.Nombres," ", usu.Apellidos):"";
                        historico.InclusionHistorico = x.InclusionHistorico;
                        lista1.Add(historico);
                    }
                    );


                    var lst1 = ctx.TblInventarioHistoricos.Where(x => x.TipoObjeto == "TblInventario" && x.Objeto.Contains("\"Id\":" + logs.First().Id))
                        .OrderByDescending(x => x.InclusionHistorico).ToList();



                    lista2.ForEach(x =>
                    {
                        tbl_inventario_historico_complex historico = new tbl_inventario_historico_complex();
                        historico.Id = x.Id;
                        historico.TipoObjeto = x.TipoObjeto;
                        historico.Objeto = x.Objeto;
                        historico.UsuariosAppId = x.UsuariosAppId;

                        //UsuariosApp usu = ctx.UsuariosApps.Where((x) => x.Id == historico.UsuariosAppId).FirstOrDefault();

                        //historico.NombreUsuario = string.Concat(usu.Nombres, " ", usu.Apellidos);
                        historico.InclusionHistorico = x.InclusionHistorico;
                        lista2.Add(historico);
                    }
                    );




                    List <object> list= new List<object>();
                    list.Add(lista1);
                    list.Add(lista2);

                    return TipoAccion.Positiva(list);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }



    }
}
