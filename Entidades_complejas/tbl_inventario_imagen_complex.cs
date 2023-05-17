using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class tbl_inventario_imagen_complex
    {
        public int Id { get; set; }
        public int TblInventarioId { get; set; }
        public string Imagen { get; set; } = null!;
        public int? usuarioAppid { get; set; }
    }
}
