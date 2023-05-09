using Data.Models;
using Entidades_complejas;
using Microsoft.EntityFrameworkCore;

namespace LDAP
{
    public class LDAP_Connection
    {
        private readonly PmsInventarioContext _ctx = new();

        public TipoAccion GetById(int id, string nombre)
        {
            try
            {
                CatCliente conexiones = _ctx.CatClientes
                        .Where(x => x.Id == id)
                        .Include(q => q.CatDirLdaps)
                        .FirstOrDefault()!;

                if (conexiones == null)
                {
                    throw new Exception("No existen registros en CatCliente");
                }
                else
                {
                    ListarUsuarios listar = new();
                    return listar.Listar_Usuarios(conexiones.CatDirLdaps.ElementAt(0).DirEntry!, nombre);
                    //return TipoAccion.Positiva(listar.Listar_Usuarios(conexiones.CatDirLdaps.ElementAt(0).DirEntry!, nombre));
                }
            }
            catch (Exception ex)
            {

                return TipoAccion.Negativa(ex.Message);
            }
        }
    }
}
