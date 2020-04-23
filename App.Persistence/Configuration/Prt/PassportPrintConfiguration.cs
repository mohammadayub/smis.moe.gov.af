using App.Domain.Entity.prt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Prt
{
    public class PassportPrintConfiguration : IEntityTypeConfiguration<PassportPrint>
    {
        public void Configure(EntityTypeBuilder<PassportPrint> entity)
        {
            entity.ToTable("PassportPrint", "prt");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp(0) with time zone");

            entity.Property(e => e.PassportId).HasColumnName("PassportID");

            entity.Property(e => e.PrintQueueId).HasColumnName("PrintQueueID");

            entity.Property(e => e.PrintedDate).HasColumnType("date");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.Property(e => e.ValidTo).HasColumnType("date");

            entity.HasOne(d => d.Passport)
                .WithMany(p => p.PassportPrint)
                .HasForeignKey(d => d.PassportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_PassportPrint__FK_1");

            entity.HasOne(d => d.PrintQueue)
                .WithMany(p => p.PassportPrint)
                .HasForeignKey(d => d.PrintQueueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_PassportPrint__FK");
        }
    }
}
