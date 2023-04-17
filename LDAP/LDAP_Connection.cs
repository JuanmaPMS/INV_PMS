using Data.Models;
using Entidades_complejas;

namespace LDAP
{
    public class LDAP_Connection
    {
        private readonly PmsInventarioContext _ctx = new();

        public TipoAccion Get(int? id)
        {
            try
            {
                List<CatDirLdap> conexiones = id == null ? _ctx.CatDirLdaps.ToList() : _ctx.CatDirLdaps.Where(x => x.Id == id).ToList();

                if (conexiones.Count == 0)
                {
                    throw new Exception("No existen registros en Cat_Dir_Ldap");
                }
                else
                {
                    return TipoAccion.Positiva(conexiones);
                }
            }
            catch (Exception ex)
            {

                return TipoAccion.Negativa(ex.Message);
            }
        }
    }
}
