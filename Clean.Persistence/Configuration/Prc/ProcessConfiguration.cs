using Clean.Domain.Entity.prc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clean.Persistence.Configuration.Prc
{
    public class ProcessConfiguration : IEntityTypeConfiguration<Process>
    {
        public void Configure(EntityTypeBuilder<Process> entity)
        {
            entity.ToTable("Process", "prc");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .UseIdentityAlwaysColumn();

            entity.Property(e => e.Description).HasMaxLength(500);

            entity.Property(e => e.Name).HasMaxLength(200);

            entity.Property(e => e.ScreenId).HasColumnName("ScreenID");

            entity.Property(e => e.Sorter).HasMaxLength(10);

            entity.HasOne(d => d.Screen)
                .WithMany(e => e.Process)
                .HasForeignKey(d => d.ScreenId)
                .HasConstraintName("_Process__FK");
        }
    }
}
