using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class tbl_inventario_arrendamiento_complex
    {
        public int? Id { get; set; }
        public int CatProductoId { get; set; }
        public int CatClienteId { get; set; }
        public int Cantidad { get; set; }
    }


    public class inventario_disponible_arrendamiento
    {
        public int IdProduto { get; set; }
        public string Modelo { get; set; }
        public string Fabricante { get; set; }
        public string Caracteristicas { get; set; }

    }

    public class rel_empleado_inventario_arrendamiento_complex
    {
        public int Id { get; set; }
        public int TblInventarioArrendamientoId { get; set; }
        public string CuentaEmpleadoCliente { get; set; } = null!;
        public string? Responsiva { get; set; } = null!;
        public string? NombreEmpleadoCliente { get; set; }

        //public List<RelArchivosEmpleadoInventarioArrendamiento> Archivos { get; set; }

    }

    public class empleado_inventario_arrendamiento_complex: VwEmpleadoInventarioArrendamiento
    {
        public List<RelArchivosEmpleadoInventarioArrendamiento> Archivos { get; set; }

    }


}
