using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblAdquisicion
{
    public int Id { get; set; }

    public int CatProveedorId { get; set; }

    public int CatPropietarioId { get; set; }

    public double Monto { get; set; }

    public double Impuesto { get; set; }

    public int Articulos { get; set; }

    public string FacPdf { get; set; } = null!;

    public string FacXml { get; set; } = null!;

    public DateTime Fechadecompra { get; set; }

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatPropietario CatPropietario { get; set; } = null!;

    public virtual CatProveedor CatProveedor { get; set; } = null!;

    public virtual ICollection<RelAdquisicionDetalle> RelAdquisicionDetalles { get; } = new List<RelAdquisicionDetalle>();

    public virtual ICollection<TblInventario> TblInventarios { get; } = new List<TblInventario>();
}
