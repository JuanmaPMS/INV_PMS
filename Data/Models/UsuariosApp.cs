using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class UsuariosApp
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? Activo { get; set; }

    public DateTime? Inclusion { get; set; }

    public virtual ICollection<TblHistoricoInventario> TblHistoricoInventarios { get; } = new List<TblHistoricoInventario>();
}
