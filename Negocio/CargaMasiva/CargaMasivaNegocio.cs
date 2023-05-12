using Data.Models;
using Spire.Xls;
using System;
using System.Collections.Generic;
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
                //Hoja 1 -> Formulario de adquision 
                adquision.Range["A1"].Value = "Proveedor";
                adquision.Range["A5"].DataValidation.Values = listProveedores.ToArray();
                adquision.Range["B1"].Value = "Propietario";
                adquision.Range["B2"].DataValidation.Values = listPropietarios.ToArray();
                adquision.Range["C1"].Value = "Articulos";
                adquision.Range["D1"].Value = "Monto";
                adquision.Range["E1"].Value = "Impuesto";
                adquision.Range["F1"].Value = "Fecha de compra";
                //Hoja 2 -> Productos
                productos.Range["A1"].Value = "Producto";
                productos.Range["A2:A"+ num.ToString()].DataValidation.Values = listProductos.ToArray();
                productos.Range["B1"].Value = "Cantidad";
                productos.Range["C1"].Value = "Costo Unitario";

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
