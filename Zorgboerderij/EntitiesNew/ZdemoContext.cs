using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Zorgboerderij.EntitiesNew;

public partial class ZdemoContext : DbContext
{
    public ZdemoContext()
    {
    }

    public ZdemoContext(DbContextOptions<ZdemoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tblafwezig> Tblafwezigs { get; set; }

    public virtual DbSet<Tblbakje> Tblbakjes { get; set; }

    public virtual DbSet<Tblclienten> Tblclientens { get; set; }

    public virtual DbSet<Tbldagcode> Tbldagcodes { get; set; }

    public virtual DbSet<Tbldp> Tbldps { get; set; }

    public virtual DbSet<Tblinlog> Tblinlogs { get; set; }

    public virtual DbSet<Tblmisbruik> Tblmisbruiks { get; set; }

    public virtual DbSet<Tblorganisatie> Tblorganisaties { get; set; }

    public virtual DbSet<Tblorgkeuze> Tblorgkeuzes { get; set; }

    public virtual DbSet<Tblpersoneel> Tblpersoneels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ZDemo;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tblafwezig>(entity =>
        {
            entity.HasKey(e => e.Afid).HasName("PK__tblafwez__5809062CD5F152D9");

            entity.ToTable("tblafwezig");

            entity.Property(e => e.Afid).HasColumnName("afid");
            entity.Property(e => e.Afsoort).HasColumnName("afsoort");
            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Datum)
                .HasColumnType("datetime")
                .HasColumnName("datum");

            entity.HasOne(d => d.Client).WithMany(p => p.Tblafwezigs)
                .HasForeignKey(d => d.Clientid)
                .HasConstraintName("FK__tblafwezi__clien__5FB337D6");
        });

        modelBuilder.Entity<Tblbakje>(entity =>
        {
            entity.HasKey(e => e.Bid).HasName("PK__tblbakje__DE90ADE702662CD5");

            entity.ToTable("tblbakje");

            entity.Property(e => e.Bid).HasColumnName("bid");
            entity.Property(e => e.Foto)
                .HasMaxLength(75)
                .HasColumnName("foto");
            entity.Property(e => e.Kleur)
                .HasMaxLength(50)
                .HasColumnName("kleur");
            entity.Property(e => e.Titel)
                .HasMaxLength(50)
                .HasColumnName("titel");
        });

