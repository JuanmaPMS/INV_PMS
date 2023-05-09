using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblInventario
{
    public int Id { get; set; }

    public int TblAdquisicionId { get; set; }

    public int CatProductoId { get; set; }

    public int CatEstatusinventarioId { get; set; }

    public string Numerodeserie { get; set; } = null!;

    public string Inventarioclv { get; set; } = null!;

    public string? Notas { get; set; }

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatEstatusinventario CatEstatusinventario { get; set; } = null!;

    public virtual CatProducto CatProducto { get; set; } = null!;

    public virtual ICollection<RelInventarioUbicacion> RelInventarioUbicacions { get; } = new List<RelInventarioUbicacion>();

    public virtual ICollection<RelUsuarioInventario> RelUsuarioInventarios { get; } = new List<RelUsuarioInventario>();

    public virtual TblAdquisicion TblAdquisicion { get; set; } = null!;

    public virtual ICollection<TblInventarioAccesoriosincluido> TblInventarioAccesoriosincluidos { get; } = new List<TblInventarioAccesoriosincluido>();

    public virtual ICollection<TblInventarioArrendamiento> TblInventarioArrendamientos { get; } = new List<TblInventarioArrendamiento>();

    public virtual ICollection<TblInventarioUbicacion> TblInventarioUbicacions { get; } = new List<TblInventarioUbicacion>();
}
