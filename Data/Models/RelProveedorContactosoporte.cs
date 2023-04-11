using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelProveedorContactosoporte
{
    public int Id { get; set; }

    public int CatProveedorId { get; set; }

    public int CatContactosoporteId { get; set; }

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatContactosoporte CatContactosoporte { get; set; } = null!;

    public virtual CatProveedor CatProveedor { get; set; } = null!;

    public virtual ICollection<RelAdquisicionesSoftwareProveedor> RelAdquisicionesSoftwareProveedors { get; } = new List<RelAdquisicionesSoftwareProveedor>();
}
