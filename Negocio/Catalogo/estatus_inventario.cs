using Data.Models;
using Entidades_complejas;

namespace Negocio.Catalogo
{
    public class estatus_inventario
    {
        private readonly PmsInventarioContext _ctx = new();

        public TipoAccion Get(int? id)
        {
            try
            {
                List<CatEstatusinventario> inventario = id == null ?
                    _ctx.CatEstatusinventarios.ToList() :
                    _ctx.CatEstatusinventarios.Where(x => x.Id == id).ToList();

                if (inventario.Count == 0)
                {
                    throw new Exception("No existen registros en CatEstatusinventario");
                }
                else
                {
                    return TipoAccion.Positiva(inventario);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }
    }
}
