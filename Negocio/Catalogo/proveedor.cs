using Data.Models;
using Entidades_complejas;
using Microsoft.IdentityModel.Tokens;

namespace Negocio.Catalogo
{
    public class proveedor_negocio
    {
        private readonly PmsInventarioContext _ctx = new();

        public TipoAccion Get(int? id)
        {
            try
            {
                var proveedores = id == null ?
                                   from proveedor in _ctx.CatProveedors
                                   join RelProveedorContactosoporte in _ctx.RelProveedorContactosoportes on proveedor.Id equals RelProveedorContactosoporte.CatProveedorId into ProveedorContacto
                                   from PC in ProveedorContacto.DefaultIfEmpty()
                                   join soporte in _ctx.CatContactosoportes.Where(x => x.Estatus == true) on PC.CatContactosoporteId equals soporte.Id into PCSoporte
                                   from soporteL in PCSoporte.DefaultIfEmpty()
                                   where proveedor.Estatus == true //&& soporteL.Estatus == true 
                                   select new
                                   {
                                       Id_Proveedor = proveedor.Id,
                                       Razon_Social_Proveedor = proveedor.Razonsocial,
                                       Correo_Proveedor = proveedor.Correo,
                                       Rfc_Proveedor = proveedor.Rfc,
                                       Id_Rel_Proveedor_Soporte = PC.Id,
                                       Id_Soporte = soporteL.Id,
                                       Nombre_Soporte = soporteL.Nombre,
                                       Correo_Soporte = soporteL.Correo,
                                       Telefono_Soporte = soporteL.Telefono
                                   } :
                                   from proveedor in _ctx.CatProveedors
                                   join RelProveedorContactosoporte in _ctx.RelProveedorContactosoportes on proveedor.Id equals RelProveedorContactosoporte.CatProveedorId into ProveedorContacto
                                   from PC in ProveedorContacto.DefaultIfEmpty()
                                   join soporte in _ctx.CatContactosoportes.Where(x => x.Estatus == true) on PC.CatContactosoporteId equals soporte.Id into PCSoporte
                                   from soporteL in PCSoporte.DefaultIfEmpty()
                                   where proveedor.Estatus == true //&& soporteL.Estatus == true 
                                   && proveedor.Id == id
                                   select new
                                   {
                                       Id_Proveedor = proveedor.Id,
                                       Razon_Social_Proveedor = proveedor.Razonsocial,
                                       Correo_Proveedor = proveedor.Correo,
                                       Rfc_Proveedor = proveedor.Rfc,
                                       Id_Rel_Proveedor_Soporte = PC.Id,
                                       Id_Soporte = soporteL.Id,
                                       Nombre_Soporte = soporteL.Nombre,
                                       Correo_Soporte = soporteL.Correo,
                                       Telefono_Soporte = soporteL.Telefono
                                   };

                if (proveedores == null)
                { 
                    throw new Exception("No existen registros en Cat_Proveedor"); 
                }
                else
                {
                    List<cat_proveedor_complex> listado_proveedores = new();
                    var proveedores_filtrado = proveedores.Select(q => new
                    {
                        q.Id_Proveedor,
                        q.Razon_Social_Proveedor,
                        q.Correo_Proveedor,
                        q.Rfc_Proveedor
                    }).Distinct();

                    //var list = proveedores_filtrado.ToList();

                    foreach (var proveedor in proveedores_filtrado.ToList())
                    {
                        cat_proveedor_complex nuevo_proveedor = new()
                        {
                            Id = proveedor.Id_Proveedor,
                            Razonsocial = proveedor.Razon_Social_Proveedor,
                            Correo = proveedor.Correo_Proveedor,
                            Rfc = proveedor.Rfc_Proveedor,
                            Contacto = new()
                        };

                        var contactos_soporte = proveedores.Where(q => q.Id_Proveedor == proveedor.Id_Proveedor && q.Id_Soporte > 0)
                            .Select(q => new
                            {
                                q.Id_Soporte,
                                q.Nombre_Soporte,
                                q.Correo_Soporte,
                                q.Telefono_Soporte
                            }).ToList();
                        if(contactos_soporte != null)
                        {
                            foreach (var soporte_contacto in contactos_soporte.ToList())
                            {
                                cat_contacto_soporte_complex contacto = new()
                                {
                                    Id = soporte_contacto.Id_Soporte,
                                    Nombre = soporte_contacto.Nombre_Soporte,
                                    Correo = soporte_contacto.Correo_Soporte,
                                    Telefono = soporte_contacto.Telefono_Soporte
                                };
                                nuevo_proveedor.Contacto.Add(contacto);
                            }
                        }
            

                        listado_proveedores.Add(nuevo_proveedor);
                    }

                    return TipoAccion.Positiva(listado_proveedores); 
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Add(cat_proveedor_complex entidad)
        {
            using var transaction = _ctx.Database.BeginTransaction();

            try
            {
                CatProveedor proveedor = _ctx.CatProveedors
                    .Where(x => x.Correo.ToUpper() == entidad.Correo.ToUpper() || x.Rfc.ToUpper() == entidad.Rfc.ToUpper())
                    .FirstOrDefault()!;

                if (proveedor != null)
                { 
                    throw new Exception("Ya existe el Proveedor en CatProveedor, favor de validar."); 
                }

                proveedor = new()
                {
                    Razonsocial = entidad.Razonsocial.ToUpper(),
                    Correo = entidad.Correo.ToUpper(),
                    Rfc = entidad.Rfc.ToUpper(),
                    Estatus = true,
                    Inclusion = DateTime.Now
                };

                _ctx.CatProveedors.Add(proveedor);
                _ctx.SaveChanges();

                if(entidad.Contacto!.Count > 0) 
                {
                    foreach (var contacto_soporte in entidad.Contacto!)
                    {
                        CatContactosoporte soporte = _ctx.CatContactosoportes
                            .Where(x => x.Correo.ToUpper() == contacto_soporte.Correo.ToUpper())
                            .FirstOrDefault()!;

                        if (soporte != null)
                        {
                            throw new Exception("Ya existe el Contacto soporte en CatContactosoporte, favor de validar.");
                        }

                        soporte = new()
                        {
                            Nombre = contacto_soporte.Nombre.ToUpper(),
                            Correo = contacto_soporte.Correo.ToUpper(),
                            Telefono = contacto_soporte.Telefono,
                            Estatus = true,
                            Inclusion = DateTime.Now
                        };

                        _ctx.CatContactosoportes.Add(soporte);
                        _ctx.SaveChanges();

                        RelProveedorContactosoporte relProveedorContactosoporte = new()
                        {
                            CatProveedorId = proveedor.Id,
                            CatContactosoporteId = soporte.Id,
                            Estatus = true
                        };

                        _ctx.RelProveedorContactosoportes.Add(relProveedorContactosoporte);
                        _ctx.SaveChanges();
                    }
                }
                
                transaction.Commit();

                return TipoAccion.Positiva("Se inserto registro exitosamente.", proveedor.Id);
                
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Update(cat_proveedor_complex entidad)
        {
            using var transaction = _ctx.Database.BeginTransaction();

            try
            {
                CatProveedor proveedor = _ctx.CatProveedors
                    .Where(x => x.Id == entidad.Id)
                    .FirstOrDefault()!;

                if (proveedor == null)
                {
                    throw new Exception("No existe el Proveedor en CatProveedor, favor de validar.");
                }

                proveedor.Razonsocial = entidad.Razonsocial.ToUpper();
                proveedor.Correo = entidad.Correo.ToUpper();
                proveedor.Rfc = entidad.Rfc.ToUpper();
                proveedor.Estatus = true;
                proveedor.Inclusion = DateTime.Now;

                _ctx.CatProveedors.Update(proveedor);
                _ctx.SaveChanges();

                // Eliminar:
                List<RelProveedorContactosoporte> proveedorContactosoporte_a_eliminar = _ctx.RelProveedorContactosoportes
                    .Where(q => q.CatProveedorId == entidad.Id)
                    .ToList();

                _ctx.RelProveedorContactosoportes.RemoveRange(proveedorContactosoporte_a_eliminar);
                _ctx.SaveChanges();

                if (entidad.Contacto!.Count > 0)
                {
                    foreach (var contacto_soporte in entidad.Contacto!)
                    {
                        CatContactosoporte soporte = _ctx.CatContactosoportes
                            .Where(x => x.Id == contacto_soporte.Id)
                            .FirstOrDefault()!;

                        soporte = new()
                        {
                            Nombre = contacto_soporte.Nombre.ToUpper(),
                            Correo = contacto_soporte.Correo.ToUpper(),
                            Telefono = contacto_soporte.Telefono,
                            Estatus = true,
                            Inclusion = DateTime.Now
                        };

                        _ctx.CatContactosoportes.Add(soporte);
                        _ctx.SaveChanges();

                        RelProveedorContactosoporte relProveedorContactosoporte = new()
                        {
                            CatProveedorId = proveedor.Id,
                            CatContactosoporteId = soporte.Id,
                            Estatus = true
                        };

                        _ctx.RelProveedorContactosoportes.Add(relProveedorContactosoporte);
                        _ctx.SaveChanges();
                    }
                }

                transaction.Commit();

                return TipoAccion.Positiva("Se actualizo registro exitosamente.", proveedor.Id);
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Disable(int id)
        {
            try
            {
                CatProveedor proveedor = _ctx.CatProveedors.Where(x => x.Id == id).FirstOrDefault()!;

                if (proveedor == null)
                { 
                    throw new Exception("No existe el registro en CatProveedor, favor de validar."); 
                }
                else
                {
                    proveedor.Estatus = false;

                    _ctx.CatProveedors.Update(proveedor);
                    _ctx.SaveChanges();

                    return TipoAccion.Positiva("Se inhabilito registro exitosamente.", proveedor.Id);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }
    }
}
