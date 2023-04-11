using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwFamiliaArticuloCategorium
{
    public int Idfamiliaarticulo { get; set; }

    public string Familiaarticulo { get; set; } = null!;

    public int Idcategoria { get; set; }

    public string Categoria { get; set; } = null!;

    public string Categoriadescripcion { get; set; } = null!;

    public bool Estatus { get; set; }
}
