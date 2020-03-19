using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class BiometricDetailConfiguration : IEntityTypeConfiguration<BiometricDetail>
    {
        public void Configure(EntityTypeBuilder<BiometricDetail> entity)
        {
            entity.ToTable("BiometricDetail", "prf");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.BiometricCode)
                .IsRequired()
                .HasColumnType("character varying");

            entity.Property(e => e.BiometricId).HasColumnName("BiometricID");

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.HasOne(d => d.Biometric)
                .WithMany(p => p.BiometricDetail)
                .HasForeignKey(d => d.BiometricId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("biometricdetail_fk");
        }
    }
}
