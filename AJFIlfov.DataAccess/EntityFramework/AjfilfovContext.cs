using System;
using System.Collections.Generic;
using AJFIlfov.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace AJFIlfov.DataAccess.EntityFramework;

public partial class AjfilfovContext : DbContext
{
    public AjfilfovContext()
    {
    }

    public AjfilfovContext(DbContextOptions<AjfilfovContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Disponibilitate> Disponibilitates { get; set; }

    public virtual DbSet<DisponibilitateAdmin> DisponibilitateAdmins { get; set; }

    public virtual DbSet<Echipe> Echipes { get; set; }

    public virtual DbSet<Grupe> Grupes { get; set; }

    public virtual DbSet<GrupeEchipa> GrupeEchipas { get; set; }

    public virtual DbSet<Localitati> Localitatis { get; set; }

    public virtual DbSet<Marimi> Marimis { get; set; }

    public virtual DbSet<Meciuri> Meciuris { get; set; }

    public virtual DbSet<PasswordRecovery> PasswordRecoveries { get; set; }

    public virtual DbSet<Refereestat> Refereestats { get; set; }

    public virtual DbSet<Roluri> Roluris { get; set; }

    public virtual DbSet<Stadioane> Stadioanes { get; set; }

    public virtual DbSet<StadionLocalitate> StadionLocalitates { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<Utilizatori> Utilizatoris { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:ajfilfov.database.windows.net,1433;Initial Catalog=AJFIlfov;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Disponibilitate>(entity =>
        {
            entity.HasKey(e => e.IdDisponibilitate).HasName("PK__Disponib__A79EDADBA5F65FC0");

            entity.ToTable("Disponibilitate");

            entity.HasIndex(e => e.IdUtilizator, "IX_Disponibilitate_IdUtilizator");

            entity.Property(e => e.IdDisponibilitate).ValueGeneratedNever();
            entity.Property(e => e.OraIncepere).HasColumnType("datetime");
            entity.Property(e => e.OraIncheiere).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Zi).HasColumnType("date");

            entity.HasOne(d => d.IdUtilizatorNavigation).WithMany(p => p.Disponibilitates)
                .HasForeignKey(d => d.IdUtilizator)
                .HasConstraintName("FK__Disponibi__IdUti__38996AB5");
        });

        modelBuilder.Entity<DisponibilitateAdmin>(entity =>
        {
            entity.HasKey(e => e.IdDisponibilitateAdmin);

            entity.ToTable("DisponibilitateAdmin");

            entity.Property(e => e.IdDisponibilitateAdmin).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Zi).HasColumnType("date");
        });

        modelBuilder.Entity<Echipe>(entity =>
        {
            entity.HasKey(e => e.IdEchipa).HasName("PK__Echipe__D4A3A318683D43A3");

            entity.ToTable("Echipe");

            entity.Property(e => e.IdEchipa).ValueGeneratedNever();
            entity.Property(e => e.Nume).HasMaxLength(50);
        });

        modelBuilder.Entity<Grupe>(entity =>
        {
            entity.HasKey(e => e.IdGrupa).HasName("PK__Grupe__303F6F275E4C9D6A");

            entity.ToTable("Grupe");

            entity.Property(e => e.IdGrupa).ValueGeneratedNever();
            entity.Property(e => e.Nume).HasMaxLength(50);
        });

        modelBuilder.Entity<GrupeEchipa>(entity =>
        {
            entity.HasKey(e => e.IdGrupaEchipa).HasName("PK__GrupeEch__F01D7F8F07F92D34");

            entity.ToTable("GrupeEchipa");

            entity.HasIndex(e => e.IdEchipa, "IX_GrupeEchipa_IdEchipa");

            entity.HasIndex(e => e.IdGrupa, "IX_GrupeEchipa_IdGrupa");

            entity.Property(e => e.IdGrupaEchipa).ValueGeneratedNever();

            entity.HasOne(d => d.IdEchipaNavigation).WithMany(p => p.GrupeEchipas)
                .HasForeignKey(d => d.IdEchipa)
                .HasConstraintName("FK__GrupeEchi__IdEch__2A4B4B5E");

            entity.HasOne(d => d.IdGrupaNavigation).WithMany(p => p.GrupeEchipas)
                .HasForeignKey(d => d.IdGrupa)
                .HasConstraintName("FK__GrupeEchi__IdGru__2B3F6F97");
        });

        modelBuilder.Entity<Localitati>(entity =>
        {
            entity.HasKey(e => e.IdLocalitate).HasName("PK__Localita__937B022C50107212");

            entity.ToTable("Localitati");

            entity.Property(e => e.IdLocalitate).ValueGeneratedNever();
            entity.Property(e => e.Nume).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(500);
        });

        modelBuilder.Entity<Marimi>(entity =>
        {
            entity.HasKey(e => e.IdMarime).HasName("PK__Marimi__95BA145B80EFB053");

            entity.ToTable("Marimi");

            entity.Property(e => e.IdMarime).ValueGeneratedNever();
            entity.Property(e => e.Marime).HasMaxLength(10);
        });

        modelBuilder.Entity<Meciuri>(entity =>
        {
            entity.HasKey(e => e.IdMeci).HasName("PK__Meciuri__4D7C0B75D32C1A8D");

            entity.ToTable("Meciuri");

            entity.HasIndex(e => e.IdArbitru, "IX_Meciuri_IdArbitru");

            entity.HasIndex(e => e.IdArbitruAsistent1, "IX_Meciuri_IdArbitruAsistent1");

            entity.HasIndex(e => e.IdArbitruAsistent2, "IX_Meciuri_IdArbitruAsistent2");

            entity.HasIndex(e => e.IdArbitruRezerva, "IX_Meciuri_IdArbitruRezerva");

            entity.HasIndex(e => e.IdEchipaGazda, "IX_Meciuri_IdEchipaGazda");

            entity.HasIndex(e => e.IdEchipaOaspete, "IX_Meciuri_IdEchipaOaspete");

            entity.HasIndex(e => e.IdObservator, "IX_Meciuri_IdObservator");

            entity.HasIndex(e => e.IdStadionLocalitate, "IX_Meciuri_IdStadionLocalitate");

            entity.Property(e => e.IdMeci).ValueGeneratedNever();
            entity.Property(e => e.DataJoc).HasColumnType("date");
            entity.Property(e => e.IdDeleted).HasColumnName("idDeleted");
            entity.Property(e => e.Observatii).HasMaxLength(500);
            entity.Property(e => e.Rezultat).HasMaxLength(50);

            entity.HasOne(d => d.IdArbitruNavigation).WithMany(p => p.MeciuriIdArbitruNavigations)
                .HasForeignKey(d => d.IdArbitru)
                .HasConstraintName("FK__Meciuri__IdArbit__4222D4EF");

            entity.HasOne(d => d.IdArbitruAsistent1Navigation).WithMany(p => p.MeciuriIdArbitruAsistent1Navigations)
                .HasForeignKey(d => d.IdArbitruAsistent1)
                .HasConstraintName("FK__Meciuri__IdArbit__4316F928");

            entity.HasOne(d => d.IdArbitruAsistent2Navigation).WithMany(p => p.MeciuriIdArbitruAsistent2Navigations)
                .HasForeignKey(d => d.IdArbitruAsistent2)
                .HasConstraintName("FK__Meciuri__IdArbit__440B1D61");

            entity.HasOne(d => d.IdArbitruRezervaNavigation).WithMany(p => p.MeciuriIdArbitruRezervaNavigations)
                .HasForeignKey(d => d.IdArbitruRezerva)
                .HasConstraintName("FK__Meciuri__IdArbit__44FF419A");

            entity.HasOne(d => d.IdEchipaGazdaNavigation).WithMany(p => p.MeciuriIdEchipaGazdaNavigations)
                .HasForeignKey(d => d.IdEchipaGazda)
                .HasConstraintName("FK__Meciuri__IdEchip__403A8C7D");

            entity.HasOne(d => d.IdEchipaOaspeteNavigation).WithMany(p => p.MeciuriIdEchipaOaspeteNavigations)
                .HasForeignKey(d => d.IdEchipaOaspete)
                .HasConstraintName("FK__Meciuri__IdEchip__412EB0B6");

            entity.HasOne(d => d.IdObservatorNavigation).WithMany(p => p.MeciuriIdObservatorNavigations)
                .HasForeignKey(d => d.IdObservator)
                .HasConstraintName("FK__Meciuri__IdObser__45F365D3");

            entity.HasOne(d => d.IdStadionLocalitateNavigation).WithMany(p => p.Meciuris)
                .HasForeignKey(d => d.IdStadionLocalitate)
                .HasConstraintName("FK__Meciuri__IdStadi__46E78A0C");
        });

        modelBuilder.Entity<PasswordRecovery>(entity =>
        {
            entity.ToTable("PasswordRecovery");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code).HasMaxLength(8);
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.IsAvailable).HasColumnName("isAvailable");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.PasswordRecoveries)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_PasswordRecovery_User");
        });

        modelBuilder.Entity<Refereestat>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("refereestats");

            entity.Property(e => e.Total).HasColumnName("total");
            entity.Property(e => e.VarstaMedie).HasColumnName("varsta_medie");
        });

        modelBuilder.Entity<Roluri>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roluri__2A49584C6CB95133");

            entity.ToTable("Roluri");

            entity.Property(e => e.IdRol).ValueGeneratedNever();
            entity.Property(e => e.Nume).HasMaxLength(50);
        });

        modelBuilder.Entity<Stadioane>(entity =>
        {
            entity.HasKey(e => e.IdStadion).HasName("PK__Stadioan__6A3F1A8882EFD894");

            entity.ToTable("Stadioane");

            entity.Property(e => e.IdStadion).ValueGeneratedNever();
            entity.Property(e => e.Nume).HasMaxLength(50);
        });

        modelBuilder.Entity<StadionLocalitate>(entity =>
        {
            entity.HasKey(e => e.IdStadionLocalitate).HasName("PK__StadionL__8D363E8182F8F13E");

            entity.ToTable("StadionLocalitate");

            entity.HasIndex(e => e.IdLocalitate, "IX_StadionLocalitate_IdLocalitate");

            entity.HasIndex(e => e.IdStadion, "IX_StadionLocalitate_IdStadion");

            entity.Property(e => e.IdStadionLocalitate).ValueGeneratedNever();

            entity.HasOne(d => d.IdLocalitateNavigation).WithMany(p => p.StadionLocalitates)
                .HasForeignKey(d => d.IdLocalitate)
                .HasConstraintName("FK__StadionLo__IdLoc__3C69FB99");

            entity.HasOne(d => d.IdStadionNavigation).WithMany(p => p.StadionLocalitates)
                .HasForeignKey(d => d.IdStadion)
                .HasConstraintName("FK__StadionLo__IdSta__3B75D760");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserAddr__1788CC4C0BBEEBDD");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.State).HasMaxLength(100);
            entity.Property(e => e.StreetAddress).HasMaxLength(255);
            entity.Property(e => e.ZipCode).HasMaxLength(20);

            entity.HasOne(d => d.User).WithOne(p => p.UserAddress)
                .HasForeignKey<UserAddress>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserAddre__UserI__160F4887");
        });

        modelBuilder.Entity<Utilizatori>(entity =>
        {
            entity.HasKey(e => e.IdUtilizator).HasName("PK__Utilizat__99101D6D186B6C92");

            entity.ToTable("Utilizatori");

            entity.HasIndex(e => e.IdMarimeAdidasi, "IX_Utilizatori_IdMarimeAdidasi");

            entity.HasIndex(e => e.IdMarimeHaine, "IX_Utilizatori_IdMarimeHaine");

            entity.HasIndex(e => e.IdRol, "IX_Utilizatori_IdRol");

            entity.Property(e => e.IdUtilizator).ValueGeneratedNever();
            entity.Property(e => e.Adresa).HasMaxLength(150);
            entity.Property(e => e.DataIncepere).HasColumnType("date");
            entity.Property(e => e.DataNastere).HasColumnType("date");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.IsSuspended).HasColumnName("isSuspended");
            entity.Property(e => e.Mail).HasMaxLength(50);
            entity.Property(e => e.Nume).HasMaxLength(50);
            entity.Property(e => e.Parola).HasMaxLength(50);
            entity.Property(e => e.NumarTelefon).HasMaxLength(50);
            entity.Property(e => e.Prenume).HasMaxLength(50);

            entity.HasOne(d => d.IdMarimeAdidasiNavigation).WithMany(p => p.UtilizatoriIdMarimeAdidasiNavigations)
                .HasForeignKey(d => d.IdMarimeAdidasi)
                .HasConstraintName("FK__Utilizato__IdMar__34C8D9D1");

            entity.HasOne(d => d.IdMarimeHaineNavigation).WithMany(p => p.UtilizatoriIdMarimeHaineNavigations)
                .HasForeignKey(d => d.IdMarimeHaine)
                .HasConstraintName("FK__Utilizato__IdMar__35BCFE0A");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Utilizatoris)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Utilizato__IdRol__33D4B598");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
