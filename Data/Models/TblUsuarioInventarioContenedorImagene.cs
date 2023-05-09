using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblUsuarioInventarioContenedorImagene
{
    public int Id { get; set; }

    public string Imagen { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int TblUsuarioInventarioContenedorId { get; set; }

    public DateTime? Inclusion { get; set; }

    public virtual TblUsuarioInventarioContenedor TblUsuarioInventarioContenedor { get; set; } = null!;
}
