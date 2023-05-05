using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelUsuarioCorreoAdicional
{
    public int Id { get; set; }

    public int CatUsuarioId { get; set; }

    public string Correo { get; set; } = null!;

    public bool Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatUsuario CatUsuario { get; set; } = null!;
}
