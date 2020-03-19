using Clean.Domain.Entity.au;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Au
{
    public class AuditConfiguration : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> entity)
        {
            entity.ToTable("Audit", "au");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.DbContextObject).HasMaxLength(100);

            entity.Property(e => e.DbObjectName).HasMaxLength(100);

            entity.Property(e => e.OperationTypeId).HasColumnName("OperationTypeID");

            entity.Property(e => e.RecordId)
                .HasColumnName("RecordID")
                .HasMaxLength(200);

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.Property(e => e.ValueAfter).HasColumnType("character varying");

            entity.Property(e => e.ValueBefore).HasColumnType("character varying");

            entity.HasOne(d => d.OperationType)
                .WithMany(p => p.Audit)
                .HasForeignKey(d => d.OperationTypeId)
                .HasConstraintName("audit_fk");
        }
    }
}
