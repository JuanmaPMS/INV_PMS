using Data.Models;
using Entidades_complejas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.PortableExecutable;
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

                var asignacion = ctx.VwEmpleadoInventarioArrendamientos.Where(x => x.Idrelempleadoinventarioarrendamiento == id).FirstOrDefault();
                empleado_inventario_arrendamiento_complex objeto = new empleado_inventario_arrendamiento_complex();

                objeto.Idrelempleadoinventarioarrendamiento =  asignacion.Idrelempleadoinventarioarrendamiento; 
                objeto.Responsiva = asignacion.Responsiva; 
                objeto.CuentaEmpleadoCliente = asignacion.CuentaEmpleadoCliente; 
                objeto.NombreEmpleadoCliente = asignacion.NombreEmpleadoCliente; 
                objeto.Estatus = asignacion.Estatus; 
                objeto.Idinventarioarrendamiento = asignacion.Idinventarioarrendamiento; 
                objeto.Idinventario = asignacion.Idinventario; 
                objeto.Idadquisicion = asignacion.Idadquisicion; 
                objeto.Idproducto = asignacion.Idproducto; 
                objeto.Idfabricante = asignacion.Idfabricante; 
                objeto.Fabricante = asignacion.Fabricante; 
                objeto.Modelo = asignacion.Modelo; 
                objeto.Idcategoria = asignacion.Idcategoria; 
                objeto.Categoria = asignacion.Categoria; 
                objeto.Esestatico = asignacion.Esestatico; 
                objeto.Anio = asignacion.Anio; 
                objeto.Nuevo = asignacion.Nuevo; 
                objeto.Vidautil = asignacion.Vidautil; 
                objeto.Caracteristicas = asignacion.Caracteristicas; 
                objeto.Numerodeserie = asignacion.Numerodeserie; 
                objeto.Inventarioclv = asignacion.Inventarioclv; 
                objeto.Notainventario = asignacion.Notainventario; 
                objeto.CatEstatusinventarioId = asignacion.CatEstatusinventarioId; 
                objeto.CatEstatusinventario = asignacion.CatEstatusinventario; 
                objeto.Idcliente = asignacion.Idcliente; 
                objeto.Cliente = asignacion.Cliente; 
                objeto.Direccioncliente = asignacion.Direccioncliente; 
                objeto.Latitudcliente = asignacion.Latitudcliente; 
                objeto.Longitudcliente = asignacion.Longitudcliente; 
                objeto.Accesorios = asignacion.Accesorios;






                if (asignacion != null)
                {
                    objeto.Archivos = ctx.RelArchivosEmpleadoInventarioArrendamientos.Where(x => x.RelEmpleadoInventarioArrendamientoId == id && x.Estatus == true).ToList();
                    
                } else
                {
                    objeto = new empleado_inventario_arrendamiento_complex();
                }
                
                return objeto;

            }

        }

        public List<VwEmpleadoInventarioArrendamiento> seleccionarAsignacionTodos(int idCliente)
        {
            {
               return ctx.VwEmpleadoInventarioArrendamientos.Where(x => x.Estatus == true && x.Idcliente == idCliente).ToList();   
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

        public void agregarArchivosAsignacion(List<archivos_empleado_inventario_arrendamiento_complex> input)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {

                    List<RelArchivosEmpleadoInventarioArrendamiento> listaArchivos = new List<RelArchivosEmpleadoInventarioArrendamiento>();

                    foreach(archivos_empleado_inventario_arrendamiento_complex item in input)
                    {
                        RelArchivosEmpleadoInventarioArrendamiento archivo = new RelArchivosEmpleadoInventarioArrendamiento();
                        archivo.Id = 0;
                        archivo.RelEmpleadoInventarioArrendamientoId = item.RelEmpleadoInventarioArrendamientoId;
                        archivo.Archivo = item.Archivo;
                        archivo.Estatus = true;
                        archivo.Inclusion = DateTime.Now;
                        listaArchivos.Add(archivo); 
                    }

                    ctx.RelArchivosEmpleadoInventarioArrendamientos.AddRange(listaArchivos);
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

        public void eliminarArchivoAsignacion(int id)
        {
            using (var tran = ctx.Database.BeginTransaction())
            {
                try
                {

                    if (id == 0)
                    {
                        throw new Exception("No se ha proporcionado el inventario a eliminar.");
                    }

                    RelArchivosEmpleadoInventarioArrendamiento archivoempleadoinventario = ctx.RelArchivosEmpleadoInventarioArrendamientos.Where(x => x.Id == id).FirstOrDefault();
                    archivoempleadoinventario.Estatus = false;
                    ctx.RelArchivosEmpleadoInventarioArrendamientos.Update(archivoempleadoinventario);
                    ctx.SaveChanges();
                    tran.Commit();
                    this.Respuesta = TipoAccion.Positiva("Eliminación exitosa.", archivoempleadoinventario.Id);
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

                        throw new Exception("No se ha proporcionado el producto.");
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

        public List<VwInventarioProductosDisponible> seleccionarInventarioProductosDisponible()
        {
            {
                return ctx.VwInventarioProductosDisponibles.Where(x => x.CatEstatusinventarioId == 1).ToList();
            }
        }

        public List<VwInventarioArrendamiento> seleccionarInventarioArrendamientoDisponible(int idCliente)
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
