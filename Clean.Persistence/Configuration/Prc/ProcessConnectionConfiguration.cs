using Clean.Domain.Entity.prc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Prc
{
    public class ProcessConnectionConfiguration : IEntityTypeConfiguration<ProcessConnection>
    {
        public void Configure(EntityTypeBuilder<ProcessConnection> entity)
        {
            entity.ToTable("ProcessConnection", "prc");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.ProcessId).HasColumnName("ProcessID");

            entity.HasOne(d => d.ConnectedToNavigation)
                .WithMany(p => p.ProcessConnectionConnectedToNavigation)
                .HasForeignKey(d => d.ConnectedTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_ProcessConnection__FK_1");

            entity.HasOne(d => d.Process)
                .WithMany(p => p.ProcessConnectionProcess)
                .HasForeignKey(d => d.ProcessId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("_ProcessConnection__FK");
        }
    }
}
