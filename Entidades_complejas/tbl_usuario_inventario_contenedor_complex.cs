using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class tbl_usuario_inventario_contenedor_complex
    {
        public int Id { get; set; }

        public string Contenedor { get; set; } = null!;

        public int RelUsuarioInventarioId { get; set; }


        public  List<tbl_usuario_inventario_contenedor_imagenes_complex> TblUsuarioInventarioContenedorImagenes { get; set; } = null!;
    }

    public class tbl_usuario_inventario_contenedor_imagenes_complex
    {
        public int Id { get; set; }

        public string Imagen { get; set; } = null!;

        public string Descripcion { get; set; } = null!;
    }
}
