using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class responsiva_complex
    {
        public string? Usuario { get; set; }
        public string? Propietario { get; set; }
        public bool Asignacion { get; set; }
        public List<info_equipo_complex>? InfoGeneralEquipo { get; set; } = new();
        public List<info_equipo_complex>? InfoHardwareEquipo { get; set; } = new();
        public List<info_equipo_complex>? infoAccesoriosEquipo { get; set; } = new();
        public List<string>? InfoSoftwareEquipo { get; set; }
    }

    public class info_equipo_complex
    {
        public string? Caracteristica { get; set; }
        public string? Descripcion { get; set; }
    }
}
