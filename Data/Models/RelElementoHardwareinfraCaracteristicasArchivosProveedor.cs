using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class RelElementoHardwareinfraCaracteristicasArchivosProveedor
{
    public int Id { get; set; }

    public int CatElementoHardwareinfraId { get; set; }

    public int TblCaracteristicasElementoHardwareinfraId { get; set; }

    public int TblArchivosElementoHardwareinfraId { get; set; }

    public int RelProveedorContactosoporteId { get; set; }

    public int RelCategoriaFamiliaArticuloId { get; set; }

    public bool? Estatus { get; set; }

    public DateTime Inclusion { get; set; }

    public virtual CatElementoHardwareinfra CatElementoHardwareinfra { get; set; } = null!;

    public virtual ICollection<RelAdquisicionesHardwareinfra> RelAdquisicionesHardwareinfras { get; } = new List<RelAdquisicionesHardwareinfra>();

    public virtual RelCategoriaFamiliaArticulo RelCategoriaFamiliaArticulo { get; set; } = null!;

    public virtual RelProveedorContactosoporte RelProveedorContactosoporte { get; set; } = null!;

    public virtual TblArchivosElementoHardwareinfra TblArchivosElementoHardwareinfra { get; set; } = null!;

    public virtual TblCaracteristicasElementoHardwareinfra TblCaracteristicasElementoHardwareinfra { get; set; } = null!;

    public virtual ICollection<TblInventario> TblInventarios { get; } = new List<TblInventario>();
}
