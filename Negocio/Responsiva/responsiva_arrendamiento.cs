using Data.Models;
using Entidades_complejas;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Responsiva
{
    public class responsiva_arrendamiento_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();

        public responsiva_complex Get(int id)
        {
            responsiva_complex _responsiva = new responsiva_complex();
            try
            {
                RelEmpleadoInventarioArrendamiento relArrendamiento = ctx.RelEmpleadoInventarioArrendamientos
                                                  //.Include(x => x.CatUsuario)
                                                  .Include(x => x.TblInventarioArrendamiento).ThenInclude(x => x.TblInventario).ThenInclude(x => x.TblAdquisicion).ThenInclude(x => x.CatPropietario)
                                                  .Include(x => x.TblInventarioArrendamiento).ThenInclude(x => x.TblInventario).ThenInclude(x => x.CatProducto).ThenInclude(x => x.CatCategoriaProducto)
                                                  .Where(x => x.Id == id).FirstOrDefault();

                if (relArrendamiento == null)
                { throw new Exception("No se encontro la asignación solicitada."); }

                _responsiva.Usuario = relArrendamiento.NombreEmpleadoCliente;
                _responsiva.Propietario = relArrendamiento.TblInventarioArrendamiento.TblInventario.TblAdquisicion.CatPropietario.Razonsocial;
                _responsiva.Asignacion = true;

                //Info general equipo
                List<info_equipo_complex> lstGeneral = new List<info_equipo_complex>();
                info_equipo_complex g1 = new info_equipo_complex();
                g1.Caracteristica = relArrendamiento.TblInventarioArrendamiento.TblInventario.CatProducto.CatCategoriaProducto.Nombre;
                g1.Descripcion = relArrendamiento.TblInventarioArrendamiento.TblInventario.CatProducto.Modelo;
                lstGeneral.Add(g1);

                info_equipo_complex g2 = new info_equipo_complex();
                g2.Caracteristica = "No. de Serie";
                g2.Descripcion = relArrendamiento.TblInventarioArrendamiento.TblInventario.Numerodeserie;
                lstGeneral.Add(g2);

                _responsiva.InfoGeneralEquipo = lstGeneral;

                //Accesorios equipo
                List<info_equipo_complex> lstAccesorios = new List<info_equipo_complex>();
                List<TblInventarioAccesoriosincluido> accesoriosincluido = ctx.TblInventarioAccesoriosincluidos.Where(x => x.TblInventarioId == relArrendamiento.TblInventarioArrendamiento.TblInventarioId).ToList();
                foreach (TblInventarioAccesoriosincluido accesorio in accesoriosincluido)
                {
                    info_equipo_complex addAccesorio = new info_equipo_complex();
                    addAccesorio.Caracteristica = accesorio.Nombre;
                    addAccesorio.Descripcion = accesorio.Detalle;
                    lstAccesorios.Add(addAccesorio);
                }

                _responsiva.InfoAccesoriosEquipo = lstAccesorios;

                //Hardware equipo
                List<string> lstHardware = new List<string>();
                List<RelProductoCatacteristica> hardware = ctx.RelProductoCatacteristicas.Where(x => x.CatProductoId == relArrendamiento.TblInventarioArrendamiento.TblInventario.CatProductoId && x.Hardware == true).ToList();
                foreach (RelProductoCatacteristica caracteristica in hardware)
                {
                    string nombre = caracteristica.Nombre;
                    lstHardware.Add(nombre);
                }

                _responsiva.InfoHardwareEquipo = lstHardware;

                //Software equipo
                List<string> lstSoftware = new List<string>();
                List<RelProductoCatacteristica> software = ctx.RelProductoCatacteristicas.Where(x => x.CatProductoId == relArrendamiento.TblInventarioArrendamiento.TblInventario.CatProductoId && x.Software == true).ToList();
                foreach (RelProductoCatacteristica caracteristica in hardware)
                {
                    string nombre = caracteristica.Nombre;
                    lstSoftware.Add(nombre);
                }

                _responsiva.InfoSoftwareEquipo = lstSoftware;

                return _responsiva;
            }
            catch (Exception ex)
            {
                return _responsiva = new();
            }
        }
    }
}
