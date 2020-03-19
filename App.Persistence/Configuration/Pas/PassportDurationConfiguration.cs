using App.Domain.Entity.pas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Pas
{
    public class PassportDurationConfiguration : IEntityTypeConfiguration<PassportDuration>
    {
        public void Configure(EntityTypeBuilder<PassportDuration> entity)
        {
            entity.ToTable("PassportDuration", "pas");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ModifiedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.PassportTypeId).HasColumnName("PassportTypeID");

            entity.HasOne(d => d.PassportType)
                .WithMany(p => p.PassportDuration)
                .HasForeignKey(d => d.PassportTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_PassportDuration__FK");
        }
    }
}
