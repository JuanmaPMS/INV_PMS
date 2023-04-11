using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwProveedorSoporte
{
    public int Id { get; set; }

    public int Idproveedor { get; set; }

    public string Razonsocial { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public bool Estatus { get; set; }

    public int Idsoporte { get; set; }

    public string Nombrecorreo { get; set; } = null!;

    public string Soportecorreo { get; set; } = null!;

    public string Telefonosporte { get; set; } = null!;
}
