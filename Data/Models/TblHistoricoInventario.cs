using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblHistoricoInventario
{
    public int Id { get; set; }

    public int TblInventarioId { get; set; }

    public string Tipo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public int? UsuariosAppId { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual TblInventario TblInventario { get; set; } = null!;

    public virtual UsuariosApp? UsuariosApp { get; set; }
}
