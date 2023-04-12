using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class cat_categoria_producto_complex
    {
        public int? Id { get; set; }
        public string Nombre { get; set; } = null!;
        public bool Estatico { get; set; }
    }
}
