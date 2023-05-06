using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwUsuarioInventario
{
    public int Idrelusuarioinventario { get; set; }

    public string Responsiva { get; set; } = null!;

    public int Idusuario { get; set; }

    public string? Nombreusuario { get; set; }

    public bool Estatus { get; set; }

    public int Idinventario { get; set; }

    public int Idadquisicion { get; set; }

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

    public string Numerodeserie { get; set; } = null!;

    public string Inventarioclv { get; set; } = null!;

    public string? Notainventario { get; set; }

    public int CatEstatusinventarioId { get; set; }

    public string CatEstatusinventario { get; set; } = null!;

    public string? Accesorios { get; set; }
}
