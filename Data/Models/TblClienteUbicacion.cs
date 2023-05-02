using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblClienteUbicacion
{
    public int Id { get; set; }

    public int CatClienteId { get; set; }

    public string Direccion { get; set; } = null!;

    public string Edificio { get; set; } = null!;

    public string Piso { get; set; } = null!;

    public string? Plano { get; set; }

    public bool? Estatus { get; set; }

    public DateTime? Inclusion { get; set; }

    public virtual CatCliente CatCliente { get; set; } = null!;

    public virtual ICollection<RelClienteUbicacionOficina> RelClienteUbicacionOficinas { get; } = new List<RelClienteUbicacionOficina>();
}
