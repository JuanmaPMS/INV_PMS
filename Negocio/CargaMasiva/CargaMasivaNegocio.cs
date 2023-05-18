using Data.Models;
using Entidades_complejas;
using Negocio.HistoricoInventario;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.CargaMasiva
{
    public class CargaMasivaNegocio
    {
        private readonly PmsInventarioContext ctx = new PmsInventarioContext();

        public byte[] obtenerPlantilla(int num)
        {
            try
            {
                List<string> listProductos = new List<string>();
                foreach (var producto in ctx.CatProductos.Where(x => x.Estatus == true))
                {
                    listProductos.Add(producto.Modelo);
                }
                List<string> listProveedores = new List<string>();
                foreach (var proveedor in ctx.CatProveedors.Where(x => x.Estatus == true))
                {
                    listProveedores.Add(proveedor.Razonsocial);
                }
                List<string> listPropietarios = new List<string>();
                foreach (var propietario in ctx.CatPropietarios.Where(x => x.Estatus == true))
                {
                    listPropietarios.Add(propietario.Razonsocial);
                }
                //Create a Workbook object
                Spire.Xls.Workbook workbook = new Workbook();
                //Obtener y nombrar cada del libro de excel
                Worksheet adquision = workbook.Worksheets[0];
                adquision.Name = "Adquisición";
                Worksheet productos = workbook.Worksheets[1];
                productos.Name = "Productos";
                Worksheet productosNuevos = workbook.Worksheets[2];
                productosNuevos.Name = "Productos2";
                for (int i = 0; i < listProveedores.Count; i++)
                {
                    productosNuevos.Range["J"+(i+2).ToString()].Value = listProveedores[i];
                }
                //Hoja 1 -> Formulario de adquision 
                adquision.Range["A1"].Value = "Proveedor";
                adquision.Range["A1"].ColumnWidth = 65;
                adquision.Range["A1"].Style.Color = Color.DimGray;
                adquision.Range["A1"].Style.Font.Color = Color.White;
                adquision.Range["A1"].Style.VerticalAlignment = VerticalAlignType.Center;
                adquision.Range["A1"].Style.Font.Size = 12;
                adquision.Range["A1"].Style.Font.IsBold = true;
                adquision.Range["A2"].DataValidation.DataRange = productosNuevos.Range["J2:J"+(listProveedores.ToArray().Length+2).ToString()];
                //adquision.Range["A2"].DataValidation.Values = listProveedores.ToArray();
                adquision.Range["A2"].DataValidation.IsSuppressDropDownArrow = false;
                adquision.Range["B1"].Value = "Propietario";
                adquision.Range["B1"].ColumnWidth = 45;
                adquision.Range["B1"].Style.Color = Color.DimGray;
                adquision.Range["B1"].Style.Font.Color = Color.White;
                adquision.Range["B1"].Style.VerticalAlignment = VerticalAlignType.Center;
                adquision.Range["B1"].Style.Font.Size = 12;
                adquision.Range["B1"].Style.Font.IsBold = true;
                adquision.Range["B2"].DataValidation.Values = listPropietarios.ToArray();
                adquision.Range["C1"].Value = "Articulos";
                adquision.Range["C1"].ColumnWidth = 20;
                adquision.Range["C1"].Style.Color = Color.DimGray;
                adquision.Range["C1"].Style.Font.Color = Color.White;
                adquision.Range["C1"].Style.VerticalAlignment = VerticalAlignType.Center;
                adquision.Range["C1"].Style.Font.Size = 12;
                adquision.Range["C1"].Style.Font.IsBold = true;
                adquision.Range["D1"].Value = "Monto";
                adquision.Range["D1"].ColumnWidth = 20;
                adquision.Range["D1"].Style.Color = Color.DimGray;
                adquision.Range["D1"].Style.Font.Color = Color.White;
                adquision.Range["D1"].Style.VerticalAlignment = VerticalAlignType.Center;
                adquision.Range["D1"].Style.Font.Size = 12;
                adquision.Range["D1"].Style.Font.IsBold = true;
                adquision.Range["E1"].Value = "Impuesto";
                adquision.Range["E1"].ColumnWidth = 20;
                adquision.Range["E1"].Style.Color = Color.DimGray;
                adquision.Range["E1"].Style.Font.Color = Color.White;
                adquision.Range["E1"].Style.VerticalAlignment = VerticalAlignType.Center;
                adquision.Range["E1"].Style.Font.Size = 12;
                adquision.Range["E1"].Style.Font.IsBold = true;
                //adquision.Range["F1"].Value = "Fecha de compra";
                adquision.Range["F1"].Value = "Fecha de compra (DD/MM/YYYY)";
                adquision.Range["F1"].ColumnWidth = 35;
                adquision.Range["F1"].Style.Color = Color.DimGray;
                adquision.Range["F1"].Style.Font.Color = Color.White;
                adquision.Range["F1"].Style.VerticalAlignment = VerticalAlignType.Center;
                adquision.Range["F1"].Style.Font.Size = 12;
                adquision.Range["F1"].Style.Font.IsBold = true;
                //Hoja 2 -> Productos
                productos.Range["A1"].Value = "Producto";
                productos.Range["A1"].ColumnWidth = 40;
                productos.Range["A1"].Style.Color = Color.DimGray;
                productos.Range["A1"].Style.Font.Color = Color.White;
                productos.Range["A1"].Style.VerticalAlignment = VerticalAlignType.Center;
                productos.Range["A1"].Style.Font.Size = 12;
                productos.Range["A1"].Style.Font.IsBold = true;
                productos.Range["A2:A" + (num + 1).ToString()].DataValidation.Values = listProductos.ToArray();
                productos.Range["B1"].Value = "Costo Unitario";
                productos.Range["B1"].ColumnWidth = 20;
                productos.Range["B1"].Style.Color = Color.DimGray;
                productos.Range["B1"].Style.Font.Color = Color.White;
                productos.Range["B1"].Style.VerticalAlignment = VerticalAlignType.Center;
                productos.Range["B1"].Style.Font.Size = 12;
                productos.Range["B1"].Style.Font.IsBold = true;
                productos.Range["C1"].Value = "Clave";
                productos.Range["C1"].ColumnWidth = 20;
                productos.Range["C1"].Style.Color = Color.DimGray;
                productos.Range["C1"].Style.Font.Color = Color.White;
                productos.Range["C1"].Style.VerticalAlignment = VerticalAlignType.Center;
                productos.Range["C1"].Style.Font.Size = 12;
                productos.Range["C1"].Style.Font.IsBold = true;
                productos.Range["D1"].Value = "Serie";
                productos.Range["D1"].ColumnWidth = 20;
                productos.Range["D1"].Style.Color = Color.DimGray;
                productos.Range["D1"].Style.Font.Color = Color.White;
                productos.Range["D1"].Style.VerticalAlignment = VerticalAlignType.Center;
                productos.Range["D1"].Style.Font.Size = 12;
                productos.Range["D1"].Style.Font.IsBold = true;
                productosNuevos.Visibility = WorksheetVisibility.Hidden;
                MemoryStream stream = new MemoryStream();
                workbook.SaveToStream(stream, FileFormat.Version2016);
                byte[] data = stream.ToArray();
                stream.Flush();
                stream.Close();
                return data;
            }
            catch
            {
                return null;
            }
        }

        //Carga masiva
        public TipoAccion carga_masiva(adquisicion_masiva_complex input, int idUsuario)
        {
            TipoAccion result = new TipoAccion();
            try
            {
                string MsError = string.Empty;
                int ProveedorId = 0;
                int PropietarioId = 0;

                CatProveedor catProveedor = ctx.CatProveedors.Where(x => x.Razonsocial == input.Proveedor).FirstOrDefault();
                if(catProveedor == null)
                { MsError = "No se encontró al proveedor\n"; }
                else { ProveedorId = catProveedor.Id; }

                CatPropietario catPropietario = ctx.CatPropietarios.Where(x => x.Razonsocial == input.Propietario).FirstOrDefault();
                if (catPropietario == null)
                { MsError = "No se encontró al propietario\n"; }
                else { PropietarioId = catPropietario.Id; }

                TblAdquisicion ad = new TblAdquisicion();
                ad.CatProveedorId = ProveedorId;
                ad.CatPropietarioId = PropietarioId;
                ad.Monto = Convert.ToDouble(input.Monto!);
                ad.Impuesto = Convert.ToDouble(input.Impuesto!);
                ad.Articulos = Convert.ToInt32(input.Articulos!);
                ad.Fechadecompra = input.Fechadecompra!.Value;
                ad.FacXml = string.Empty;
                ad.FacPdf = string.Empty;
                ctx.TblAdquisicions.Add(ad);
                ctx.SaveChanges();

                if (input.RelAdquisicionDetalles != null && input.RelAdquisicionDetalles.Count() > 0)
                {
                    foreach (adquisicion_masiva_detalle_complex detalle_complex in input.RelAdquisicionDetalles)
                    {
                        int ProductoId = 0;
                        double CostoUnitario = 0;
                        CatProducto catProducto = ctx.CatProductos.Where(x => x.Modelo == detalle_complex.Producto).FirstOrDefault();
                        if (catProducto == null)
                        { MsError = "No se encontró el producto: " + detalle_complex.Producto + "\n"; }
                        else { ProductoId = catProducto.Id; }

                        try { CostoUnitario = Convert.ToDouble(detalle_complex.Costosiunitario!); }
                        catch { MsError = "El costo unitario del producto: " + detalle_complex.Producto + ", no tiene el formato requerido.\n"; }

                        if(ProductoId > 0)
                        {
                            //Inserta inventario
                            TblInventario tblInventario = new TblInventario();

                            tblInventario.TblAdquisicionId = ad.Id;
                            tblInventario.CatProductoId = ProductoId;
                            tblInventario.CatEstatusinventarioId = 1;//LIBRE
                            tblInventario.Numerodeserie = detalle_complex.Numerodeserie;
                            tblInventario.Inventarioclv = detalle_complex.Inventarioclv;
                            tblInventario.Estatus = true;
                            tblInventario.Inclusion = DateTime.Now;

                            ctx.TblInventarios.Add(tblInventario);
                            ctx.SaveChanges();



                            ///INICIO - SE REGISTRA HISTORICO
                            var inventario = ctx.VwInventarios.Where(x => x.Idinventario == tblInventario.Id).FirstOrDefault();

                            if (tblInventario.Numerodeserie != null)
                            {
                                historico_inventario_negocio.CapturaNumeroDeSerie(idUsuario, inventario, tblInventario.Numerodeserie);
                            }

                            if (tblInventario.Inventarioclv != null)
                            {
                                historico_inventario_negocio.CapturaClaveInventario(idUsuario, inventario, tblInventario.Inventarioclv);
                            }
                            ///FIN - SE REGISTRA HISTORICO

                            RelAdquisicionDetalle relDetalle = ctx.RelAdquisicionDetalles.Where(x => x.TblAdquisicionId == ad.Id && x.CatProductoId == ProductoId).FirstOrDefault();
                            if (relDetalle == null)
                            {
                                RelAdquisicionDetalle adquisicionDetalle = new RelAdquisicionDetalle();
                                adquisicionDetalle.Cantidad = 1;
                                adquisicionDetalle.TblAdquisicionId = ad.Id;
                                adquisicionDetalle.CatProductoId = ProductoId;
                                adquisicionDetalle.Costosiunitario = CostoUnitario;
                                adquisicionDetalle.Estatus = true;
                                adquisicionDetalle.Inclusion = DateTime.Now;

                                ctx.RelAdquisicionDetalles.Add(adquisicionDetalle);
                                ctx.SaveChanges();
                            }
                            else
                            {
                                relDetalle.Cantidad = relDetalle.Cantidad + 1;
                                ctx.RelAdquisicionDetalles.Update(relDetalle);
                                ctx.SaveChanges();
                            }
                        }
                        
                    }
                }

                if(string.IsNullOrEmpty(MsError))
                {
                    result = TipoAccion.Positiva("Carga completada", ad.Id);
                }
                else
                {
                    result = TipoAccion.Negativa(MsError, ad.Id);
                }          
            }
            catch (Exception ex)
            {
                result = TipoAccion.Negativa(ex.Message);
            }

            return result;
        }

        public TipoAccion carga_masiva_adjuntos(adquisicion_masiva_doc_complex input)
        {
            TipoAccion result = new TipoAccion();
            try
            {
                TblAdquisicion tblAdquisicion = ctx.TblAdquisicions.Where(x => x.Id == input.IdAdquision).FirstOrDefault();

                if(tblAdquisicion == null) 
                {
                    throw new Exception("No existe el registro en Tbl_Adquisiciones, favor de validar.");
                }
 
                tblAdquisicion.FacXml = input.Xml;
                tblAdquisicion.FacPdf = input.Pdf;

                ctx.TblAdquisicions.Update(tblAdquisicion);
                ctx.SaveChanges();

                result = TipoAccion.Positiva("Actualización Exitosa", tblAdquisicion.Id);
            }
            catch (Exception ex)
            {
                result = TipoAccion.Negativa(ex.Message);
            }

            return result;
        }




    }
}
