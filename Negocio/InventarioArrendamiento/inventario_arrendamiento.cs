using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.InventarioArrendamiento
{
    public class inventario_arrendamiento_negocio
    {
        public PmsInventarioContext ctx = new PmsInventarioContext();
        public tbl_inventario_complex inventario { get; set; }
        public TipoAccion Respuesta { get; set; }
        public Boolean estatus { get; set; }

        public void agregar(tbl_inventario_arrendamiento_complex input)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    if (input.Cantidad == 0)
                    {

                        throw new Exception("No se ha proporcionado una cantidad.");
                    }

                    if (input.CatClienteId == 0)
                    {

                        throw new Exception("No se ha proporcionado un cliente.");
                    }


                    if (input.CatProductoId == 0)
                    {

                        throw new Exception("No se ha proporcionado un producto.");
                    }

                    List<VwInventario> disponibles = this.inventarioDisponibleArrendamiento(input.CatProductoId);
                    List<VwInventario> lista = new List<VwInventario>();

                    if (disponibles.Count == 0)
                    {

                        throw new Exception("No existe inventario disponible para arrendar.");
                    }
                    else {

                        if (disponibles.Count >= input.Cantidad)
                        {
                            lista = disponibles.Take(input.Cantidad).ToList();

                            foreach (VwInventario inventario_complex in lista)
                            {
                                TblInventarioArrendamiento tblInventario = new TblInventarioArrendamiento();
                                tblInventario.TblInventarioId = inventario_complex.Idinventario;
                                tblInventario.CatClienteId = input.CatClienteId;
                                tblInventario.Estatus = true;
                                tblInventario.Inclusion = DateTime.Now;
                                ctx.TblInventarioArrendamientos.Add(tblInventario);
                                

                                //SE ACTUALIZA EL ESTATUS DEL INVENTARIO
                                TblInventario inventario = ctx.TblInventarios.Where(x=>x.Id == inventario_complex.Idinventario).FirstOrDefault();

                                //11 ==> EN ARRENDAMIENTO
                                inventario.CatEstatusinventarioId = 11;
                                ctx.TblInventarios.Update(inventario);
                                ctx.SaveChanges();
                            }
                        }
                        else {
                            throw new Exception("No hay suficiente inventario para arrendar");
                        }

                    }
                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Arrendamiento exitoso.");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }


        public empleado_inventario_arrendamiento_complex seleccionarAsignacion(int id)
        {
            {
                var asignacion = (empleado_inventario_arrendamiento_complex)ctx.VwEmpleadoInventarioArrendamientos.Where(x => x.Idrelempleadoinventarioarrendamiento == id).FirstOrDefault(); 
                if(asignacion != null)
                {
                    asignacion.Archivos = ctx.RelArchivosEmpleadoInventarioArrendamientos.Where(x => x.RelEmpleadoInventarioArrendamientoId == id).ToList();
                    
                } else
                {
                    asignacion = new empleado_inventario_arrendamiento_complex();
                }
                
                return asignacion;

            }

        }

        public void agregarAsignacion(rel_empleado_inventario_arrendamiento_complex input)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {
                    if (input.TblInventarioArrendamientoId == 0)
                    {

                        throw new Exception("No se ha proporcionado el producto a asignar.");
                    }

                    if (input.CuentaEmpleadoCliente == String.Empty)
                    {

                        throw new Exception("No se ha proporcionado el empleado.");
                    }

                    //if (input.Responsiva == String.Empty)
                    //{

                    //    throw new Exception("No se ha proporcionado la responsiva.");
                    //}

                    RelEmpleadoInventarioArrendamiento empleadoinventario = new RelEmpleadoInventarioArrendamiento();
                    empleadoinventario.Id = 0;
                    empleadoinventario.TblInventarioArrendamientoId = input.TblInventarioArrendamientoId;
                    empleadoinventario.CuentaEmpleadoCliente = input.CuentaEmpleadoCliente;
                    empleadoinventario.NombreEmpleadoCliente = input.NombreEmpleadoCliente;
                    empleadoinventario.Responsiva = input.Responsiva;
                    empleadoinventario.Estatus = true;
                    empleadoinventario.Inclusion = DateTime.Now;

                    ctx.RelEmpleadoInventarioArrendamientos.Add(empleadoinventario);

                    ctx.SaveChanges();

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Asignacion exitosa.",empleadoinventario.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }

        public void agregarArchivosAsignacion(List<RelArchivosEmpleadoInventarioArrendamiento> input)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {

                    ctx.RelArchivosEmpleadoInventarioArrendamientos.UpdateRange(input);
                    ctx.SaveChanges();

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Registro de archivos exitoso");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }


        public void editarAsignacion(rel_empleado_inventario_arrendamiento_complex input)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {

                    if (input.Id == 0)
                    {

                        throw new Exception("No se ha proporcionado el inventario a actualizar.");
                    }

                    if (input.TblInventarioArrendamientoId == 0)
                    {

                        throw new Exception("No se ha proporcionado el producto a asignar.");
                    }

                    if (input.CuentaEmpleadoCliente == String.Empty)
                    {

                        throw new Exception("No se ha proporcionado el empleado.");
                    }

                    if (input.Responsiva == String.Empty)
                    {

                        throw new Exception("No se ha proporcionado la responsiva.");
                    }

                    RelEmpleadoInventarioArrendamiento empleadoinventario = ctx.RelEmpleadoInventarioArrendamientos.Where(x=> x.Id== input.Id).FirstOrDefault();
                    //empleadoinventario.CuentaEmpleadoCliente = input.CuentaEmpleadoCliente;
                    //empleadoinventario.NombreEmpleadoCliente = input.NombreEmpleadoCliente;
                    empleadoinventario.Responsiva = input.Responsiva;

                    ctx.RelEmpleadoInventarioArrendamientos.Update(empleadoinventario);


                    ctx.SaveChanges();

                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("actualizacion exitosa.", empleadoinventario.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }

        public void eliminarAsignacion(int id)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {

                    if (id== 0)
                    {
                        throw new Exception("No se ha proporcionado el inventario a eliminar.");
                    }

                    RelEmpleadoInventarioArrendamiento empleadoinventario = ctx.RelEmpleadoInventarioArrendamientos.Where(x => x.Id == id).FirstOrDefault();
                    empleadoinventario.Estatus = false;
                    ctx.RelEmpleadoInventarioArrendamientos.Update(empleadoinventario);

                    ctx.SaveChanges();
                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Eliminación exitosa.", empleadoinventario.Id);
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    this.Respuesta = TipoAccion.Negativa(ex.Message);
                }
            }
        }


        //Obtencion de datos
        public inventario_arrendamiento_negocio()
        { }

        public List<VwInventarioArrendamientoAgrupado> todosAgrupado()
        {
            
            return ctx.VwInventarioArrendamientoAgrupados.ToList(); 
        }

        public List<VwInventarioArrendamiento> detalle(int idProducto, int idCliente)
        {
            {
                
                return ctx.VwInventarioArrendamientos.Where(x => x.Idproducto == idProducto && x.Idcliente == idCliente).ToList();

            }

        }

        public List<VwInventarioArrendamiento> seleccionarInventarioDisponible(int idCliente)
        {
            {
                return ctx.VwInventarioArrendamientos.Where(x => x.Idcliente == idCliente).ToList();
            }
        }

        public List<VwInventario> inventarioDisponibleArrendamiento(int idProducto)
        {
            {
                if (idProducto != null)

                {
                    return ctx.VwInventarios.Where(x => x.Numerodeserie != string.Empty && x.Idproducto == idProducto && x.CatEstatusinventarioId == 1).ToList(); //CatEstatusinventarioId = 1 ====> Libre
                }
                else
                {
                    return new List<VwInventario>();
                }
            }

        }




    }
}
