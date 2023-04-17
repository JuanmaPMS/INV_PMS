using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatUsuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Cuenta { get; set; }

    public string? Correo { get; set; }

    public string? Ubicacion { get; set; }

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<RelUsuarioInventario> RelUsuarioInventarios { get; } = new List<RelUsuarioInventario>();
}
