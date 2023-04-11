using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models;
public class adquisicion_producto_complex: tbl_adquisicion_complex
{
    public cat_producto_complex? Producto { get; set; }
}