        modelBuilder.Entity<Tblclienten>(entity =>
        {
            entity.HasKey(e => e.Clientid).HasName("PK__tblclien__819DC769CB1967BD");

            entity.ToTable("tblclienten");

            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Achternaam)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("achternaam");
            entity.Property(e => e.Dinsdag).HasColumnName("dinsdag");
            entity.Property(e => e.Donderdag).HasColumnName("donderdag");
            entity.Property(e => e.Foto)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("foto");
            entity.Property(e => e.Groepskleur).HasColumnName("groepskleur");
            entity.Property(e => e.Maandag).HasColumnName("maandag");
            entity.Property(e => e.Pin).HasColumnName("pin");
            entity.Property(e => e.Voornaam)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("voornaam");
            entity.Property(e => e.Vrijdag).HasColumnName("vrijdag");
            entity.Property(e => e.Woensdag).HasColumnName("woensdag");
            entity.Property(e => e.Zaterdag).HasColumnName("zaterdag");
        });

        modelBuilder.Entity<Tbldagcode>(entity =>
        {
            entity.HasKey(e => e.Dcid).HasName("PK__tbldagco__207EF8A8325896C4");

            entity.ToTable("tbldagcode");

            entity.Property(e => e.Dcid).HasColumnName("dcid");
            entity.Property(e => e.Code)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("code");
            entity.Property(e => e.Dag)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("dag");
        });

        modelBuilder.Entity<Tbldp>(entity =>
        {
            entity.HasKey(e => e.Dpid).HasName("PK__tbldp__2DDDC239F1691CDB");

            entity.ToTable("tbldp");

            entity.Property(e => e.Dpid).HasColumnName("dpid");
            entity.Property(e => e.Bid).HasColumnName("bid");
            entity.Property(e => e.Clientid).HasColumnName("clientid");
            entity.Property(e => e.Dagid)
                .HasMaxLength(50)
                .HasColumnName("dagid");
            entity.Property(e => e.Datum)
                .HasColumnType("datetime")
                .HasColumnName("datum");
            entity.Property(e => e.Kleur)
                .HasMaxLength(50)
                .HasColumnName("kleur");
            entity.Property(e => e.Sid).HasColumnName("sid");
            entity.Property(e => e.Sid2).HasColumnName("sid2");
            entity.Property(e => e.Soort).HasColumnName("soort");
            entity.Property(e => e.Volgorde).HasColumnName("volgorde");

            entity.HasOne(d => d.BidNavigation).WithMany(p => p.Tbldps)
                .HasForeignKey(d => d.Bid)
                .HasConstraintName("FK__tbldp__bid__6383C8BA");

            entity.HasOne(d => d.Client).WithMany(p => p.Tbldps)
                .HasForeignKey(d => d.Clientid)
                .HasConstraintName("FK__tbldp__clientid__628FA481");
        });

        modelBuilder.Entity<Tblinlog>(entity =>
        {
            entity.HasKey(e => e.Inlogid).HasName("PK__tblinlog__FCD7887AA632A8A3");

            entity.ToTable("tblinlog");

            entity.Property(e => e.Inlogid).HasColumnName("inlogid");
            entity.Property(e => e.Correct).HasColumnName("correct");
            entity.Property(e => e.Datum).HasColumnName("datum");
            entity.Property(e => e.Inlognaam)
                .HasMaxLength(50)
                .HasColumnName("inlognaam");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .HasColumnName("ip");
            entity.Property(e => e.Orgid).HasColumnName("orgid");
            entity.Property(e => e.Tijd).HasColumnName("tijd");
            entity.Property(e => e.Wachtwoord)
                .HasMaxLength(50)
                .HasColumnName("wachtwoord");

            entity.HasOne(d => d.Org).WithMany(p => p.Tblinlogs)
                .HasForeignKey(d => d.Orgid)
                .HasConstraintName("FK__tblinlog__orgid__534D60F1");
        });

        modelBuilder.Entity<Tblmisbruik>(entity =>
        {
            entity.HasKey(e => e.Misbruikid).HasName("PK__tblmisbr__DAA81CA93F834CA2");

            entity.ToTable("tblmisbruik");

            entity.Property(e => e.Misbruikid).HasColumnName("misbruikid");
            entity.Property(e => e.Datum).HasColumnName("datum");
            entity.Property(e => e.Inlognaam)
                .HasMaxLength(250)
                .HasColumnName("inlognaam");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .HasColumnName("ip");
            entity.Property(e => e.Soort)
                .HasMaxLength(50)
                .HasColumnName("soort");
            entity.Property(e => e.Tijd).HasColumnName("tijd");
        });

        modelBuilder.Entity<Tblorganisatie>(entity =>
        {
            entity.HasKey(e => e.Orgid).HasName("PK__tblorgan__3580E7B08AC5FAAB");

            entity.ToTable("tblorganisatie");

            entity.Property(e => e.Orgid).HasColumnName("orgid");
            entity.Property(e => e.Code)
                .HasMaxLength(150)
                .HasColumnName("code");
            entity.Property(e => e.Organisatie)
                .HasMaxLength(250)
                .HasColumnName("organisatie");
            entity.Property(e => e.Orgidweb)
                .HasMaxLength(50)
                .HasColumnName("orgidweb");
            entity.Property(e => e.Plaats)
                .HasMaxLength(200)
                .HasColumnName("plaats");
        });

        modelBuilder.Entity<Tblorgkeuze>(entity =>
        {
            entity.HasKey(e => e.Tblkopid).HasName("PK__tblorgke__E9469DAC3807F98A");

            entity.ToTable("tblorgkeuze");

            entity.Property(e => e.Tblkopid).HasColumnName("tblkopid");
            entity.Property(e => e.Wsid).HasColumnName("wsid");
            entity.Property(e => e.Wstotaal1).HasColumnName("wstotaal1");
            entity.Property(e => e.Wstotaal2).HasColumnName("wstotaal2");
            entity.Property(e => e.Wstotaal3).HasColumnName("wstotaal3");
        });

        modelBuilder.Entity<Tblpersoneel>(entity =>
        {
            entity.HasKey(e => e.Persid).HasName("PK__tblperso__46D34C7F9103B39F");

            entity.ToTable("tblpersoneel");

            entity.Property(e => e.Persid).HasColumnName("persid");
            entity.Property(e => e.Achternaam)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("achternaam");
            entity.Property(e => e.Dinsdag).HasColumnName("dinsdag");
            entity.Property(e => e.Donderdag).HasColumnName("donderdag");
            entity.Property(e => e.Foto)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("foto");
            entity.Property(e => e.Groepskleur).HasColumnName("groepskleur");
            entity.Property(e => e.Maandag).HasColumnName("maandag");
            entity.Property(e => e.Voornaam)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("voornaam");
            entity.Property(e => e.Vrijdag).HasColumnName("vrijdag");
            entity.Property(e => e.Woensdag).HasColumnName("woensdag");
            entity.Property(e => e.Zaterdag).HasColumnName("zaterdag");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
