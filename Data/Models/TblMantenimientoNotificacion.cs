using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblMantenimientoNotificacion
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public int? CatClienteId { get; set; }

    public bool? Activo { get; set; }

    public DateTime? Inclusion { get; set; }

    public virtual CatCliente? CatCliente { get; set; }
}
