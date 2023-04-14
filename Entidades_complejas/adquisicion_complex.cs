using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class adquisicion_complex
    {
        public int? Id { get; set; }
        public int? CatProveedorId { get; set; }
        public string? Proveedor { get; set; }
        public int? CatPropietarioId { get; set; }
        public string? Propietario { get; set; }
        public double? Monto { get; set; }
        public double? Impuesto { get; set; }
        public int? Articulos { get; set; }
        public string? FacPdf { get; set; } = null!;
        public string? FacXml { get; set; } = null!;
        public DateTime? Fechadecompra { get; set; }
        public List<adquisicion_detalle_complex>? RelAdquisicionDetalles { get; set; }
        public List<inventario_complex>? TblInventarios { get; set; }
    }

    public class adquisicion_detalle_complex
    {
        public int? Id { get; set; }
        public int Cantidad { get; set; }
        public int TblAdquisicionId { get; set; }
        public int CatProductoId { get; set; }
        public double Costosiunitario { get; set; }
    }

    public class inventario_complex
    {
        public int Id { get; set; }
        public int TblAdquisicionId { get; set; }
        public int CatProductoId { get; set; }
        public int CatEstatusinventarioId { get; set; }
        public string? Estatusinventario { get; set; }
        public string Numerodeserie { get; set; } = null!;
        public string Inventarioclv { get; set; } = null!;
        public string? Notas { get; set; }
    }

}
