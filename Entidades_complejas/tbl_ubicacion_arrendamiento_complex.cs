using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class tbl_ubicacion_arrendamiento_complex
    {
        public int? Id { get; set; }
        public int CatClienteId { get; set; }
        public string? Cliente { get; set; }
        public string Direccion { get; set; } = null!;
        public string Edificio { get; set; } = null!;
        public string Piso { get; set; } = null!;
        public string? Plano { get; set; }
        public List<rel_ubicacion_oficina_arrendamiento_complex>? RelClienteUbicacionOficinaArrendamientos { get; set; }
    }

    public class rel_ubicacion_oficina_arrendamiento_complex
    {
        public int? Id { get; set; }
        public int TblClienteUbicacionArrendamientoId { get; set; }
        public string Nombre { get; set; } = null!;
        public int EjeX { get; set; }
        public int EjeY { get; set; }
        public int Alto { get; set; }
        public int Ancho { get; set; }

    }
}
