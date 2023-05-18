using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwHistoricoInventario
{
    public int Id { get; set; }

    public int TblInventarioId { get; set; }

    public string? Modelo { get; set; }

    public string? Numerodeserie { get; set; }

    public string? Inventarioclv { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public int? UsuariosAppId { get; set; }

    public string Nombreusuario { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public DateTime Inclusion { get; set; }
}
