using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblInventarioUbicacionArrendamiento
{
    public int Id { get; set; }

    public int TblInventarioId { get; set; }

    public int CatClienteId { get; set; }

    public string Edificio { get; set; } = null!;

    public string Piso { get; set; } = null!;

    public string Oficina { get; set; } = null!;

    public string? Autorizasalida { get; set; }

    public string? Autorizaentrada { get; set; }

    public string? Notas { get; set; }

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatCliente CatCliente { get; set; } = null!;

    public virtual TblInventario TblInventario { get; set; } = null!;
}
