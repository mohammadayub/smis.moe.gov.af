using Clean.Domain.Entity.prc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Prc
{
    public class ProcessTrackingConfiguration : IEntityTypeConfiguration<ProcessTracking>
    {
        public void Configure(EntityTypeBuilder<ProcessTracking> entity)
        {
            entity.ToTable("ProcessTracking", "prc");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ModuleId).HasColumnName("ModuleID");

            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");

            entity.Property(e => e.ReferedProcessId).HasColumnName("ReferedProcessID");

            entity.Property(e => e.Remarks).HasMaxLength(1000);

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.Property(e => e.ToUserId).HasColumnName("ToUserID");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Module)
                .WithMany(p => p.ProcessTracking)
                .HasForeignKey(d => d.ModuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_ProcessTracking__FK_2");

            entity.HasOne(d => d.Process)
                .WithMany(p => p.ProcessTrackingProcess)
                .HasForeignKey(d => d.ProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_ProcessTracking__FK");

        }
    }
}
