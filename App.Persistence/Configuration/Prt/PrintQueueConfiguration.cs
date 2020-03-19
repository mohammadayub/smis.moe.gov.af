using App.Domain.Entity.prt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prt
{
    public class PrintQueueConfiguration : IEntityTypeConfiguration<PrintQueue>
    {
        public void Configure(EntityTypeBuilder<PrintQueue> entity)
        {
            entity.ToTable("PrintQueue", "prt");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.ProcessedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Application)
                .WithMany(p => p.PrintQueue)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_PrintQueue__FK");
        }
    }
}
