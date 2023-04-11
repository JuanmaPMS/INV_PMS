using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatProducto
{
    public int Id { get; set; }

    public int CatCategoriaProductoId { get; set; }

    public int CatFabricanteId { get; set; }

    public string Modelo { get; set; } = null!;

    public int? Anio { get; set; }

    public bool Nuevo { get; set; }

    public int Vidautil { get; set; }

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatCategoriaProducto CatCategoriaProducto { get; set; } = null!;

    public virtual CatFabricante CatFabricante { get; set; } = null!;

    public virtual ICollection<RelAdquisicionDetalle> RelAdquisicionDetalles { get; } = new List<RelAdquisicionDetalle>();

    public virtual ICollection<RelProductoCatacteristica> RelProductoCatacteristicas { get; } = new List<RelProductoCatacteristica>();

    public virtual ICollection<TblInventario> TblInventarios { get; } = new List<TblInventario>();
}
