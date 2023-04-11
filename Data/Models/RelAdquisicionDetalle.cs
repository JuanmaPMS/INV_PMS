using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelAdquisicionDetalle
{
    public int Id { get; set; }

    public int Cantidad { get; set; }

    public int TblAdquisicionId { get; set; }

    public int CatProductoId { get; set; }

    public double Costosiunitario { get; set; }

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatProducto CatProducto { get; set; } = null!;

    public virtual TblAdquisicion TblAdquisicion { get; set; } = null!;
}
