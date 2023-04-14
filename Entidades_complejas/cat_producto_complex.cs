using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;

public class cat_producto_complex
{
    public int? Id { get; set; }
    public int? CatCategoriaProductoId { get; set; }
    public int? CatFabricanteId { get; set; }
    public string? Modelo { get; set; } = null!;
    public int? Anio { get; set; }
    public bool? Nuevo { get; set; }
    public int? Vidautil { get; set; }
    public List<String>? Caracteristicas_ { get; set; }
}

public class rel_producto_caracteristicas_complex
{
    public int? Id { get; set; }
    public int CatProductoId { get; set; }
    public string Nombre { get; set; } = null!;
}
