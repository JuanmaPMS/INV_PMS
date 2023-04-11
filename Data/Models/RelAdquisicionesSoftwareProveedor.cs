using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelAdquisicionesSoftwareProveedor
{
    public int Id { get; set; }

    public double Cunitario { get; set; }

    public int Unidades { get; set; }

    public string Facxml { get; set; } = null!;

    public string Facpdf { get; set; } = null!;

    public int CatSoftwareId { get; set; }

    public int RelProveedorContactosoporteId { get; set; }

    public bool Garantia { get; set; }

    public string Garantiafile { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatSoftware CatSoftware { get; set; } = null!;

    public virtual RelProveedorContactosoporte RelProveedorContactosoporte { get; set; } = null!;
}
