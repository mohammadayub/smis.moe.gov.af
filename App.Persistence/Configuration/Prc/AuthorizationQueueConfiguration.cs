using App.Domain.Entity.prc;
using App.Domain.Entity.qc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prc
{
    public class AuthorizationQueueConfiguration : IEntityTypeConfiguration<AuthorizationQueue>
    {
        public void Configure(EntityTypeBuilder<AuthorizationQueue> entity)
        {
            entity.ToTable("AuthorizationQueue", "prc");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

            entity.Property(e => e.AssignedDate).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ProcessedDate).HasColumnType("timestamp with time zone");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Application)
                .WithMany(p => p.AuthorizationQueue)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_AuthorizationQueue__FK");
        }
    }
}
