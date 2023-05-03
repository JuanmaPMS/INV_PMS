using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelUsuarioInventario
{
    public int Id { get; set; }

    public int CatUsuarioId { get; set; }

    public int TblInventarioId { get; set; }

    public string Responsiva { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatUsuario CatUsuario { get; set; } = null!;

    public virtual ICollection<RelUsuarioInventarioConfiguracion> RelUsuarioInventarioConfiguracions { get; } = new List<RelUsuarioInventarioConfiguracion>();

    public virtual TblInventario TblInventario { get; set; } = null!;

    public virtual ICollection<TblMantenimientoInventario> TblMantenimientoInventarios { get; } = new List<TblMantenimientoInventario>();
}
