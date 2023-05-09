using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwInventarioUbicacion
{
    public int Id { get; set; }

    public int CatProductoId { get; set; }

    public int CatCategoriaProductoId { get; set; }

    public string Categoria { get; set; } = null!;

    public bool Estatico { get; set; }

    public int CatFabricanteId { get; set; }

    public string Fabricante { get; set; } = null!;

    public string Modelo { get; set; } = null!;

    public int? Anio { get; set; }

    public bool Nuevo { get; set; }

    public int Vidautil { get; set; }

    public string Numerodeserie { get; set; } = null!;

    public string Inventarioclv { get; set; } = null!;

    public int CatEstatusinventarioId { get; set; }

    public string Estatus { get; set; } = null!;

    public int CatUsuarioId { get; set; }

    public string UsuarioNombre { get; set; } = null!;

    public string UsuarioCuenta { get; set; } = null!;

    public string UsuarioCorreo { get; set; } = null!;

    public string Cliente { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Edificio { get; set; } = null!;

    public string Piso { get; set; } = null!;

    public string Oficina { get; set; } = null!;

    public string Filtro { get; set; } = null!;
}
