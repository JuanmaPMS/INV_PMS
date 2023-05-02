using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelClienteUbicacionOficina
{
    public int Id { get; set; }

    public int TblClienteUbicacionId { get; set; }

    public string Nombre { get; set; } = null!;

    public int EjeX { get; set; }

    public int EjeY { get; set; }

    public int Alto { get; set; }

    public int Ancho { get; set; }

    public bool? Estatus { get; set; }

    public DateTime? Inclusion { get; set; }

    public virtual TblClienteUbicacion TblClienteUbicacion { get; set; } = null!;
}
