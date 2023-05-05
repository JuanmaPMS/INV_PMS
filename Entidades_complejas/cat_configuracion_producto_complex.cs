using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class cat_configuracion_producto_complex
    {
        public int Id { get; set; }

        public int? CatCategoriaProductoId { get; set; }

        public string? Descripcion { get; set; }
    }
}
