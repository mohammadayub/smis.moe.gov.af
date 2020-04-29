using App.Domain.Entity.pas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Pas
{
    public class DisabledPassportsConfiguration : IEntityTypeConfiguration<DisabledPassport>
    {
        public void Configure(EntityTypeBuilder<DisabledPassport> entity)
        {
            entity.ToTable("DisabledPassport", "pas");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.DisabledReasonId).HasColumnName("DisabledReasonID");

            entity.HasOne(d => d.Application)
                .WithMany(p => p.DisabledPassport)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_DisabledPassport__FK");

            entity.HasOne(d => d.DisabledReason)
                .WithMany(p => p.DisabledPassport)
                .HasForeignKey(d => d.DisabledReasonId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_DisabledPassport__FK_1");
        }
    }
}
