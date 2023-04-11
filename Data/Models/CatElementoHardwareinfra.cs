using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatElementoHardwareinfra
{
    public int Id { get; set; }

    public string Modelo { get; set; } = null!;

    public int Anio { get; set; }

    public string Fabricante { get; set; } = null!;

    public int Vidautil { get; set; }

    public string Observaciones { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual ICollection<RelElementoHardwareinfraCaracteristicasArchivosProveedor> RelElementoHardwareinfraCaracteristicasArchivosProveedors { get; } = new List<RelElementoHardwareinfraCaracteristicasArchivosProveedor>();
}
