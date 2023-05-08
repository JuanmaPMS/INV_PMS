using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatFamiliaArticuloFalla
{
    public int Id { get; set; }

    public int CatFamiliaArticuloId { get; set; }

    public string Falla { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatFamiliaArticulo CatFamiliaArticulo { get; set; } = null!;
}
