using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblMantenimientoInventario
{
    public int Id { get; set; }

    public int RelUsuarioInventarioId { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual RelUsuarioInventario RelUsuarioInventario { get; set; } = null!;
}
