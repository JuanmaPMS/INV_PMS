using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatPropietario
{
    public int Id { get; set; }

    public string Sigla { get; set; } = null!;

    public string Razonsocial { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual ICollection<TblAdquisicion> TblAdquisicions { get; } = new List<TblAdquisicion>();
}
