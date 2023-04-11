using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatContactosoporte
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual ICollection<RelProveedorContactosoporte> RelProveedorContactosoportes { get; } = new List<RelProveedorContactosoporte>();
}
