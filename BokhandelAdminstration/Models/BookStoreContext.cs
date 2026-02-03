using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BokhandelAdminstration.Models;

public partial class BookStoreContext : DbContext
{
    public BookStoreContext()
    {
    }

    public BookStoreContext(DbContextOptions<BookStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Beställningar> Beställningars { get; set; }

    public virtual DbSet<Beställningsrader> Beställningsraders { get; set; }

    public virtual DbSet<Butiker> Butikers { get; set; }

    public virtual DbSet<Böcker> Böckers { get; set; }

    public virtual DbSet<Författare> Författares { get; set; }

    public virtual DbSet<Förlag> Förlags { get; set; }

    public virtual DbSet<KategoriStatistik> KategoriStatistiks { get; set; }

    public virtual DbSet<Kategorier> Kategoriers { get; set; }

    public virtual DbSet<Kunder> Kunders { get; set; }

    public virtual DbSet<LagerSaldo> LagerSaldos { get; set; }

    public virtual DbSet<Leverantörer> Leverantörers { get; set; }

    public virtual DbSet<TitlarPerFörfattare> TitlarPerFörfattares { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Bokhandel;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Beställningar>(entity =>
        {
            entity.HasKey(e => e.BeställningId).HasName("PK__Beställn__DB1862F1124D1D26");

            entity.ToTable("Beställningar");

            entity.Property(e => e.BeställningId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BeställningID");
            entity.Property(e => e.KundId).HasColumnName("Kund_Id");

            entity.HasOne(d => d.Kund).WithMany(p => p.Beställningars)
                .HasForeignKey(d => d.KundId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Beställni__Kund___4F7CD00D");
        });

        modelBuilder.Entity<Beställningsrader>(entity =>
        {
            entity.HasKey(e => new { e.BeställningId, e.Isbn13 }).HasName("PK__Beställn__F8A71B112930DCE6");

            entity.ToTable("Beställningsrader");

            entity.Property(e => e.BeställningId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("BeställningID");
            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN13");
            entity.Property(e => e.PrisVidKöp).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Beställning).WithMany(p => p.Beställningsraders)
                .HasForeignKey(d => d.BeställningId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Beställni__Bestä__5441852A");

            entity.HasOne(d => d.Isbn13Navigation).WithMany(p => p.Beställningsraders)
                .HasForeignKey(d => d.Isbn13)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Beställni__ISBN1__5535A963");
        });

        modelBuilder.Entity<Butiker>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Butiker__3214EC27A8D48B97");

            entity.ToTable("Butiker");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Adress).HasMaxLength(100);
            entity.Property(e => e.Butiksnamn).HasMaxLength(50);
            entity.Property(e => e.Ort).HasMaxLength(50);
            entity.Property(e => e.Postnummer).HasMaxLength(10);
        });

        modelBuilder.Entity<Böcker>(entity =>
        {
            entity.HasKey(e => e.Isbn13).HasName("PK__Böcker__3BF79E035CB6899F");

            entity.ToTable("Böcker");

            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN13");
            entity.Property(e => e.FörlagId).HasColumnName("FörlagID");
            entity.Property(e => e.KategoriId).HasColumnName("KategoriID");
            entity.Property(e => e.Pris).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Språk).HasMaxLength(30);
            entity.Property(e => e.Titel).HasMaxLength(100);

            entity.HasOne(d => d.Förlag).WithMany(p => p.Böckers)
                .HasForeignKey(d => d.FörlagId)
                .HasConstraintName("FK__Böcker__FörlagID__3E52440B");

            entity.HasOne(d => d.Kategori).WithMany(p => p.Böckers)
                .HasForeignKey(d => d.KategoriId)
                .HasConstraintName("FK__Böcker__Kategori__3F466844");

            entity.HasMany(d => d.Författares).WithMany(p => p.Isbn13s)
                .UsingEntity<Dictionary<string, object>>(
                    "Bokförfattare",
                    r => r.HasOne<Författare>().WithMany()
                        .HasForeignKey("FörfattareId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Bokförfat__Förfa__4CA06362"),
                    l => l.HasOne<Böcker>().WithMany()
                        .HasForeignKey("Isbn13")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Bokförfat__ISBN1__4BAC3F29"),
                    j =>
                    {
                        j.HasKey("Isbn13", "FörfattareId").HasName("PK__Bokförfa__23F38F4ED738705E");
                        j.ToTable("Bokförfattare");
                        j.IndexerProperty<string>("Isbn13")
                            .HasMaxLength(13)
                            .IsUnicode(false)
                            .IsFixedLength()
                            .HasColumnName("ISBN13");
                        j.IndexerProperty<int>("FörfattareId").HasColumnName("FörfattareID");
                    });
        });

        modelBuilder.Entity<Författare>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Författa__3214EC2718FB8187");

            entity.ToTable("Författare");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Efternamn).HasMaxLength(50);
            entity.Property(e => e.Förnamn).HasMaxLength(50);
        });

        modelBuilder.Entity<Förlag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Förlag__3214EC27A5ACC372");

            entity.ToTable("Förlag");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Land).HasMaxLength(50);
            entity.Property(e => e.LeverantörId).HasColumnName("LeverantörID");
            entity.Property(e => e.Namn).HasMaxLength(50);

            entity.HasOne(d => d.Leverantör).WithMany(p => p.Förlags)
                .HasForeignKey(d => d.LeverantörId)
                .HasConstraintName("FK__Förlag__Leverant__693CA210");
        });

        modelBuilder.Entity<KategoriStatistik>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("KategoriStatistik");

            entity.Property(e => e.AntalTitlar)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Kategori).HasMaxLength(50);
            entity.Property(e => e.TotalFörsäljning)
                .HasMaxLength(44)
                .IsUnicode(false);
            entity.Property(e => e.TotaltSåldaExemplar)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Kategorier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Kategori__3214EC27223D2583");

            entity.ToTable("Kategorier");

            entity.HasIndex(e => e.KategoriNamn, "UQ__Kategori__CA6EAC6E30F4E71F").IsUnique();

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.KategoriNamn).HasMaxLength(50);
        });

        modelBuilder.Entity<Kunder>(entity =>
        {
            entity.HasKey(e => e.KundId).HasName("PK__Kunder__7C0AD0D5BE73FD3C");

            entity.ToTable("Kunder");

            entity.HasIndex(e => new { e.KundNummer, e.Mail }, "UQ__Kunder__CAAF2736F1B7EB35").IsUnique();

            entity.Property(e => e.KundId).HasColumnName("Kund_Id");
            entity.Property(e => e.Efternamn).HasMaxLength(30);
            entity.Property(e => e.Förnamn).HasMaxLength(30);
            entity.Property(e => e.KundNummer)
                .HasMaxLength(30)
                .HasColumnName("Kund_Nummer");
            entity.Property(e => e.Mail).HasMaxLength(50);
        });

        modelBuilder.Entity<LagerSaldo>(entity =>
        {
            entity.HasKey(e => new { e.ButikId, e.Isbn13 }).HasName("PK__LagerSal__9669121A0A8E5E1A");

            entity.ToTable("LagerSaldo");

            entity.Property(e => e.ButikId).HasColumnName("ButikID");
            entity.Property(e => e.Isbn13)
                .HasMaxLength(13)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ISBN13");

            entity.HasOne(d => d.Butik).WithMany(p => p.LagerSaldos)
                .HasForeignKey(d => d.ButikId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LagerSald__Butik__440B1D61");

            entity.HasOne(d => d.Isbn13Navigation).WithMany(p => p.LagerSaldos)
                .HasForeignKey(d => d.Isbn13)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LagerSald__ISBN1__44FF419A");
        });

        modelBuilder.Entity<Leverantörer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Leverant__3214EC2715CE427E");

            entity.ToTable("Leverantörer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Adress).HasMaxLength(100);
            entity.Property(e => e.Epost).HasMaxLength(100);
            entity.Property(e => e.Kontaktperson).HasMaxLength(100);
            entity.Property(e => e.Namn).HasMaxLength(100);
            entity.Property(e => e.Ort).HasMaxLength(50);
            entity.Property(e => e.Postnummer).HasMaxLength(10);
            entity.Property(e => e.Telefon).HasMaxLength(20);
        });

        modelBuilder.Entity<TitlarPerFörfattare>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TitlarPerFörfattare");

            entity.Property(e => e.Lagervärde)
                .HasMaxLength(44)
                .IsUnicode(false);
            entity.Property(e => e.Namn).HasMaxLength(101);
            entity.Property(e => e.Titlar)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Ålder)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
