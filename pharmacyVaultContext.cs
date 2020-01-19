using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PharmaCite_Pharmacy_Managment_System.Models
{
    public partial class pharmacyVaultContext : DbContext
    {
        public pharmacyVaultContext()
        {
        }

        public pharmacyVaultContext(DbContextOptions<pharmacyVaultContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<MedicinesSold> MedicinesSold { get; set; }
        public virtual DbSet<MedicinesStored> MedicinesStored { get; set; }
        public virtual DbSet<Pharmacist> Pharmacist { get; set; }
        public virtual DbSet<UserCompliants> UserCompliants { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("manager");

                entity.Property(e => e.ManagerId).HasColumnName("managerID");

                entity.Property(e => e.ManagerName)
                    .IsRequired()
                    .HasColumnName("managerName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasColumnName("pwd")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MedicinesSold>(entity =>
            {
                entity.HasKey(e => e.SoldId)
                    .HasName("PK__medicine__9CA508CED3450F08");

                entity.ToTable("medicines_sold");

                entity.Property(e => e.SoldId)
                    .HasColumnName("soldId")
                    .ValueGeneratedNever();

                entity.Property(e => e.SoldBy).HasColumnName("soldBy");

                entity.Property(e => e.SoldDate)
                    .HasColumnName("soldDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SoldMedicine).HasColumnName("soldMedicine");

                entity.Property(e => e.SoldTo).HasColumnName("soldTo");

                entity.HasOne(d => d.SoldByNavigation)
                    .WithMany(p => p.MedicinesSold)
                    .HasForeignKey(d => d.SoldBy)
                    .HasConstraintName("FK_sold_med_To_Pharmacist");

                entity.HasOne(d => d.SoldMedicineNavigation)
                    .WithMany(p => p.MedicinesSold)
                    .HasForeignKey(d => d.SoldMedicine)
                    .HasConstraintName("FK_sold_med_To_Medicines_stored");

                entity.HasOne(d => d.SoldToNavigation)
                    .WithMany(p => p.MedicinesSold)
                    .HasForeignKey(d => d.SoldTo)
                    .HasConstraintName("FK_sold_med_To_Users");
            });

            modelBuilder.Entity<MedicinesStored>(entity =>
            {
                entity.HasKey(e => e.MedicineId)
                    .HasName("PK__medicine__BA9E65CE00659D9B");

                entity.ToTable("medicines_stored");

                entity.Property(e => e.MedicineId).HasColumnName("medicineID");

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.MedicineName)
                    .IsRequired()
                    .HasColumnName("medicineName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.StoredDate)
                    .HasColumnName("storedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Pharmacist>(entity =>
            {
                entity.ToTable("pharmacist");

                entity.Property(e => e.PharmacistId).HasColumnName("pharmacistID");

                entity.Property(e => e.HiredBy).HasColumnName("hiredBy");

                entity.Property(e => e.HiredDate)
                    .HasColumnName("hiredDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PharmacistName)
                    .IsRequired()
                    .HasColumnName("pharmacistName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasColumnName("pwd")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.HiredByNavigation)
                    .WithMany(p => p.Pharmacist)
                    .HasForeignKey(d => d.HiredBy)
                    .HasConstraintName("FK__pharmacis__hired__628FA481");
            });

            modelBuilder.Entity<UserCompliants>(entity =>
            {
                entity.HasKey(e => e.ComplaintId)
                    .HasName("PK__user_com__489708E1D7FADF6A");

                entity.ToTable("user_compliants");

                entity.Property(e => e.ComplaintId).HasColumnName("complaintID");

                entity.Property(e => e.CompaintText)
                    .IsRequired()
                    .HasColumnName("compaintText")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ComplaintBy).HasColumnName("complaintBy");

                entity.Property(e => e.ComplaintDate)
                    .HasColumnName("complaintDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ComplaintOn).HasColumnName("complaintOn");

                entity.HasOne(d => d.ComplaintByNavigation)
                    .WithMany(p => p.UserCompliants)
                    .HasForeignKey(d => d.ComplaintBy)
                    .HasConstraintName("FK_user_compliants_To_Users");

                entity.HasOne(d => d.ComplaintOnNavigation)
                    .WithMany(p => p.UserCompliants)
                    .HasForeignKey(d => d.ComplaintOn)
                    .HasConstraintName("FK_user_compliants_To_Medicines_stored");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__users__CB9A1CDF95D4661B");

                entity.ToTable("users");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Age).HasColumnName("age");

                entity.Property(e => e.Pwd)
                    .IsRequired()
                    .HasColumnName("pwd")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RegisteredDate)
                    .HasColumnName("registeredDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
