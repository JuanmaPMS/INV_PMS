using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblInventarioHistorico
{
    public int Id { get; set; }

    public string? TipoObjeto { get; set; }

    public string? Objeto { get; set; }

    public int? UsuariosAppId { get; set; }

    public DateTime? InclusionHistorico { get; set; }
}
