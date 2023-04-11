using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblCaracteristicasElementoHardwareinfra
{
    public int Id { get; set; }

    public string Caracteristica { get; set; } = null!;

    public string Valor { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual ICollection<RelElementoHardwareinfraCaracteristicasArchivosProveedor> RelElementoHardwareinfraCaracteristicasArchivosProveedors { get; } = new List<RelElementoHardwareinfraCaracteristicasArchivosProveedor>();
}
