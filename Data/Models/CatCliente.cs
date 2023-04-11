using System;
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

    public virtual ICollection<TblInventarioUbicacion> TblInventarioUbicacions { get; } = new List<TblInventarioUbicacion>();
}
