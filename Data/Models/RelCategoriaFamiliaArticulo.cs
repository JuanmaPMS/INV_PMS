using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelCategoriaFamiliaArticulo
{
    public int Id { get; set; }

    public int CatCategoriaFamiliaArticuloId { get; set; }

    public int CatFamiliaArticuloId { get; set; }

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatCategoriaFamiliaArticulo CatCategoriaFamiliaArticulo { get; set; } = null!;

    public virtual CatFamiliaArticulo CatFamiliaArticulo { get; set; } = null!;
}
