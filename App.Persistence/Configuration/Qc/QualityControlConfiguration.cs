using App.Domain.Entity.qc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Qc
{
    public class QualityControlConfiguration : IEntityTypeConfiguration<QualityControl>
    {
        public void Configure(EntityTypeBuilder<QualityControl> entity)
        {
            entity.ToTable("QualityControl", "qc");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.PassportPrintId).HasColumnName("PassportPrintID");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.PassportPrint)
                .WithMany(p => p.QualityControl)
                .HasForeignKey(d => d.PassportPrintId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("qualitycontrol_fk");
        }
    }
}
