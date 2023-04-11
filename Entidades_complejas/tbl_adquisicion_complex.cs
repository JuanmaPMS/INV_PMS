﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;
public class tbl_adquisicion_complex
{
    public int? Id { get; set; }
    public int? CatProveedorId { get; set; }
    public int? CatPropietarioId { get; set; }
    public double? Monto { get; set; }
    public double? Impuesto { get; set; }
    public int? Articulos { get; set; }
    public string? FacPdf { get; set; } = null!;
    public string? FacXml { get; set; } = null!;
    public DateTime? Fechadecompra { get; set; }
}

