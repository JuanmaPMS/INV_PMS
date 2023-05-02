using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class TblInventarioArrendamiento
{
    public int Id { get; set; }

    public int TblInventarioId { get; set; }

    public int CatClienteId { get; set; }

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatCliente CatCliente { get; set; } = null!;

    public virtual ICollection<RelEmpleadoInventarioArrendamiento> RelEmpleadoInventarioArrendamientos { get; } = new List<RelEmpleadoInventarioArrendamiento>();

    public virtual TblInventario TblInventario { get; set; } = null!;
}
