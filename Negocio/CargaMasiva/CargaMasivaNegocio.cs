using Data.Models;
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
                adquision.Range["F1"].Value = "Fecha de compra";
                adquision.Range["F1"].ColumnWidth = 30;
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
                productos.Range["A2:A"+ num.ToString()].DataValidation.Values = listProductos.ToArray();
                productos.Range["B1"].Value = "Cantidad";
                productos.Range["B1"].ColumnWidth = 20;
                productos.Range["B1"].Style.Color = Color.DimGray;
                productos.Range["B1"].Style.Font.Color = Color.White;
                productos.Range["B1"].Style.VerticalAlignment = VerticalAlignType.Center;
                productos.Range["B1"].Style.Font.Size = 12;
                productos.Range["B1"].Style.Font.IsBold = true;
                productos.Range["C1"].Value = "Costo Unitario";
                productos.Range["C1"].ColumnWidth = 20;
                productos.Range["C1"].Style.Color = Color.DimGray;
                productos.Range["C1"].Style.Font.Color = Color.White;
                productos.Range["C1"].Style.VerticalAlignment = VerticalAlignType.Center;
                productos.Range["C1"].Style.Font.Size = 12;
                productos.Range["C1"].Style.Font.IsBold = true;
                productosNuevos.Visibility = WorksheetVisibility.Hidden;
                MemoryStream stream = new MemoryStream();
                workbook.SaveToStream(stream);
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
    }
}
