using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HormoneTracker.Core.Models;

namespace HormoneTracker.DAL
{
    public partial class HormoneTrackerDBContext : DbContext
    {
        public HormoneTrackerDBContext(DbContextOptions<HormoneTrackerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Analysis> Analyses { get; set; } = null!;
        public virtual DbSet<Datum> Data { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<Medicine> Medicines { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Tip> Tips { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            modelBuilder.Entity<Analysis>(entity =>
            {
                entity.ToTable("Analysis");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Analyses)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Analysis_Patient");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Analyses)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_Analysis_Status");

                entity.HasMany(d => d.Data)
                    .WithMany(p => p.Analyses)
                    .UsingEntity<Dictionary<string, object>>(
                        "AnalysisDatum",
                        l => l.HasOne<Datum>().WithMany().HasForeignKey("DataId").OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_AnalysisData_Data"),
                        r => r.HasOne<Analysis>().WithMany().HasForeignKey("AnalysisId").OnDelete(DeleteBehavior.Cascade).HasConstraintName("FK_AnalysisData_Analysis"),
                        j =>
                        {
                            j.HasKey("AnalysisId", "DataId");

                            j.ToTable("AnalysisData");
                        });
            });

            modelBuilder.Entity<Datum>(entity =>
            {
                entity.HasKey(e => e.DataId);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MidName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);
            });

            modelBuilder.Entity<Medicine>(entity =>
            {
                entity.ToTable("Medicine");

                entity.Property(e => e.LastDoseDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Period).HasMaxLength(50);

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Medicines)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Medicine_Patient");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MidName).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(50);

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Patient_Doctor");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.HasMany(d => d.Data)
                    .WithMany(p => p.Products)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProductDatum",
                        l => l.HasOne<Datum>().WithMany().HasForeignKey("DataId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ProductData_Data"),
                        r => r.HasOne<Product>().WithMany().HasForeignKey("ProductId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_ProductData_Product"),
                        j =>
                        {
                            j.HasKey("ProductId", "DataId");

                            j.ToTable("ProductData");

                            j.IndexerProperty<int>("ProductId").ValueGeneratedOnAdd();
                        });
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");
            });

            modelBuilder.Entity<Tip>(entity =>
            {
                entity.ToTable("Tip");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.Tips)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Tip_Patient");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
