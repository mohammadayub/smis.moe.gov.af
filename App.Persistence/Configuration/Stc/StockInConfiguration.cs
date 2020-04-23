using App.Domain.Entity.stc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Stc
{
    public class StockInConfiguration : IEntityTypeConfiguration<StockIn>
    {
        public void Configure(EntityTypeBuilder<StockIn> entity)
        {
            entity.ToTable("StockIn", "stc");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.PassportDurationId).HasColumnName("PassportDurationID");

            entity.Property(e => e.PassportTypeId).HasColumnName("PassportTypeID");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.Property(e => e.ToUserId).HasColumnName("ToUserID");

            entity.HasOne(d => d.PassportDuration)
                .WithMany()
                .HasForeignKey(d => d.PassportDurationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stockin_fk_1");

            entity.HasOne(d => d.PassportType)
                .WithMany()
                .HasForeignKey(d => d.PassportTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("stockin_fk");
        }
    }
}
