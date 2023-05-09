using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class cat_familia_articulo_falla_complex
    {
        public int? Id { get; set; }

        public int? CatFamiliaArticuloId { get; set; }

        public string? Falla { get; set; } = null!;

        public string? Descripcion { get; set; } = null!;

        public bool? Estatus { get; set; }

        public DateTime? Inclusion { get; set; }
    }
}
