using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwElementoHardwareinfra
{
    public int Idelementohardwareinfra { get; set; }

    public string Articulo { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public int Idelemento { get; set; }

    public string Modelo { get; set; } = null!;

    public int Anio { get; set; }

    public string Fabricante { get; set; } = null!;

    public int Vidautil { get; set; }

    public string Observaciones { get; set; } = null!;
}
