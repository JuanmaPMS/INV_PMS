using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class adquisicion_masiva_complex
    {
        public string? Proveedor { get; set; }
        public string? Propietario { get; set; }
        public string? Monto { get; set; }
        public string? Impuesto { get; set; }
        public string? Articulos { get; set; }
        public DateTime? Fechadecompra { get; set; }
        public List<adquisicion_masiva_detalle_complex>? RelAdquisicionDetalles { get; set; }
    }

    public class adquisicion_masiva_detalle_complex
    {
        public string? Producto { get; set; }
        public string? Costosiunitario { get; set; }
        public string? Numerodeserie { get; set; }
        public string? Inventarioclv { get; set; }
    }
}
