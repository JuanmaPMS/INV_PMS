using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblUsuarioInventarioContenedor
{
    public int Id { get; set; }

    public string Contenedor { get; set; } = null!;

    public int RelUsuarioInventarioId { get; set; }

    public DateTime? Inclusion { get; set; }

    public virtual RelUsuarioInventario RelUsuarioInventario { get; set; } = null!;

    public virtual ICollection<TblUsuarioInventarioContenedorImagene> TblUsuarioInventarioContenedorImagenes { get; } = new List<TblUsuarioInventarioContenedorImagene>();
}
