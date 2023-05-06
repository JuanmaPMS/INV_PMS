using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class rel_usuario_inventario_complex
    {
        public int Id { get; set; }
        public int CatUsuarioId { get; set; }
        public int TblInventarioId { get; set; }
        public string Responsiva { get; set; } = null!;
        public List<rel_usuario_inventario_configuracion_complex>? Configuracion { get; set; }
    }

    public class rel_usuario_inventario_configuracion_complex
    {
        public int Id { get; set; }
        public int RelUsuarioInventarioId { get; set; }
        public int CatConfiguracionProductoId { get; set; }
        public string Valor { get; set; } = null!;
    }


    public class usuario_inventario_complex : VwUsuarioInventario
    {
        public List<RelArchivosUsuarioInventario> Archivos { get; set; }
        public List<RelUsuarioInventarioConfiguracion> Configuracion { get; set; }

    }
}
