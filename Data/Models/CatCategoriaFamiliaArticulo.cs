using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatCategoriaFamiliaArticulo
{
    public int Id { get; set; }

    public string Categoria { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual ICollection<RelCategoriaFamiliaArticulo> RelCategoriaFamiliaArticulos { get; } = new List<RelCategoriaFamiliaArticulo>();
}
