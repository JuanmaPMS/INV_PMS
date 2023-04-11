using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelProductoCatacteristica
{
    public int Id { get; set; }

    public int CatProductoId { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual CatProducto CatProducto { get; set; } = null!;
}
