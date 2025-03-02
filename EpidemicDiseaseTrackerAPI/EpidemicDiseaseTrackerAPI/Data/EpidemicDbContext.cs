using System;
using System.Collections.Generic;
using EpidemicDiseaseTrackerAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EpidemicDiseaseTrackerAPI.Data;

public partial class EpidemicDbContext : DbContext
{
    public EpidemicDbContext()
    {
    }

    public EpidemicDbContext(DbContextOptions<EpidemicDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DiseaseCasesByWeek> DiseaseCasesByWeeks { get; set; }

    public virtual DbSet<DiseaseCasesByYear> DiseaseCasesByYears { get; set; }

    public virtual DbSet<DiseaseData> DiseaseData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=MAALUZ;Database=EpidemicDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DiseaseCasesByWeek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DiseaseC__3214EC276D6B74DA");
        });

        modelBuilder.Entity<DiseaseCasesByYear>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DiseaseC__3214EC27EB11B64A");
        });

        modelBuilder.Entity<DiseaseData>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DiseaseD__3214EC070D4B8275");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
