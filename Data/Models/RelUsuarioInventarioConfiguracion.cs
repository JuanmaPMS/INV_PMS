using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelUsuarioInventarioConfiguracion
{
    public int Id { get; set; }

    public int RelUsuarioInventarioId { get; set; }

    public int CatConfiguracionProductoId { get; set; }

    public string Valor { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime? Inclusion { get; set; }

    public virtual CatConfiguracionProducto CatConfiguracionProducto { get; set; } = null!;

    public virtual RelUsuarioInventario RelUsuarioInventario { get; set; } = null!;
}
