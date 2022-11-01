using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using HormoneTracker.Core.Models;

namespace HormoneTracker.DAL
{
    public partial class HormoneTrackerDBContext : DbContext
    {
        public HormoneTrackerDBContext()
        {
        }

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
        public virtual DbSet<ProductDatum> ProductData { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Tip> Tips { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=PRUDIUSVLADPC\\DEV;Initial Catalog=HormoneTrackerDB;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.AdminId);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(50);
            });

            modelBuilder.Entity<Analysis>(entity =>
            {
                entity.ToTable("Analysis");

                entity.Property(e => e.AnalysisId);

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
                        l => l.HasOne<Datum>().WithMany().HasForeignKey("DataId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AnalysisData_Data"),
                        r => r.HasOne<Analysis>().WithMany().HasForeignKey("AnalysisId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_AnalysisData_Analysis"),
                        j =>
                        {
                            j.HasKey("AnalysisId", "DataId");

                            j.ToTable("AnalysisData");
                        });
            });

            modelBuilder.Entity<Datum>(entity =>
            {
                entity.HasKey(e => e.DataId);

                entity.Property(e => e.DataId);
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.ToTable("Doctor");

                entity.Property(e => e.DoctorId);

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

                entity.Property(e => e.MedicineId);

                entity.Property(e => e.LastDoseDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Period).HasMaxLength(50);
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");

                entity.Property(e => e.PatientId);

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

                entity.Property(e => e.ProductId);
            });

            modelBuilder.Entity<ProductDatum>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.DataId });

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductData)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductData_Product");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId);
            });

            modelBuilder.Entity<Tip>(entity =>
            {
                entity.ToTable("Tip");

                entity.Property(e => e.TipId);

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
