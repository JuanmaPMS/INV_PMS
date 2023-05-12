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
                    var lst = ctx.TblInventarioHistoricos.Where(x => x.Objeto.Contains("\"TblInventarioId\":" + logs.First().Id ))
                        .OrderByDescending(x => x.InclusionHistorico).ToList();

                    var lst1 = ctx.TblInventarioHistoricos.Where(x => x.TipoObjeto == "TblInventario" && x.Objeto.Contains("\"Id\":" + logs.First().Id))
                        .OrderByDescending(x => x.InclusionHistorico).ToList();

                    List <object> list= new List<object>();
                    list.Add(lst);
                    list.Add(lst1);

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
