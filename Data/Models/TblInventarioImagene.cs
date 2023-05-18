using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblInventarioImagene
{
    public int Id { get; set; }

    public int TblInventarioId { get; set; }

    public string Imagen { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime? Inclusion { get; set; }

    public virtual TblInventario TblInventario { get; set; } = null!;
}
