using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelArchivosUsuarioInventario
{
    public int Id { get; set; }

    public int? RelUsuarioInventarioId { get; set; }

    public string? Archivo { get; set; }

    public bool? Estatus { get; set; }

    public DateTime? Inclusion { get; set; }

    public virtual RelUsuarioInventario? RelUsuarioInventario { get; set; }
}
