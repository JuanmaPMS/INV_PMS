using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwCatProducto
{
    public int Idproducto { get; set; }

    public int Idfabricante { get; set; }

    public string Fabricante { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public int Idcategoria { get; set; }

    public string Categoria { get; set; } = null!;

    public bool Esestatico { get; set; }

    public int? Anio { get; set; }

    public bool Nuevo { get; set; }

    public int Vidautil { get; set; }

    public string? Caracteristicas { get; set; }
}
