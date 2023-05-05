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
        public cat_usuario_complex? ubicacion { get; set; }

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
    }
}
