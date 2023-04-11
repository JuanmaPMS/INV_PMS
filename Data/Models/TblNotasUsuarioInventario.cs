using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblNotasUsuarioInventario
{
    public int Id { get; set; }

    public string Nota { get; set; } = null!;

    public string Archivo { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }
}
