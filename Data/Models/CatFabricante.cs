using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatFabricante
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual ICollection<CatProducto> CatProductos { get; } = new List<CatProducto>();
}
