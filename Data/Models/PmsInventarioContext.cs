using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public partial class PmsInventarioContext : DbContext
{
    public PmsInventarioContext()
    {
    }

    public PmsInventarioContext(DbContextOptions<PmsInventarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CatCategoriaFamiliaArticulo> CatCategoriaFamiliaArticulos { get; set; }

    public virtual DbSet<CatCategoriaProducto> CatCategoriaProductos { get; set; }

    public virtual DbSet<CatCliente> CatClientes { get; set; }

    public virtual DbSet<CatContactosoporte> CatContactosoportes { get; set; }

    public virtual DbSet<CatDirLdap> CatDirLdaps { get; set; }

    public virtual DbSet<CatEstatusinventario> CatEstatusinventarios { get; set; }

    public virtual DbSet<CatFabricante> CatFabricantes { get; set; }

    public virtual DbSet<CatFamiliaArticulo> CatFamiliaArticulos { get; set; }

    public virtual DbSet<CatProducto> CatProductos { get; set; }

    public virtual DbSet<CatPropietario> CatPropietarios { get; set; }

    public virtual DbSet<CatProveedor> CatProveedors { get; set; }

    public virtual DbSet<CatSoftware> CatSoftwares { get; set; }

    public virtual DbSet<CatUsuario> CatUsuarios { get; set; }

    public virtual DbSet<RelAdquisicionDetalle> RelAdquisicionDetalles { get; set; }

    public virtual DbSet<RelAdquisicionesSoftwareProveedor> RelAdquisicionesSoftwareProveedors { get; set; }

    public virtual DbSet<RelCategoriaFamiliaArticulo> RelCategoriaFamiliaArticulos { get; set; }

    public virtual DbSet<RelProductoCatacteristica> RelProductoCatacteristicas { get; set; }

    public virtual DbSet<RelProveedorContactosoporte> RelProveedorContactosoportes { get; set; }

    public virtual DbSet<RelUsuarioInventario> RelUsuarioInventarios { get; set; }

    public virtual DbSet<TblAdquisicion> TblAdquisicions { get; set; }

    public virtual DbSet<TblInventario> TblInventarios { get; set; }

    public virtual DbSet<TblInventarioAccesoriosincluido> TblInventarioAccesoriosincluidos { get; set; }

    public virtual DbSet<TblInventarioUbicacion> TblInventarioUbicacions { get; set; }

    public virtual DbSet<TblNotasUsuarioInventario> TblNotasUsuarioInventarios { get; set; }

    public virtual DbSet<VwAdquisicion> VwAdquisicions { get; set; }

    public virtual DbSet<VwCatProducto> VwCatProductos { get; set; }

    public virtual DbSet<VwFamiliaArticuloCategorium> VwFamiliaArticuloCategoria { get; set; }

    public virtual DbSet<VwInventario> VwInventarios { get; set; }

    public virtual DbSet<VwInventarioAsignacion> VwInventarioAsignacions { get; set; }

    public virtual DbSet<VwProveedorSoporte> VwProveedorSoportes { get; set; }

    public virtual DbSet<VwSoftwareProveedorsoporteAdquisicion> VwSoftwareProveedorsoporteAdquisicions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=198.251.71.105;user=juanma;password=T3st_sqlI55;database=pms_inventario;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CatCategoriaFamiliaArticulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_CATE__3214EC278EE4CF2A");

            entity.ToTable("CAT_CATEGORIA_FAMILIA_ARTICULO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Categoria)
                .HasMaxLength(500)
                .HasColumnName("CATEGORIA");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(2000)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
        });

        modelBuilder.Entity<CatCategoriaProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_CATE__3214EC27438BD881");

            entity.ToTable("CAT_CATEGORIA_PRODUCTO");

            entity.HasIndex(e => new { e.Nombre, e.Estatico }, "UQ_CAT_CATEGORIA_PRODUCTO").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estatico).HasColumnName("ESTATICO");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<CatCliente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_CLIE__3214EC278D1FE64C");

            entity.ToTable("CAT_CLIENTE");

            entity.HasIndex(e => e.Nombre, "UQ__CAT_CLIE__B21D0AB943F000FB").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Direccion)
                .HasMaxLength(500)
                .HasColumnName("DIRECCION");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Latitud)
                .HasMaxLength(500)
                .HasColumnName("LATITUD");
            entity.Property(e => e.Longitud)
                .HasMaxLength(500)
                .HasColumnName("LONGITUD");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Razonsocial)
                .HasMaxLength(500)
                .HasColumnName("RAZONSOCIAL");
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .HasColumnName("RFC");
            entity.Property(e => e.Sigla)
                .HasMaxLength(50)
                .HasColumnName("SIGLA");
        });

        modelBuilder.Entity<CatContactosoporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_CONT__3214EC274970873D");

            entity.ToTable("CAT_CONTACTOSOPORTE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Correo)
                .HasMaxLength(500)
                .HasColumnName("CORREO");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Telefono)
                .HasMaxLength(500)
                .HasColumnName("TELEFONO");
        });

        modelBuilder.Entity<CatDirLdap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_DIR___3214EC2778AA25F2");

            entity.ToTable("CAT_DIR_LDAP");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CatClienteId).HasColumnName("CAT_CLIENTE_ID");
            entity.Property(e => e.DirEntry)
                .HasMaxLength(500)
                .HasColumnName("DIR_ENTRY");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");

            entity.HasOne(d => d.CatCliente).WithMany(p => p.CatDirLdaps)
                .HasForeignKey(d => d.CatClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CAT_DIR_L__CAT_C__5D2BD0E6");
        });

        modelBuilder.Entity<CatEstatusinventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_ESTA__3214EC27ADA72C8C");

            entity.ToTable("CAT_ESTATUSINVENTARIO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estatus)
                .HasMaxLength(500)
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
        });

        modelBuilder.Entity<CatFabricante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_FABR__3214EC2772CAA5B5");

            entity.ToTable("CAT_FABRICANTE");

            entity.HasIndex(e => e.Nombre, "UQ__CAT_FABR__B21D0AB9DAE82E62").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<CatFamiliaArticulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_FAMI__3214EC27C5FF442D");

            entity.ToTable("CAT_FAMILIA_ARTICULO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Articulo)
                .HasMaxLength(500)
                .HasColumnName("ARTICULO");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(2000)
                .HasColumnName("DESCRIPCION");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
        });

        modelBuilder.Entity<CatProducto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_PROD__3214EC277AFC1A6B");

            entity.ToTable("CAT_PRODUCTO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Anio).HasColumnName("ANIO");
            entity.Property(e => e.CatCategoriaProductoId).HasColumnName("CAT_CATEGORIA_PRODUCTO_ID");
            entity.Property(e => e.CatFabricanteId).HasColumnName("CAT_FABRICANTE_ID");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .HasColumnName("MODELO");
            entity.Property(e => e.Nuevo).HasColumnName("NUEVO");
            entity.Property(e => e.Vidautil).HasColumnName("VIDAUTIL");

            entity.HasOne(d => d.CatCategoriaProducto).WithMany(p => p.CatProductos)
                .HasForeignKey(d => d.CatCategoriaProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CAT_PRODU__CAT_C__5A846E65");

            entity.HasOne(d => d.CatFabricante).WithMany(p => p.CatProductos)
                .HasForeignKey(d => d.CatFabricanteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CAT_PRODU__CAT_F__5B78929E");
        });

        modelBuilder.Entity<CatPropietario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_PROP__3214EC271E88D507");

            entity.ToTable("CAT_PROPIETARIO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Razonsocial)
                .HasMaxLength(500)
                .HasColumnName("RAZONSOCIAL");
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .HasColumnName("RFC");
            entity.Property(e => e.Sigla)
                .HasMaxLength(50)
                .HasColumnName("SIGLA");
        });

        modelBuilder.Entity<CatProveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_PROV__3214EC275BD5BE85");

            entity.ToTable("CAT_PROVEEDOR");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Correo)
                .HasMaxLength(500)
                .HasColumnName("CORREO");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Razonsocial)
                .HasMaxLength(500)
                .HasColumnName("RAZONSOCIAL");
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .HasColumnName("RFC");
        });

        modelBuilder.Entity<CatSoftware>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_SOFT__3214EC278E166710");

            entity.ToTable("CAT_SOFTWARE");

            entity.HasIndex(e => e.Nombre, "UQ__CAT_SOFT__B21D0AB9C7778702").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Fabricante)
                .HasMaxLength(500)
                .HasColumnName("FABRICANTE");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("NOMBRE");
        });

        modelBuilder.Entity<CatUsuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CAT_USUA__3214EC27B85AE7BD");

            entity.ToTable("CAT_USUARIO");

            entity.HasIndex(e => new { e.Cuenta, e.Correo }, "UQ_CAT_USUARIO_").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Correo)
                .HasMaxLength(500)
                .HasColumnName("CORREO");
            entity.Property(e => e.Cuenta)
                .HasMaxLength(500)
                .HasColumnName("CUENTA");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.Ubicacion).HasColumnName("UBICACION");
        });

        modelBuilder.Entity<RelAdquisicionDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__REL_ADQU__3214EC27EDA896AB");

            entity.ToTable("REL_ADQUISICION_DETALLE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");
            entity.Property(e => e.CatProductoId).HasColumnName("CAT_PRODUCTO_ID");
            entity.Property(e => e.Costosiunitario).HasColumnName("COSTOSIUNITARIO");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.TblAdquisicionId).HasColumnName("TBL_ADQUISICION_ID");

            entity.HasOne(d => d.CatProducto).WithMany(p => p.RelAdquisicionDetalles)
                .HasForeignKey(d => d.CatProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__REL_ADQUI__CAT_P__66EA454A");

            entity.HasOne(d => d.TblAdquisicion).WithMany(p => p.RelAdquisicionDetalles)
                .HasForeignKey(d => d.TblAdquisicionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__REL_ADQUI__TBL_A__65F62111");
        });

        modelBuilder.Entity<RelAdquisicionesSoftwareProveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__REL_ADQU__3214EC275AA1F33A");

            entity.ToTable("REL_ADQUISICIONES_SOFTWARE_PROVEEDOR");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CatSoftwareId).HasColumnName("CAT_SOFTWARE_ID");
            entity.Property(e => e.Cunitario).HasColumnName("CUNITARIO");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Facpdf)
                .HasMaxLength(500)
                .HasColumnName("FACPDF");
            entity.Property(e => e.Facxml)
                .HasMaxLength(500)
                .HasColumnName("FACXML");
            entity.Property(e => e.Garantia).HasColumnName("GARANTIA");
            entity.Property(e => e.Garantiafile)
                .HasMaxLength(500)
                .HasColumnName("GARANTIAFILE");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.RelProveedorContactosoporteId).HasColumnName("REL_PROVEEDOR_CONTACTOSOPORTE_ID");
            entity.Property(e => e.Unidades).HasColumnName("UNIDADES");

            entity.HasOne(d => d.CatSoftware).WithMany(p => p.RelAdquisicionesSoftwareProveedors)
                .HasForeignKey(d => d.CatSoftwareId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__REL_ADQUI__CAT_S__5AEE82B9");

            entity.HasOne(d => d.RelProveedorContactosoporte).WithMany(p => p.RelAdquisicionesSoftwareProveedors)
                .HasForeignKey(d => d.RelProveedorContactosoporteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__REL_ADQUI__REL_P__5BE2A6F2");
        });

        modelBuilder.Entity<RelCategoriaFamiliaArticulo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__REL_CATE__3214EC27D6DD23C7");

            entity.ToTable("REL_CATEGORIA_FAMILIA_ARTICULO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CatCategoriaFamiliaArticuloId).HasColumnName("CAT_CATEGORIA_FAMILIA_ARTICULO_ID");
            entity.Property(e => e.CatFamiliaArticuloId).HasColumnName("CAT_FAMILIA_ARTICULO_ID");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");

            entity.HasOne(d => d.CatCategoriaFamiliaArticulo).WithMany(p => p.RelCategoriaFamiliaArticulos)
                .HasForeignKey(d => d.CatCategoriaFamiliaArticuloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__REL_CATEG__CAT_C__6B24EA82");

            entity.HasOne(d => d.CatFamiliaArticulo).WithMany(p => p.RelCategoriaFamiliaArticulos)
                .HasForeignKey(d => d.CatFamiliaArticuloId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__REL_CATEG__CAT_F__6C190EBB");
        });

        modelBuilder.Entity<RelProductoCatacteristica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__REL_PROD__3214EC277F3817F7");

            entity.ToTable("REL_PRODUCTO_CATACTERISTICAS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CatProductoId).HasColumnName("CAT_PRODUCTO_ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("NOMBRE");

            entity.HasOne(d => d.CatProducto).WithMany(p => p.RelProductoCatacteristicas)
                .HasForeignKey(d => d.CatProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__REL_PRODU__CAT_P__2F650636");
        });

        modelBuilder.Entity<RelProveedorContactosoporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__REL_PROV__3214EC2771FAF599");

            entity.ToTable("REL_PROVEEDOR_CONTACTOSOPORTE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CatContactosoporteId).HasColumnName("CAT_CONTACTOSOPORTE_ID");
            entity.Property(e => e.CatProveedorId).HasColumnName("CAT_PROVEEDOR_ID");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");

            entity.HasOne(d => d.CatContactosoporte).WithMany(p => p.RelProveedorContactosoportes)
                .HasForeignKey(d => d.CatContactosoporteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__REL_PROVE__CAT_C__48CFD27E");

            entity.HasOne(d => d.CatProveedor).WithMany(p => p.RelProveedorContactosoportes)
                .HasForeignKey(d => d.CatProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__REL_PROVE__CAT_P__47DBAE45");
        });

        modelBuilder.Entity<RelUsuarioInventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__REL_USUA__3214EC2760A69DFC");

            entity.ToTable("REL_USUARIO_INVENTARIO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CatUsuarioId).HasColumnName("CAT_USUARIO_ID");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Responsiva)
                .HasMaxLength(500)
                .HasColumnName("RESPONSIVA");
            entity.Property(e => e.TblInventarioId).HasColumnName("TBL_INVENTARIO_ID");

            entity.HasOne(d => d.CatUsuario).WithMany(p => p.RelUsuarioInventarios)
                .HasForeignKey(d => d.CatUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__REL_USUAR__CAT_U__24E777C3");

            entity.HasOne(d => d.TblInventario).WithMany(p => p.RelUsuarioInventarios)
                .HasForeignKey(d => d.TblInventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__REL_USUAR__TBL_I__25DB9BFC");
        });

        modelBuilder.Entity<TblAdquisicion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TBL_ADQU__3214EC2763AA05CC");

            entity.ToTable("TBL_ADQUISICION");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Articulos).HasColumnName("ARTICULOS");
            entity.Property(e => e.CatPropietarioId).HasColumnName("CAT_PROPIETARIO_ID");
            entity.Property(e => e.CatProveedorId).HasColumnName("CAT_PROVEEDOR_ID");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.FacPdf)
                .HasMaxLength(500)
                .HasColumnName("FAC_PDF");
            entity.Property(e => e.FacXml)
                .HasMaxLength(500)
                .HasColumnName("FAC_XML");
            entity.Property(e => e.Fechadecompra)
                .HasColumnType("datetime")
                .HasColumnName("FECHADECOMPRA");
            entity.Property(e => e.Impuesto).HasColumnName("IMPUESTO");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Monto).HasColumnName("MONTO");

            entity.HasOne(d => d.CatPropietario).WithMany(p => p.TblAdquisicions)
                .HasForeignKey(d => d.CatPropietarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TBL_ADQUI__CAT_P__61316BF4");

            entity.HasOne(d => d.CatProveedor).WithMany(p => p.TblAdquisicions)
                .HasForeignKey(d => d.CatProveedorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TBL_ADQUI__CAT_P__603D47BB");
        });

        modelBuilder.Entity<TblInventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TBL_INVE__3214EC27EFBD6443");

            entity.ToTable("TBL_INVENTARIO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CatEstatusinventarioId).HasColumnName("CAT_ESTATUSINVENTARIO_ID");
            entity.Property(e => e.CatProductoId).HasColumnName("CAT_PRODUCTO_ID");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Inventarioclv)
                .HasMaxLength(500)
                .HasColumnName("INVENTARIOCLV");
            entity.Property(e => e.Notas).HasColumnName("NOTAS");
            entity.Property(e => e.Numerodeserie)
                .HasMaxLength(500)
                .HasColumnName("NUMERODESERIE");
            entity.Property(e => e.TblAdquisicionId).HasColumnName("TBL_ADQUISICION_ID");

            entity.HasOne(d => d.CatEstatusinventario).WithMany(p => p.TblInventarios)
                .HasForeignKey(d => d.CatEstatusinventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TBL_INVEN__CAT_E__0C1BC9F9");

            entity.HasOne(d => d.CatProducto).WithMany(p => p.TblInventarios)
                .HasForeignKey(d => d.CatProductoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TBL_INVEN__CAT_P__0B27A5C0");

            entity.HasOne(d => d.TblAdquisicion).WithMany(p => p.TblInventarios)
                .HasForeignKey(d => d.TblAdquisicionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TBL_INVEN__TBL_A__0A338187");
        });

        modelBuilder.Entity<TblInventarioAccesoriosincluido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TBL_INVE__3214EC27A3370EBB");

            entity.ToTable("TBL_INVENTARIO_ACCESORIOSINCLUIDOS");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Detalle)
                .HasMaxLength(500)
                .HasColumnName("DETALLE");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.TblInventarioId).HasColumnName("TBL_INVENTARIO_ID");

            entity.HasOne(d => d.TblInventario).WithMany(p => p.TblInventarioAccesoriosincluidos)
                .HasForeignKey(d => d.TblInventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TBL_INVEN__TBL_I__10E07F16");
        });

        modelBuilder.Entity<TblInventarioUbicacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TBL_INVE__3214EC27C90CEDE9");

            entity.ToTable("TBL_INVENTARIO_UBICACION");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Autorizaentrada)
                .HasMaxLength(500)
                .HasColumnName("AUTORIZAENTRADA");
            entity.Property(e => e.Autorizasalida)
                .HasMaxLength(500)
                .HasColumnName("AUTORIZASALIDA");
            entity.Property(e => e.CatClienteId).HasColumnName("CAT_CLIENTE_ID");
            entity.Property(e => e.Edificio)
                .HasMaxLength(500)
                .HasColumnName("EDIFICIO");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Notas)
                .HasMaxLength(500)
                .HasColumnName("NOTAS");
            entity.Property(e => e.Oficina)
                .HasMaxLength(500)
                .HasColumnName("OFICINA");
            entity.Property(e => e.Piso)
                .HasMaxLength(500)
                .HasColumnName("PISO");
            entity.Property(e => e.TblInventarioId).HasColumnName("TBL_INVENTARIO_ID");

            entity.HasOne(d => d.CatCliente).WithMany(p => p.TblInventarioUbicacions)
                .HasForeignKey(d => d.CatClienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TBL_INVEN__CAT_C__1975C517");

            entity.HasOne(d => d.TblInventario).WithMany(p => p.TblInventarioUbicacions)
                .HasForeignKey(d => d.TblInventarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__TBL_INVEN__TBL_I__1881A0DE");
        });

        modelBuilder.Entity<TblNotasUsuarioInventario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TBL_NOTA__3214EC2700476AC0");

            entity.ToTable("TBL_NOTAS_USUARIO_INVENTARIO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Archivo)
                .HasMaxLength(500)
                .HasColumnName("ARCHIVO");
            entity.Property(e => e.Estatus)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("ESTATUS");
            entity.Property(e => e.Inclusion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("INCLUSION");
            entity.Property(e => e.Nota)
                .HasMaxLength(500)
                .HasColumnName("NOTA");
        });

        modelBuilder.Entity<VwAdquisicion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_ADQUISICION");

            entity.Property(e => e.Anio).HasColumnName("ANIO");
            entity.Property(e => e.Cantidad).HasColumnName("CANTIDAD");
            entity.Property(e => e.Caracteristicas).HasColumnName("CARACTERISTICAS");
            entity.Property(e => e.Categoria)
                .HasMaxLength(500)
                .HasColumnName("CATEGORIA");
            entity.Property(e => e.Costosiunitario).HasColumnName("COSTOSIUNITARIO");
            entity.Property(e => e.Esestatico).HasColumnName("ESESTATICO");
            entity.Property(e => e.Fabricante)
                .HasMaxLength(500)
                .HasColumnName("FABRICANTE");
            entity.Property(e => e.FacPdf)
                .HasMaxLength(500)
                .HasColumnName("FAC_PDF");
            entity.Property(e => e.FacXml)
                .HasMaxLength(500)
                .HasColumnName("FAC_XML");
            entity.Property(e => e.Idadquisicion).HasColumnName("IDADQUISICION");
            entity.Property(e => e.Idcategoria).HasColumnName("IDCATEGORIA");
            entity.Property(e => e.Idfabricante).HasColumnName("IDFABRICANTE");
            entity.Property(e => e.Idproducto).HasColumnName("IDPRODUCTO");
            entity.Property(e => e.Idpropietario).HasColumnName("IDPROPIETARIO");
            entity.Property(e => e.Idproveedor).HasColumnName("IDPROVEEDOR");
            entity.Property(e => e.Impuesto).HasColumnName("IMPUESTO");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .HasColumnName("MODELO");
            entity.Property(e => e.Montototaladqusicion).HasColumnName("MONTOTOTALADQUSICION");
            entity.Property(e => e.Nuevo).HasColumnName("NUEVO");
            entity.Property(e => e.Propietario)
                .HasMaxLength(500)
                .HasColumnName("PROPIETARIO");
            entity.Property(e => e.Proveedor)
                .HasMaxLength(500)
                .HasColumnName("PROVEEDOR");
            entity.Property(e => e.Rfcproveedor)
                .HasMaxLength(13)
                .HasColumnName("RFCPROVEEDOR");
            entity.Property(e => e.Vidautil).HasColumnName("VIDAUTIL");
        });

        modelBuilder.Entity<VwCatProducto>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_CAT_PRODUCTO");

            entity.Property(e => e.Anio).HasColumnName("ANIO");
            entity.Property(e => e.Caracteristicas).HasColumnName("CARACTERISTICAS");
            entity.Property(e => e.Categoria)
                .HasMaxLength(500)
                .HasColumnName("CATEGORIA");
            entity.Property(e => e.Esestatico).HasColumnName("ESESTATICO");
            entity.Property(e => e.Fabricante)
                .HasMaxLength(500)
                .HasColumnName("FABRICANTE");
            entity.Property(e => e.Idcategoria).HasColumnName("IDCATEGORIA");
            entity.Property(e => e.Idfabricante).HasColumnName("IDFABRICANTE");
            entity.Property(e => e.Idproducto).HasColumnName("IDPRODUCTO");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .HasColumnName("MODELO");
            entity.Property(e => e.Nuevo).HasColumnName("NUEVO");
            entity.Property(e => e.Vidautil).HasColumnName("VIDAUTIL");
        });

        modelBuilder.Entity<VwFamiliaArticuloCategorium>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_FAMILIA_ARTICULO_CATEGORIA");

            entity.Property(e => e.Categoria)
                .HasMaxLength(500)
                .HasColumnName("CATEGORIA");
            entity.Property(e => e.Categoriadescripcion)
                .HasMaxLength(2000)
                .HasColumnName("CATEGORIADESCRIPCION");
            entity.Property(e => e.Estatus).HasColumnName("ESTATUS");
            entity.Property(e => e.Familiaarticulo)
                .HasMaxLength(500)
                .HasColumnName("FAMILIAARTICULO");
            entity.Property(e => e.Idcategoria).HasColumnName("IDCATEGORIA");
            entity.Property(e => e.Idfamiliaarticulo).HasColumnName("IDFAMILIAARTICULO");
        });

        modelBuilder.Entity<VwInventario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_INVENTARIO");

            entity.Property(e => e.Accesorios).HasColumnName("ACCESORIOS");
            entity.Property(e => e.Anio).HasColumnName("ANIO");
            entity.Property(e => e.Autorizaentrada)
                .HasMaxLength(500)
                .HasColumnName("AUTORIZAENTRADA");
            entity.Property(e => e.Autorizasalida)
                .HasMaxLength(500)
                .HasColumnName("AUTORIZASALIDA");
            entity.Property(e => e.Caracteristicas).HasColumnName("CARACTERISTICAS");
            entity.Property(e => e.CatEstatusinventario)
                .HasMaxLength(500)
                .HasColumnName("CAT_ESTATUSINVENTARIO");
            entity.Property(e => e.Categoria)
                .HasMaxLength(500)
                .HasColumnName("CATEGORIA");
            entity.Property(e => e.Cliente)
                .HasMaxLength(500)
                .HasColumnName("CLIENTE");
            entity.Property(e => e.Direccioncliente)
                .HasMaxLength(500)
                .HasColumnName("DIRECCIONCLIENTE");
            entity.Property(e => e.Edificio)
                .HasMaxLength(500)
                .HasColumnName("EDIFICIO");
            entity.Property(e => e.Esestatico).HasColumnName("ESESTATICO");
            entity.Property(e => e.Fabricante)
                .HasMaxLength(500)
                .HasColumnName("FABRICANTE");
            entity.Property(e => e.Idadquisicion).HasColumnName("IDADQUISICION");
            entity.Property(e => e.Idcategoria).HasColumnName("IDCATEGORIA");
            entity.Property(e => e.Idfabricante).HasColumnName("IDFABRICANTE");
            entity.Property(e => e.Idinventario).HasColumnName("IDINVENTARIO");
            entity.Property(e => e.Idproducto).HasColumnName("IDPRODUCTO");
            entity.Property(e => e.Inventarioclv)
                .HasMaxLength(500)
                .HasColumnName("INVENTARIOCLV");
            entity.Property(e => e.Latitudcliente)
                .HasMaxLength(500)
                .HasColumnName("LATITUDCLIENTE");
            entity.Property(e => e.Longitudcliente)
                .HasMaxLength(500)
                .HasColumnName("LONGITUDCLIENTE");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .HasColumnName("MODELO");
            entity.Property(e => e.Notainventario).HasColumnName("NOTAINVENTARIO");
            entity.Property(e => e.Nuevo).HasColumnName("NUEVO");
            entity.Property(e => e.Numerodeserie)
                .HasMaxLength(500)
                .HasColumnName("NUMERODESERIE");
            entity.Property(e => e.Oficina)
                .HasMaxLength(500)
                .HasColumnName("OFICINA");
            entity.Property(e => e.Piso)
                .HasMaxLength(500)
                .HasColumnName("PISO");
            entity.Property(e => e.Ubicacionnotas)
                .HasMaxLength(500)
                .HasColumnName("UBICACIONNOTAS");
            entity.Property(e => e.Vidautil).HasColumnName("VIDAUTIL");
        });

        modelBuilder.Entity<VwInventarioAsignacion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_INVENTARIO_ASIGNACION");

            entity.Property(e => e.Accesorios).HasColumnName("ACCESORIOS");
            entity.Property(e => e.Anio).HasColumnName("ANIO");
            entity.Property(e => e.Autorizaentrada)
                .HasMaxLength(500)
                .HasColumnName("AUTORIZAENTRADA");
            entity.Property(e => e.Autorizasalida)
                .HasMaxLength(500)
                .HasColumnName("AUTORIZASALIDA");
            entity.Property(e => e.Caracteristicas).HasColumnName("CARACTERISTICAS");
            entity.Property(e => e.CatEstatusinventario)
                .HasMaxLength(500)
                .HasColumnName("CAT_ESTATUSINVENTARIO");
            entity.Property(e => e.Categoria)
                .HasMaxLength(500)
                .HasColumnName("CATEGORIA");
            entity.Property(e => e.Cliente)
                .HasMaxLength(500)
                .HasColumnName("CLIENTE");
            entity.Property(e => e.Correousuario)
                .HasMaxLength(500)
                .HasColumnName("CORREOUSUARIO");
            entity.Property(e => e.Cuentausuario)
                .HasMaxLength(500)
                .HasColumnName("CUENTAUSUARIO");
            entity.Property(e => e.Direccioncliente)
                .HasMaxLength(500)
                .HasColumnName("DIRECCIONCLIENTE");
            entity.Property(e => e.Direccionusuario).HasColumnName("DIRECCIONUSUARIO");
            entity.Property(e => e.Edificio)
                .HasMaxLength(500)
                .HasColumnName("EDIFICIO");
            entity.Property(e => e.Esestatico).HasColumnName("ESESTATICO");
            entity.Property(e => e.Estatususuario).HasColumnName("ESTATUSUSUARIO");
            entity.Property(e => e.Fabricante)
                .HasMaxLength(500)
                .HasColumnName("FABRICANTE");
            entity.Property(e => e.Idadquisicion).HasColumnName("IDADQUISICION");
            entity.Property(e => e.Idcategoria).HasColumnName("IDCATEGORIA");
            entity.Property(e => e.Idfabricante).HasColumnName("IDFABRICANTE");
            entity.Property(e => e.Idinventario).HasColumnName("IDINVENTARIO");
            entity.Property(e => e.Idproducto).HasColumnName("IDPRODUCTO");
            entity.Property(e => e.Idusuario).HasColumnName("IDUSUARIO");
            entity.Property(e => e.Inventarioclv)
                .HasMaxLength(500)
                .HasColumnName("INVENTARIOCLV");
            entity.Property(e => e.Latitudcliente)
                .HasMaxLength(500)
                .HasColumnName("LATITUDCLIENTE");
            entity.Property(e => e.Longitudcliente)
                .HasMaxLength(500)
                .HasColumnName("LONGITUDCLIENTE");
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .HasColumnName("MODELO");
            entity.Property(e => e.Nombreusuario)
                .HasMaxLength(500)
                .HasColumnName("NOMBREUSUARIO");
            entity.Property(e => e.Notainventario).HasColumnName("NOTAINVENTARIO");
            entity.Property(e => e.Nuevo).HasColumnName("NUEVO");
            entity.Property(e => e.Numerodeserie)
                .HasMaxLength(500)
                .HasColumnName("NUMERODESERIE");
            entity.Property(e => e.Oficina)
                .HasMaxLength(500)
                .HasColumnName("OFICINA");
            entity.Property(e => e.Piso)
                .HasMaxLength(500)
                .HasColumnName("PISO");
            entity.Property(e => e.Ubicacionnotas)
                .HasMaxLength(500)
                .HasColumnName("UBICACIONNOTAS");
            entity.Property(e => e.Vidautil).HasColumnName("VIDAUTIL");
        });

        modelBuilder.Entity<VwProveedorSoporte>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_PROVEEDOR_SOPORTE");

            entity.Property(e => e.Correo)
                .HasMaxLength(500)
                .HasColumnName("CORREO");
            entity.Property(e => e.Estatus).HasColumnName("ESTATUS");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idproveedor).HasColumnName("IDPROVEEDOR");
            entity.Property(e => e.Idsoporte).HasColumnName("IDSOPORTE");
            entity.Property(e => e.Nombrecorreo)
                .HasMaxLength(500)
                .HasColumnName("NOMBRECORREO");
            entity.Property(e => e.Razonsocial)
                .HasMaxLength(500)
                .HasColumnName("RAZONSOCIAL");
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .HasColumnName("RFC");
            entity.Property(e => e.Soportecorreo)
                .HasMaxLength(500)
                .HasColumnName("SOPORTECORREO");
            entity.Property(e => e.Telefonosporte)
                .HasMaxLength(500)
                .HasColumnName("TELEFONOSPORTE");
        });

        modelBuilder.Entity<VwSoftwareProveedorsoporteAdquisicion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_SOFTWARE_PROVEEDORSOPORTE_ADQUISICION");

            entity.Property(e => e.Cfdi)
                .HasMaxLength(500)
                .HasColumnName("CFDI");
            entity.Property(e => e.Correo)
                .HasMaxLength(500)
                .HasColumnName("CORREO");
            entity.Property(e => e.Cunitario).HasColumnName("CUNITARIO");
            entity.Property(e => e.Fabricante)
                .HasMaxLength(500)
                .HasColumnName("FABRICANTE");
            entity.Property(e => e.Garantia).HasColumnName("GARANTIA");
            entity.Property(e => e.Garantiafile)
                .HasMaxLength(500)
                .HasColumnName("GARANTIAFILE");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Idproveedorsoporte).HasColumnName("IDPROVEEDORSOPORTE");
            entity.Property(e => e.Nombre)
                .HasMaxLength(500)
                .HasColumnName("NOMBRE");
            entity.Property(e => e.Razonsocial)
                .HasMaxLength(500)
                .HasColumnName("RAZONSOCIAL");
            entity.Property(e => e.Representaciongrafica)
                .HasMaxLength(500)
                .HasColumnName("REPRESENTACIONGRAFICA");
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .HasColumnName("RFC");
            entity.Property(e => e.Total).HasColumnName("TOTAL");
            entity.Property(e => e.Unidades).HasColumnName("UNIDADES");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
