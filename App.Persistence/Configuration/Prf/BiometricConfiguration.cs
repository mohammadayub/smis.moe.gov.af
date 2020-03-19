using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class BiometricConfiguration : IEntityTypeConfiguration<Biometric>
    {
        public void Configure(EntityTypeBuilder<Biometric> entity)
        {
            entity.ToTable("Biometric", "prf");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.BiometricTypeId).HasColumnName("BiometricTypeID");

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp(0) with time zone");

            entity.Property(e => e.Description).HasColumnType("character varying");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp(0) with time zone");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.HasOne(d => d.BiometricType)
                .WithMany(p => p.Biometric)
                .HasForeignKey(d => d.BiometricTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("biometric_bt_fk");

            entity.HasOne(d => d.Profile)
                .WithMany(p => p.Biometric)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("biometric_fk");
        }
    }
}
