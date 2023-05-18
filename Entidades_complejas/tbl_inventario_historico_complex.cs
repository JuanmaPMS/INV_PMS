using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades_complejas
{
    public class tbl_inventario_historico_complex
    {
        public int Id { get; set; }
        public string? TipoObjeto { get; set; }
        public string? Objeto { get; set; }
        public int? UsuariosAppId { get; set; }
        public string? NombreUsuario { get; set; }
        public DateTime? InclusionHistorico { get; set; }


    }

}
