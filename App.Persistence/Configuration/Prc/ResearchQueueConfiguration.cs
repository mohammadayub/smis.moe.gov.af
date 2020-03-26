using App.Domain.Entity.prc;
using App.Domain.Entity.qc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Qc
{
    public class ResearchQueueConfiguration : IEntityTypeConfiguration<ResearchQueue>
    {
        public void Configure(EntityTypeBuilder<ResearchQueue> entity)
        {
            entity.ToTable("ResearchQueue", "prc");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

            entity.Property(e => e.AssignedDate).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ProcessedDate).HasColumnType("timestamp with time zone");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Application)
                .WithMany(p => p.ResearchQueue)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_ResearchQueue__FK");
        }
    }
}
