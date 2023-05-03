using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelArchivosEmpleadoInventarioArrendamiento
{
    public int Id { get; set; }

    public int RelEmpleadoInventarioArrendamientoId { get; set; }

    public string Archivo { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual RelEmpleadoInventarioArrendamiento RelEmpleadoInventarioArrendamiento { get; set; } = null!;
}
