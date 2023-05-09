using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatFamiliaArticulo
{
    public int Id { get; set; }

    public string Articulo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual ICollection<CatFamiliaArticuloFalla> CatFamiliaArticuloFallas { get; } = new List<CatFamiliaArticuloFalla>();

    public virtual ICollection<RelCategoriaFamiliaArticulo> RelCategoriaFamiliaArticulos { get; } = new List<RelCategoriaFamiliaArticulo>();
}
