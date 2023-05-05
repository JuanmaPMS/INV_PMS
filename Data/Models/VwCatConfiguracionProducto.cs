using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwCatConfiguracionProducto
{
    public int Id { get; set; }

    public int? CatCategoriaProductoId { get; set; }

    public string Categoria { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Estatus { get; set; }

    public DateTime? Inclusion { get; set; }
}
