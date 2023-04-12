using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class cat_propietario_complex
    {
        public int? Id { get; set; }

        public string Sigla { get; set; } = null!;

        public string Razonsocial { get; set; } = null!;

        public string Rfc { get; set; } = null!;

    }
}
