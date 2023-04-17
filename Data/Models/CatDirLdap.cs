using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class CatDirLdap
{
    public int Id { get; set; }

    public int CatClienteId { get; set; }

    public string? DirEntry { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatCliente CatCliente { get; set; } = null!;
}
