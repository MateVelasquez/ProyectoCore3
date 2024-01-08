using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProyectoCore.Models
{
    public partial class CoreContext : DbContext
    {
        public CoreContext()
        {
        }

        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ascenso> Ascensos { get; set; }
        public virtual DbSet<Exportacion> Exportacions { get; set; }
        public virtual DbSet<ProductoQuimico> ProductoQuimicos { get; set; }
        public virtual DbSet<ProyeccionAnual> ProyeccionAnuals { get; set; }
        public virtual DbSet<TallosCosechado> TallosCosechados { get; set; }
        public virtual DbSet<Trabajador> Trabajadors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
               // optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb; database=Core; integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ascenso>(entity =>
            {
                entity.HasKey(e => e.IdAscenso)
                    .HasName("PK_idAscenso");

                entity.ToTable("Ascenso");

                entity.Property(e => e.IdAscenso)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idAscenso");

                entity.Property(e => e.IdTrabajador).HasColumnName("idTrabajador");

                entity.Property(e => e.Sustentacion)
                    .IsRequired()
                    .HasColumnType("text")
                    .HasColumnName("sustentacion");

                entity.HasOne(d => d.IdTrabajadorNavigation)
                    .WithMany(p => p.Ascensos)
                    .HasForeignKey(d => d.IdTrabajador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_idTrabajadorA");
            });

            modelBuilder.Entity<Exportacion>(entity =>
            {
                entity.HasKey(e => e.IdExportacion)
                    .HasName("PK_idExportacion");

                entity.ToTable("Exportacion");

                entity.Property(e => e.IdExportacion)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idExportacion");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.FechaExportacion)
                    .HasColumnType("date")
                    .HasColumnName("fechaExportacion");

                entity.Property(e => e.IdTrabajador).HasColumnName("idTrabajador");

                entity.HasOne(d => d.IdTrabajadorNavigation)
                    .WithMany(p => p.Exportacions)
                    .HasForeignKey(d => d.IdTrabajador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_idTrabajadorEx");
            });

            modelBuilder.Entity<ProductoQuimico>(entity =>
            {
                entity.HasKey(e => e.IdProductoQuimico)
                    .HasName("PK_idProductoQuimico");

                entity.ToTable("ProductoQuimico");

                entity.Property(e => e.IdProductoQuimico)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idProductoQuimico");

                entity.Property(e => e.Costo)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("costo");

                entity.Property(e => e.IdExportacion).HasColumnName("idExportacion");

                entity.Property(e => e.IdTrabajador).HasColumnName("idTrabajador");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdExportacionNavigation)
                    .WithMany(p => p.ProductoQuimicos)
                    .HasForeignKey(d => d.IdExportacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_idExportacion");

                entity.HasOne(d => d.IdTrabajadorNavigation)
                    .WithMany(p => p.ProductoQuimicos)
                    .HasForeignKey(d => d.IdTrabajador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_idTrabajadorPq");
            });

            modelBuilder.Entity<ProyeccionAnual>(entity =>
            {
                entity.HasKey(e => e.IdProyeccionAnual)
                    .HasName("PK_idProyeccionAnual");

                entity.ToTable("ProyeccionAnual");

                entity.Property(e => e.IdProyeccionAnual)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idProyeccionAnual");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdExportacion).HasColumnName("idExportacion");

                entity.Property(e => e.IdTrabajador).HasColumnName("idTrabajador");

                entity.HasOne(d => d.IdExportacionNavigation)
                    .WithMany(p => p.ProyeccionAnuals)
                    .HasForeignKey(d => d.IdExportacion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_idExportacionPA");

                entity.HasOne(d => d.IdTrabajadorNavigation)
                    .WithMany(p => p.ProyeccionAnuals)
                    .HasForeignKey(d => d.IdTrabajador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_idTrabajadorPA");
            });

            modelBuilder.Entity<TallosCosechado>(entity =>
            {
                entity.HasKey(e => e.IdTallosCosechados)
                    .HasName("PK_idTallosCosechados");

                entity.Property(e => e.IdTallosCosechados)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idTallosCosechados");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.FechaCosecha)
                    .HasColumnType("date")
                    .HasColumnName("fechaCosecha");

                entity.Property(e => e.IdTrabajador).HasColumnName("idTrabajador");

                entity.HasOne(d => d.IdTrabajadorNavigation)
                    .WithMany(p => p.TallosCosechados)
                    .HasForeignKey(d => d.IdTrabajador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_idTrabajadorPC");
            });

            modelBuilder.Entity<Trabajador>(entity =>
            {
                entity.HasKey(e => e.IdTrabajador)
                    .HasName("PK_idTrabajador");

                entity.ToTable("Trabajador");

                entity.Property(e => e.IdTrabajador)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idTrabajador");

                entity.Property(e => e.AreaTrabajo).HasColumnName("areaTrabajo");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("cedula");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FechaContratacion)
                    .HasColumnType("date")
                    .HasColumnName("fechaContratacion");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Salario)
                    .HasColumnType("decimal(12, 2)")
                    .HasColumnName("salario");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
