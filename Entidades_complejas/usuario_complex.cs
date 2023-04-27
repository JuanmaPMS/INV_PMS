using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class usuario_complex
    {
        public int Id { get; set; }

        public string Nombres { get; set; } = null!;

        public string Apellidos { get; set; } = null!;

        public string Usuario { get; set; } = null!;

        public string? Password { get; set; } = null!;
    }

    public class usuario_login_complex
    {
        public string Usuario { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
