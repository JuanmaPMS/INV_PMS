﻿using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatCliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Sigla { get; set; } = null!;

    public string Razonsocial { get; set; } = null!;

    public string Rfc { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Latitud { get; set; } = null!;

    public string Longitud { get; set; } = null!;

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual ICollection<CatDirLdap> CatDirLdaps { get; } = new List<CatDirLdap>();

    public virtual ICollection<TblClienteUbicacionArrendamiento> TblClienteUbicacionArrendamientos { get; } = new List<TblClienteUbicacionArrendamiento>();

    public virtual ICollection<TblClienteUbicacion> TblClienteUbicacions { get; } = new List<TblClienteUbicacion>();

    public virtual ICollection<TblInventarioArrendamiento> TblInventarioArrendamientos { get; } = new List<TblInventarioArrendamiento>();

    public virtual ICollection<TblInventarioUbicacionArrendamiento> TblInventarioUbicacionArrendamientos { get; } = new List<TblInventarioUbicacionArrendamiento>();

    public virtual ICollection<TblInventarioUbicacion> TblInventarioUbicacions { get; } = new List<TblInventarioUbicacion>();

    public virtual ICollection<TblMantenimientoNotificacion> TblMantenimientoNotificacions { get; } = new List<TblMantenimientoNotificacion>();
}
