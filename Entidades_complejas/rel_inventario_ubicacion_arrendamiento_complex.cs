using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class rel_inventario_ubicacion_arrendamiento_complex
    {
        public int Id { get; set; }
        public int TblInventarioId { get; set; }
        public int RelClienteUbicacionOficinaArrendamientoId { get; set; }
        public bool Confirm { get; set; }
    }

    public class equipo_arrendamiento_complex
    {
        public int Id { get; set; }
        public string Filtro { get; set; } = null!;
    }

    public class inventario_ubicacion_arrendamiento_complex
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Equipo { get; set; }
        public string? Serie { get; set; }
        public string? Clave { get; set; }
    }
}
