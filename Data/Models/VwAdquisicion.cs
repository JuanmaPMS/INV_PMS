using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwAdquisicion
{
    public int Idadquisicion { get; set; }

    public int Idpropietario { get; set; }

    public string Propietario { get; set; } = null!;

    public int Idproveedor { get; set; }

    public string Proveedor { get; set; } = null!;

    public string Rfcproveedor { get; set; } = null!;

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

    public int Cantidad { get; set; }

    public double Costosiunitario { get; set; }

    public double Montototaladqusicion { get; set; }

    public double Impuesto { get; set; }

    public string FacPdf { get; set; } = null!;

    public string FacXml { get; set; } = null!;
}
