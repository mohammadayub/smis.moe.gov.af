using App.Domain.Entity.pas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Pas
{
    public class PaymentConfigConfiguration : IEntityTypeConfiguration<PaymentConfig>
    {
        public void Configure(EntityTypeBuilder<PaymentConfig> entity)
        {
            entity.ToTable("PaymentConfig", "pas");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

            entity.Property(e => e.PassportDurationId).HasColumnName("PassportDurationID");

            entity.Property(e => e.PassportTypeId).HasColumnName("PassportTypeID");

            entity.Property(e => e.PaymentCategoryId).HasColumnName("PaymentCategoryID");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.PassportDuration)
                .WithMany(p => p.PaymentConfig)
                .HasForeignKey(d => d.PassportDurationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_PaymentConfig__FK_1");

            entity.HasOne(d => d.PassportType)
                .WithMany(p => p.PaymentConfig)
                .HasForeignKey(d => d.PassportTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_PaymentConfig__FK");

            entity.HasOne(d => d.PaymentCategory)
                .WithMany(p => p.PaymentConfig)
                .HasForeignKey(d => d.PaymentCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_PaymentConfig__FK_2");
        }
    }
}
