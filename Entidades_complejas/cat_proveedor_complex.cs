using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class cat_proveedor_complex
    {
        public int? Id { get; set; }

        public string Razonsocial { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Rfc { get; set; } = null!;

        public List<cat_contacto_soporte_complex>? Contacto { get; set; }
    }

    public class cat_contacto_soporte_complex
    {
        public int? Id { get; set; }

        public string Nombre { get; set; } = null!;

        public string Correo { get; set; } = null!;

        public string Telefono { get; set; } = null!;
    }
}
