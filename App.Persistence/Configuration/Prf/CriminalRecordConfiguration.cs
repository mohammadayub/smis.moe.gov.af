using App.Domain.Entity.prf;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prf
{
    public class CriminalRecordConfiguration : IEntityTypeConfiguration<CriminalRecord>
    {
        public void Configure(EntityTypeBuilder<CriminalRecord> entity)
        {
            entity.ToTable("CriminalRecord", "prf");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.CrimeTypeId).HasColumnName("CrimeTypeID");

            entity.Property(e => e.Date).HasColumnType("date");

            entity.Property(e => e.Description).HasColumnType("character varying");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ProfileId).HasColumnName("ProfileID");

            entity.Property(e => e.ReferenceNo).HasColumnType("character varying");

            entity.HasOne(d => d.CrimeType)
                .WithMany(p => p.CriminalRecord)
                .HasForeignKey(d => d.CrimeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("criminalrecord_ct_fk");

            entity.HasOne(d => d.Profile)
                .WithMany(p => p.CriminalRecord)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("criminalrecord_fk");
        }
    }
}
