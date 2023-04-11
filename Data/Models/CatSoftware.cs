using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatSoftware
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Fabricante { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual ICollection<RelAdquisicionesSoftwareProveedor> RelAdquisicionesSoftwareProveedors { get; } = new List<RelAdquisicionesSoftwareProveedor>();
}
