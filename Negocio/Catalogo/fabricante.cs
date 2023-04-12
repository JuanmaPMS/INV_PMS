using Data.Models;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades_complejas;

namespace Negocio
{
    public class fabricante_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public TipoAccion Get(int? id)
        {
            try
            {
                List<CatFabricante> fabricantes = id == null ? ctx.CatFabricantes.Where(x => x.Estatus == true).OrderBy(x => x.Nombre).ToList()
                                                : ctx.CatFabricantes.Where(x => x.Id == id).ToList();
                if (fabricantes.Count == 0)
                { throw new Exception("No existen registros en Cat_Fabricante"); }
                else
                { return TipoAccion.Positiva(fabricantes); }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Add(cat_fabricante_complex entidad)
        {
            try
            {
                CatFabricante fabricante = ctx.CatFabricantes.Where(x => x.Nombre.ToUpper() == entidad.Nombre.ToUpper() && x.Estatus == true).FirstOrDefault();

                if (fabricante != null)
                { throw new Exception("Ya existe el Fabricante en Cat_Fabricante, favor de validar."); }
                else
                {
                    CatFabricante catFabricante = new CatFabricante();
                    catFabricante.Nombre = entidad.Nombre.ToUpper();
                    catFabricante.Estatus = true;
                    catFabricante.Inclusion = DateTime.Now;

                    ctx.CatFabricantes.Add(catFabricante);
                    ctx.SaveChanges();

                    return TipoAccion.Positiva("Se inserto registro exitosamente.", catFabricante.Id);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }

        public TipoAccion Update(cat_fabricante_complex entidad)
        {
            try
            {
                CatFabricante fabricante = ctx.CatFabricantes.Where(x => x.Id == entidad.Id).FirstOrDefault();

                if (fabricante == null)
                { throw new Exception("No existe el registro en Cat_Fabricante, favor de validar."); }
                else
                {
                    //Valida que no se duplique registro
                    CatFabricante valida = ctx.CatFabricantes.Where(x => x.Nombre.ToUpper() == entidad.Nombre.ToUpper() && x.Estatus == true && x.Id != entidad.Id).FirstOrDefault();

                    if (valida != null)
                    { throw new Exception("No se puede duplicar el registro Cat_Fabricante, favor de validar."); }
                    else
                    {
                        fabricante.Nombre = entidad.Nombre.ToUpper();

                        ctx.CatFabricantes.Update(fabricante);
                        ctx.SaveChanges();

                        return TipoAccion.Positiva("Se actualizo registro exitosamente.", fabricante.Id);
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
                CatFabricante fabricante = ctx.CatFabricantes.Where(x => x.Id == id).FirstOrDefault();

                if (fabricante == null)
                { throw new Exception("No existe el registro en Cat_Fabricante, favor de validar."); }
                else
                {
                    fabricante.Estatus = false;

                    ctx.CatFabricantes.Update(fabricante);
                    ctx.SaveChanges();

                    return TipoAccion.Positiva("Se inhabilito registro exitosamente.", fabricante.Id);
                }
            }
            catch (Exception ex)
            {
                return TipoAccion.Negativa(ex.Message);
            }
        }
    }
}
