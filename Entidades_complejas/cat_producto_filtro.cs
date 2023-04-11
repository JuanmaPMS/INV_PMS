using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;

public class cat_producto_filtro
{
    public int? Id { get; set; }
    public int? CatCategoriaProductoId { get; set; }
    public int? CatFabricanteId { get; set; }
    public string? Modelo { get; set; } = null!;
    public int? Anio { get; set; }
    public bool? Nuevo { get; set; }
    public int? Vidautil { get; set; }
}
