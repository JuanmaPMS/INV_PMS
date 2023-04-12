using Data.Models;
using Entidades_complejas;

namespace Negocio.Catalogo
{
    public class cliente_negocio
    {
        private readonly PmsInventarioContext _ctx = new();

        public TipoAccion Get(int? id)
        {
            try
            {
                List<CatCliente> clientes = id == null ? 
                    _ctx.CatClientes.Where(x => x.Estatus == true).OrderBy(x => x.Nombre).ToList() : 
                    _ctx.CatClientes.Where(x => x.Id == id && x.Estatus == true).ToList();

                if (clientes.Count == 0)
                {
                    throw new Exception("No existen registros en CatCliente");
                }
                else
                {
                    return TipoAccion.Positiva(clientes);
                }
            }
            catch (Exception ex)
            {

                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Add(cat_cliente_complex entidad)
        {
            try
            {
                CatCliente cliente = _ctx.CatClientes.Where(x => 
                    x.Nombre.ToUpper() == entidad.Nombre.ToUpper() && 
                    x.Rfc == entidad.Rfc && 
                    x.Estatus == true)
                .FirstOrDefault()!;

                if (cliente != null)
                { 
                    throw new Exception("Ya existe el cliente en CatCliente, favor de validar."); 
                }
                else
                {
                    CatCliente nuevo_cliente = new()
                    {
                        Nombre = entidad.Nombre.ToUpper(),
                        Sigla = entidad.Sigla.ToUpper(),
                        Razonsocial = entidad.Razonsocial.ToUpper(),
                        Rfc = entidad.Rfc.ToUpper(),
                        Direccion = entidad.Direccion.ToUpper(),
                        Latitud = entidad.Latitud.ToUpper(),
                        Longitud = entidad.Longitud.ToUpper(),
                        Estatus = true,
                        Inclusion = DateTime.Now
                    };

                    _ctx.CatClientes.Add(nuevo_cliente);
                    _ctx.SaveChanges();

                    return TipoAccion.Positiva("Se inserto registro exitosamente.", nuevo_cliente.Id);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Update(cat_cliente_complex entidad)
        {
            try
            {
                CatCliente cliente = _ctx.CatClientes.Where(x => x.Id == entidad.Id).FirstOrDefault()!;

                if (cliente == null)
                { 
                    throw new Exception("No existe el registro en CatCliente, favor de validar."); 
                }
                else
                {
                    cliente.Nombre = entidad.Nombre.ToUpper();
                    cliente.Sigla = entidad.Sigla.ToUpper();
                    cliente.Razonsocial = entidad.Razonsocial.ToUpper();
                    cliente.Rfc = entidad.Rfc.ToUpper();
                    cliente.Direccion = entidad.Direccion.ToUpper();
                    cliente.Latitud = entidad.Latitud.ToUpper();
                    cliente.Longitud = entidad.Longitud.ToUpper();

                    _ctx.CatClientes.Update(cliente);
                    _ctx.SaveChanges();

                    return TipoAccion.Positiva("Se actualizo registro exitosamente.", cliente.Id);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Disable(int id)
        {
            try
            {
                CatCliente cliente = _ctx.CatClientes.Where(x => x.Id == id).FirstOrDefault()!;

                if (cliente == null)
                { 
                    throw new Exception("No existe el registro en CatCliente, favor de validar."); 
                }
                else
                {
                    cliente.Estatus = false;

                    _ctx.CatClientes.Update(cliente);
                    _ctx.SaveChanges();

                    return TipoAccion.Positiva("Se inhabilito registro exitosamente.", cliente.Id);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }
    }
}
