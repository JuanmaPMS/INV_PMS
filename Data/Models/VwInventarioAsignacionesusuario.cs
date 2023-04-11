using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwInventarioAsignacionesusuario
{
    public int Idinventario { get; set; }

    public int Idelementohardwareinfra { get; set; }

    public string Articulo { get; set; } = null!;

    public string Categoria { get; set; } = null!;

    public int Idelemento { get; set; }

    public string Modelo { get; set; } = null!;

    public int Anio { get; set; }

    public string Fabricante { get; set; } = null!;

    public int Vidautil { get; set; }

    public string Observaciones { get; set; } = null!;

    public string? Numerofolio { get; set; }

    public string? Numeroserie { get; set; }

    public double Cunitario { get; set; }

    public bool Garantia { get; set; }

    public string Garantiafile { get; set; } = null!;

    public string Propietario { get; set; } = null!;

    public string Estatus { get; set; } = null!;

    public int Idusuario { get; set; }

    public string? Nombre { get; set; }

    public string? Cuenta { get; set; }

    public string? Correo { get; set; }

    public string? Ubicaciontrabajo { get; set; }
}
