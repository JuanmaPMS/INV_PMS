using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelInventarioUbicacion
{
    public int Id { get; set; }

    public int TblInventarioId { get; set; }

    public int RelClienteUbicacionOficinaId { get; set; }

    public bool Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual RelClienteUbicacionOficina RelClienteUbicacionOficina { get; set; } = null!;

    public virtual TblInventario TblInventario { get; set; } = null!;
}
