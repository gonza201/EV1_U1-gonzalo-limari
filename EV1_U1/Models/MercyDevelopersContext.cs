using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EV1_U1.Models;

public partial class MercyDevelopersContext : DbContext
{
    public MercyDevelopersContext()
    {
    }

    public MercyDevelopersContext(DbContextOptions<MercyDevelopersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {

        }
    }
 //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
      //  => optionsBuilder.UseMySql("server=localhost;port=3306;database=mercy-developers;uid=root;password=hola", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.25-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PRIMARY");

            entity.ToTable("clientes");

            entity.Property(e => e.IdCliente)
                .HasColumnType("int(11)")
                .HasColumnName("id_cliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(45)
                .HasColumnName("apellido");
            entity.Property(e => e.Correo)
                .HasMaxLength(45)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(45)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(45)
                .HasColumnName("telefono");

            entity.HasMany(d => d.ServiciosIdServicios).WithMany(p => p.ClientesIdClientes)
                .UsingEntity<Dictionary<string, object>>(
                    "ClienteServicio",
                    r => r.HasOne<Servicio>().WithMany()
                        .HasForeignKey("ServiciosIdServicio")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_clientes_has_servicios_servicios1"),
                    l => l.HasOne<Cliente>().WithMany()
                        .HasForeignKey("ClientesIdCliente")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_clientes_has_servicios_clientes"),
                    j =>
                    {
                        j.HasKey("ClientesIdCliente", "ServiciosIdServicio")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("cliente_servicio");
                        j.HasIndex(new[] { "ClientesIdCliente" }, "fk_clientes_has_servicios_clientes_idx");
                        j.HasIndex(new[] { "ServiciosIdServicio" }, "fk_clientes_has_servicios_servicios1_idx");
                        j.IndexerProperty<int>("ClientesIdCliente")
                            .HasColumnType("int(11)")
                            .HasColumnName("clientes_id_cliente");
                        j.IndexerProperty<int>("ServiciosIdServicio")
                            .HasColumnType("int(11)")
                            .HasColumnName("servicios_id_servicio");
                    });
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PRIMARY");

            entity.ToTable("servicios");

            entity.Property(e => e.IdServicio)
                .HasColumnType("int(11)")
                .HasColumnName("id_servicio");
            entity.Property(e => e.Estado)
                .HasMaxLength(45)
                .HasColumnName("estado");
            entity.Property(e => e.FechaCreacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaEntrega)
                .HasColumnType("datetime")
                .HasColumnName("fecha_entrega");
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(45)
                .HasColumnName("nombre_servicio");
            entity.Property(e => e.Precio)
                .HasColumnType("int(11)")
                .HasColumnName("precio");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
