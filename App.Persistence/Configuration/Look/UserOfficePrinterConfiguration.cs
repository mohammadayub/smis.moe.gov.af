using App.Domain.Entity.look;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Persistence.Configuration.Look
{
    public class UserOfficePrinterConfiguration : IEntityTypeConfiguration<UserOfficePrinter>
    {
        public void Configure(EntityTypeBuilder<UserOfficePrinter> entity)
        {
            entity.ToTable("UserOfficePrinter", "Look");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.CreatedOn).HasColumnType("timestamp with time zone");

            entity.Property(e => e.OfficeId).HasColumnName("OfficeID");

            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Office)
                .WithMany(p => p.UserOfficePrinter)
                .HasForeignKey(d => d.OfficeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_UserOfficePrinter__FK");
        }
    }
}
