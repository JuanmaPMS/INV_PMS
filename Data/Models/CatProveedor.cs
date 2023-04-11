using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatProveedor
{
    public int Id { get; set; }

    public string Razonsocial { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual ICollection<RelProveedorContactosoporte> RelProveedorContactosoportes { get; } = new List<RelProveedorContactosoporte>();

    public virtual ICollection<TblAdquisicion> TblAdquisicions { get; } = new List<TblAdquisicion>();
}
