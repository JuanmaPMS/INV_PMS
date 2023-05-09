using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblInventarioHistorico
{
    public int Id { get; set; }

    public int? TblInventarioId { get; set; }

    public int? TblAdquisicionId { get; set; }

    public int? CatProductoId { get; set; }

    public int? CatEstatusinventarioId { get; set; }

    public string? Numerodeserie { get; set; }

    public string? Inventarioclv { get; set; }

    public string? Notas { get; set; }

    public int? UsuariosAppId { get; set; }

    public bool? Estatus { get; set; }

    public DateTime? Inclusion { get; set; }

    public DateTime? InclusionHistorico { get; set; }
}
