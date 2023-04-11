using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwSoftwareProveedorsoporteAdquisicion
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Fabricante { get; set; } = null!;

    public int Idproveedorsoporte { get; set; }

    public string Razonsocial { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public double Cunitario { get; set; }

    public int Unidades { get; set; }

    public double Total { get; set; }

    public string Cfdi { get; set; } = null!;

    public string Representaciongrafica { get; set; } = null!;

    public bool Garantia { get; set; }

    public string Garantiafile { get; set; } = null!;
}
