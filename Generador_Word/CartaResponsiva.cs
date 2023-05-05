namespace Generador_Word
{
    public class CartaResponsiva
    {
        public string? Usuario { get; set; }
        public string? Propietario { get; set; }
        public bool Asignacion { get; set; }
        public List<InfoEquipo>? InfoGeneralEquipo { get; set; } = new();
        public List<InfoEquipo>? InfoHardwareEquipo { get; set; } = new();
        public List<InfoEquipo>? infoAccesoriosEquipo { get; set; } = new();
        public List<string>? InfoSoftwareEquipo { get; set; }
    }

    public class InfoEquipo
    {
        public string? Caracteristica { get; set; }
        public string? Descripcion { get; set; }
    }
}
