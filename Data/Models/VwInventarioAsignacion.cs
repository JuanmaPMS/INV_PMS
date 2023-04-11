﻿using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class VwInventarioAsignacion
{
    public int Idusuario { get; set; }

    public string? Nombreusuario { get; set; }

    public string? Cuentausuario { get; set; }

    public string? Correousuario { get; set; }

    public string? Direccionusuario { get; set; }

    public bool Estatususuario { get; set; }

    public int Idinventario { get; set; }

    public int Idadquisicion { get; set; }

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

    public string Numerodeserie { get; set; } = null!;

    public string Inventarioclv { get; set; } = null!;

    public string? Notainventario { get; set; }

    public string CatEstatusinventario { get; set; } = null!;

    public string Cliente { get; set; } = null!;

    public string Direccioncliente { get; set; } = null!;

    public string Latitudcliente { get; set; } = null!;

    public string Longitudcliente { get; set; } = null!;

    public string Edificio { get; set; } = null!;

    public string Piso { get; set; } = null!;

    public string Oficina { get; set; } = null!;

    public string? Autorizasalida { get; set; }

    public string? Autorizaentrada { get; set; }

    public string? Ubicacionnotas { get; set; }

    public string? Accesorios { get; set; }
}
