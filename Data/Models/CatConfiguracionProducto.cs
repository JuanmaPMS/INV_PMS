using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatConfiguracionProducto
{
    public int Id { get; set; }

    public int? CatCategoriaProductoId { get; set; }

    public string? Descripcion { get; set; }

    public bool? Estatus { get; set; }

    public DateTime? Inclusion { get; set; }

    public virtual CatCategoriaProducto? CatCategoriaProducto { get; set; }

    public virtual ICollection<RelUsuarioInventarioConfiguracion> RelUsuarioInventarioConfiguracions { get; } = new List<RelUsuarioInventarioConfiguracion>();
}
