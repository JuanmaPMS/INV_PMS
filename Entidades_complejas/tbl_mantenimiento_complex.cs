using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class tbl_mantenimiento_complex
    {
        public int? Id { get; set; }

        public int RelUsuarioInventarioId { get; set; }

        public DateTime Inclusion { get; set; }

    }
}
