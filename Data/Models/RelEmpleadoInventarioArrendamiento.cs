using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelEmpleadoInventarioArrendamiento
{
    public int Id { get; set; }

    public int TblInventarioArrendamientoId { get; set; }

    public string CuentaEmpleadoCliente { get; set; } = null!;

    public string? NombreEmpleadoCliente { get; set; }

    public string? Responsiva { get; set; }

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual ICollection<RelArchivosEmpleadoInventarioArrendamiento> RelArchivosEmpleadoInventarioArrendamientos { get; } = new List<RelArchivosEmpleadoInventarioArrendamiento>();

    public virtual ICollection<RelEmpleadoInventarioArrendamientoConfiguracion> RelEmpleadoInventarioArrendamientoConfiguracions { get; } = new List<RelEmpleadoInventarioArrendamientoConfiguracion>();

    public virtual TblInventarioArrendamiento TblInventarioArrendamiento { get; set; } = null!;
}
