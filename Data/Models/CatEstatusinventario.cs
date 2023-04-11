using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatEstatusinventario
{
    public int Id { get; set; }

    public string Estatus { get; set; } = null!;

    public DateTime Inclusion { get; set; }

    public virtual ICollection<TblInventario> TblInventarios { get; } = new List<TblInventario>();
}
