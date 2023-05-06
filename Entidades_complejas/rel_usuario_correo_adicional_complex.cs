using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class rel_usuario_correo_adicional_complex
    {
        public int? Id { get; set; }
        public int? CatUsuarioId { get; set; }
        public string? Correo { get; set; } = null!;
        public bool? Estatus { get; set; }
        public DateTime? Inclusion { get; set; }
    }
}
