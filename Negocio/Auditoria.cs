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

        public static object  Get(string tipoObject, string id)
        {
            try
            {
                List<TblInventarioHistorico> logs =  ctx.TblInventarioHistoricos.Where(x => x.TipoObjeto == tipoObject && x.Objeto.Contains("{\"Id\":"+ id)).ToList();
                if (logs.Count == 0)
                { throw new Exception("No existen registros en Cat_Usuario"); }
                else
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
        }

    }
}
