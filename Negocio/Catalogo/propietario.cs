using Data.Models;
using Entidades_complejas;

namespace Negocio
{
    public class propietario_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();

        public TipoAccion Get(int? id)
        {
            try
            {
                List<CatPropietario> propietarios = id == null ? ctx.CatPropietarios.Where(x => x.Estatus == true).OrderBy(x => x.Razonsocial).ToList()
                                                : ctx.CatPropietarios.Where(x => x.Id == id).ToList();
                if (propietarios.Count == 0)
                { throw new Exception("No existen registros en Cat_Propietario"); }
                else
                { return TipoAccion.Positiva(propietarios); }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Add(cat_propietario_complex entidad)
        {
            try
            {
                CatPropietario propietario = ctx.CatPropietarios.Where(x => x.Rfc.ToUpper() == entidad.Rfc.ToUpper() && x.Estatus == true).FirstOrDefault();

                if (propietario != null)
                { throw new Exception("Ya existe el Propietario en Cat_Propietario, favor de validar."); }
                else
                {
                    CatPropietario catPropietario = new CatPropietario();
                    catPropietario.Sigla = entidad.Sigla.ToUpper();
                    catPropietario.Razonsocial = entidad.Razonsocial.ToUpper();
                    catPropietario.Rfc = entidad.Rfc.ToUpper();
                    catPropietario.Estatus = true;
                    catPropietario.Inclusion = DateTime.Now;

                    ctx.CatPropietarios.Add(catPropietario);
                    ctx.SaveChanges();

                    return TipoAccion.Positiva("Se inserto registro exitosamente.", catPropietario.Id);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Update(cat_propietario_complex entidad)
        {
            try
            {
                CatPropietario propietario = ctx.CatPropietarios.Where(x => x.Id == entidad.Id).FirstOrDefault();

                if (propietario == null)
                { throw new Exception("No existe el registro en Cat_Propietario, favor de validar."); }
                else
                {
                    //Valida que no se duplique registro
                    CatFabricante valida = ctx.CatFabricantes.Where(x => x.Nombre.ToUpper() == entidad.Rfc.ToUpper() && x.Estatus == true && x.Id != entidad.Id).FirstOrDefault();

                    if (valida != null)
                    { throw new Exception("No se puede duplicar el registro Cat_Propietario, favor de validar."); }
                    else
                    {
                        propietario.Sigla = entidad.Sigla.ToUpper();
                        propietario.Razonsocial = entidad.Razonsocial.ToUpper();
                        propietario.Rfc = entidad.Rfc.ToUpper();

                        ctx.CatPropietarios.Update(propietario);
                        ctx.SaveChanges();

                        return TipoAccion.Positiva("Se actualizo registro exitosamente.", propietario.Id);
                    }
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
                CatPropietario propietario = ctx.CatPropietarios.Where(x => x.Id == id).FirstOrDefault();

                if (propietario == null)
                { throw new Exception("No existe el registro en Cat_Propietario, favor de validar."); }
                else
                {
                    propietario.Estatus = false;

                    ctx.CatPropietarios.Update(propietario);
                    ctx.SaveChanges();

                    return TipoAccion.Positiva("Se inhabilito registro exitosamente.", propietario.Id);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }
    }
}
