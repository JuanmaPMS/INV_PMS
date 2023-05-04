using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatCategoriaProducto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool? Estatus { get; set; }

    public bool Estatico { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual ICollection<CatConfiguracionProducto> CatConfiguracionProductos { get; } = new List<CatConfiguracionProducto>();

    public virtual ICollection<CatProducto> CatProductos { get; } = new List<CatProducto>();
}
