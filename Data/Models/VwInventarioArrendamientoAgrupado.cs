using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwInventarioArrendamientoAgrupado
{
    public int? Cantidad { get; set; }

    public int? Idproducto { get; set; }

    public int? Idfabricante { get; set; }

    public string? Fabricante { get; set; }

    public string? Modelo { get; set; }

    public int? Idcategoria { get; set; }

    public string? Categoria { get; set; }

    public bool? Esestatico { get; set; }

    public int? Anio { get; set; }

    public bool? Nuevo { get; set; }

    public int? Vidautil { get; set; }

    public string? Caracteristicas { get; set; }

    public int CatEstatusinventarioId { get; set; }

    public string CatEstatusinventario { get; set; } = null!;

    public int Idcliente { get; set; }

    public string Cliente { get; set; } = null!;

    public string Direccioncliente { get; set; } = null!;
}
