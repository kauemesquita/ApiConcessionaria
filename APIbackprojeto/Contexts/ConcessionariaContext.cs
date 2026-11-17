using System;
using System.Collections.Generic;
using APIbackprojeto.Models;
using Microsoft.EntityFrameworkCore;

namespace APIbackprojeto.Contexts;

public partial class ConcessionariaContext : DbContext
{
    public ConcessionariaContext(DbContextOptions<ConcessionariaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Marca> Marca { get; set; }

    public virtual DbSet<Veiculo> Veiculo { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marca__4076A8878FABAFAB");

            entity.HasIndex(e => e.Nome, "UQ__Marca__7D8FE3B272E04C6D").IsUnique();

            entity.Property(e => e.Nome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaisOrigem)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(e => e.IdVeiculo).HasName("PK__Veiculo__CAC4F346D8244A3E");

            entity.HasIndex(e => e.Placa, "UQ__Veiculo__8310F99DE1312A13").IsUnique();

            entity.Property(e => e.Cor)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Modelo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Placa)
                .HasMaxLength(7)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Preco).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Veiculo)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Veiculo__IdMarca__60A75C0F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
