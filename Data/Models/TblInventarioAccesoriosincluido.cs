using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblInventarioAccesoriosincluido
{
    public int Id { get; set; }

    public int TblInventarioId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Detalle { get; set; }

    public virtual TblInventario TblInventario { get; set; } = null!;
}
