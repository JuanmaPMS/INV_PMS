using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class tbl_inventario_complex
    {
        public int? Id { get; set; }
        public int TblAdquisicionId { get; set; }
        public int CatProductoId { get; set; }
        public int? CatEstatusinventarioId { get; set; }
        public string Numerodeserie { get; set; } = null!;
        public string Inventarioclv { get; set; } = null!;
        public string? Notas { get; set; }
        public List<tbl_inventario_accesorio_complex>? Accesorios { get; set; }
    }

    public class tbl_inventario_accesorio_complex
    {
        public int? Id { get; set; }
        public int TblInventarioId { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Detalle { get; set; }
    }
}
